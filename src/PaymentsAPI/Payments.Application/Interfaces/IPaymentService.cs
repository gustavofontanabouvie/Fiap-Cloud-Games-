

using Payments.Application.DTOs;
using Payments.Domain.Common;

namespace Payments.Application.Interfaces;

public interface IPaymentService
{
    Task<Result<OrderResponse>> CreateOrderAsync(CreatedOrderRequest request, CancellationToken cancellationToken);
    Task<Result<OrderResponse>> GetOrderByPaymentStatus(int status, CancellationToken cancellationToken);
}
