using Microsoft.EntityFrameworkCore;
using Payments.Data.Context;
using Payments.Data.Repositories.Interface;
using Payments.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Data.Repositories.Implementation
{
    public class PaymentRepository : Repository<Order>, IPaymentRepository
    {
        private readonly PaymentsDbContext _dbContext;

        public PaymentRepository(PaymentsDbContext context) : base(context)
        {
            {
                _dbContext = context;
            }
        }
    }
}
