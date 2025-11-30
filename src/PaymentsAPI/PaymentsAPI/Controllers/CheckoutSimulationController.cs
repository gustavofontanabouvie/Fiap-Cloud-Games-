using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.DTOs;

namespace PaymentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutSimulationController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public CheckoutSimulationController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]

        public async Task<IActionResult> SimulatePurchase([FromBody] ProcessPaymentCommand command)
        {

            await _publishEndpoint.Publish(command);
            return Accepted(new { status = "Pedido recebido e sendo processado" });
        }
    }
}
