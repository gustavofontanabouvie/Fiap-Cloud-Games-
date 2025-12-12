using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Application.DTOs;

public record CreatedOrderRequest(
    Guid userId,
    Guid gameId,
    decimal price,
    string creditCardToken
    );

