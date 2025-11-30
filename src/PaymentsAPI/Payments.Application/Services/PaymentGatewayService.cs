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
        public Task<bool> ChargeCreditCardAsync(string creditCardToken, decimal amount)
        {
            //simulação de cobrança no gateway de pagamento
            return Task.FromResult(true);
        }
    }
}
