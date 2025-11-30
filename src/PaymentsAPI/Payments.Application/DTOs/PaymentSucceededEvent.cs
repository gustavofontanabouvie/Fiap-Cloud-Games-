using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Application.DTOs
{
    public record PaymentSucceededEvent
    {
        public Guid OrderId { get; init; }
        public Guid UserId { get; init; }
        public Guid GameId { get; init; }

        public decimal AmountPaid { get; init; }

        public DateTime PaidAt { get; init; }
    }
}
