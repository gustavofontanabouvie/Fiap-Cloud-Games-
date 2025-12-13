using Payments.Application.DTOs;
using Payments.Application.Interfaces;
using Payments.Data.Repositories.Interface;
using Payments.Domain;
using Payments.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payments.Domain.Common.Constants;

namespace Payments.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentGatewayService _paymentGatewayService;

    public PaymentService(IPaymentRepository paymentRepository, IPaymentGatewayService paymentGatewayService)
    {
        _paymentRepository = paymentRepository;
        _paymentGatewayService = paymentGatewayService;
    }
    public async Task<Result<OrderResponse>> CreateOrderAsync(CreatedOrderRequest request, CancellationToken cancellationToken)
    {
        bool orderExists = await _paymentRepository.ExistsAsync(o => o.UserId == request.userId && o.GameId == request.gameId && (o.PaymentStatus == OrderPaymentStatus.Paid || o.PaymentStatus == OrderPaymentStatus.Pending), cancellationToken);

        if (orderExists)
            return Result<OrderResponse>.Failure("Order already exists or in transit");

        try
        {


            var order = new Order(request.price, request.userId, request.gameId);

            var command = new ProcessPaymentCommand()
            {
                CreditCardToken = request.creditCardToken,
                GameId = request.gameId,
                OrderId = order.Id,
                UserId = request.userId
            };

            var isApproved = await _paymentGatewayService.ChargeCreditCardAsync(command);

            if (isApproved)
            {
                order.MarkAsPaid();
                order.SetTransactionId(Guid.NewGuid().ToString());
            }
            else
            {
                order.MarkAsFailed();
            }

            await _paymentRepository.AddAsync(order, cancellationToken);

            await _paymentRepository.SaveChangesAsync(cancellationToken);

            var response = new OrderResponse(order.Id, order.Number, order.PaymentStatus.ToString());

            return Result<OrderResponse>.Success(response);

        }
        catch (DomainException ex)
        {
            return Result<OrderResponse>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<OrderResponse>.Failure(ex.Message);
        }

    }

    public async Task<Result<OrderResponse>> GetOrderByPaymentStatus(int status, CancellationToken cancellationToken)
    {
        var paymentStatus = (OrderPaymentStatus)status;

        var orders = await _paymentRepository.FindAsync(o => o.PaymentStatus == paymentStatus, cancellationToken);

        var order = orders.FirstOrDefault();
        if (order == null)
            return Result<OrderResponse>.Failure("Order not found");

        var response = new OrderResponse(order.Id, order.Number, order.PaymentStatus.ToString());

        return Result<OrderResponse>.Success(response);
    }
}
