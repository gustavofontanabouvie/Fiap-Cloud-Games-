using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Application.DTOs
{
    public record PaymentFailedEvent
    {
        public Guid OrderId { get; init; }
        public string Reason { get; init; }
    }
}
