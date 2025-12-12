using Payments.Application.DTOs;
using Payments.Application.Interfaces;
using Payments.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Application.Services;

public class PaymentService : IPaymentService
{
    public Task<Result<CreateOrderResponse>> CreateOrderAsync(CreatedOrderRequest request)
    {
        throw new NotImplementedException();
    }
}
