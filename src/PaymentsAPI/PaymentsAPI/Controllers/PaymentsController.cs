using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderResponse>> GetOrderById(Guid id, CancellationToken cancellationToken)
        {
            //var result = await _paymentService.GetOrderById(id, cancellationToken);

            //if (!result.IsSuccess)
            //    return NotFound(new { error = result.Error });

            return Ok();/*(result.Value)*/
        }


        [HttpGet]
        public async Task<ActionResult<OrderResponse>> GetOrderByStatus([FromBody] int status, CancellationToken cancellationToken)
        {
            var result = await _paymentService.GetOrderByPaymentStatus(status, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponse>> PostOrder([FromBody] CreatedOrderRequest request, CancellationToken cancellationToken)
        {
            var result = await _paymentService.CreateOrderAsync(request, cancellationToken);

            if (!result.IsSuccess)
                return Conflict(new { error = result.Error });

            return CreatedAtAction("GetOrder", new { id = result.Value.id }, result.Value);

        }

    }
}
