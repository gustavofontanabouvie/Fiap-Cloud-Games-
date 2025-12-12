

using Payments.Application.DTOs;
using Payments.Domain.Common;

namespace Payments.Application.Interfaces;

public interface IPaymentService
{
    Task<Result<CreateOrderResponse>> CreateOrderAsync(CreatedOrderRequest request);
}
