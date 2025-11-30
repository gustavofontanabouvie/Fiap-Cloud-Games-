using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Application.DTOs
{
    //Alguem enviou um pagamento, primeiro evento que chega na aplicacao
    public record ProcessPaymentCommand
    {
        public Guid OrderId { get; init; }
        public Guid UserId { get; init; }
        public Guid GameId { get; init; }
        public string CreditCardToken { get; init; }

    }

}
