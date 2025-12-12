using Microsoft.EntityFrameworkCore;
using Payments.Data.Context;
using Payments.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Data.Repositories.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentsDbContext _dbContext;

        public PaymentRepository(PaymentsDbContext context)
        {
            _dbContext = context;
        }
    }
}
