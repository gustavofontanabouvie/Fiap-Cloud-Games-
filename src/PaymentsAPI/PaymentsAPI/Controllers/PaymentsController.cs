using Microsoft.AspNetCore.Mvc;
using Payments.Application.DTOs;
using Payments.Application.Interfaces;

namespace PaymentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost]
        public async Task<ActionResult<CreateOrderResponse>> PostOrder([FromBody] CreatedOrderRequest request)
        {
            var result = await _paymentService.CreateOrderAsync(request);

            if (!result.IsSuccess)
                return NotFound();

            return CreatedAtAction("GetOrder", new { id = result.Value.id }, result.Value);

        }

    }
}
