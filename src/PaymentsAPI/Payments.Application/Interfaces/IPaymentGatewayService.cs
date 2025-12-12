using Payments.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Application.Interfaces
{
    public interface IPaymentGatewayService
    {
        //true se coboru, false se falhou
        Task<bool> ChargeCreditCardAsync(ProcessPaymentCommand command);
    }
}
