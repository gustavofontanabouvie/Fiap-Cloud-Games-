using Payments.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Data.Repositories.Interface
{
    public interface IPaymentRepository : IRepository<Order>
    {
    }
}
