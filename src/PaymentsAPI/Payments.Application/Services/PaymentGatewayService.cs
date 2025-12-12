using Payments.Application.DTOs;
using Payments.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Application.Services
{
    public class PaymentGatewayService : IPaymentGatewayService
    {
        public Task<bool> ChargeCreditCardAsync(ProcessPaymentCommand command)
        {
            if (string.IsNullOrEmpty(command.CreditCardToken))
                return Task.FromResult(false);

            //simulação de cobrança no gateway de pagamento
            return Task.FromResult(true);
        }
    }
}
