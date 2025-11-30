using MassTransit;
using Payments.Application.DTOs;

namespace UserAPI.Consumers
{
    public class PaymentSucceededConsumer : IConsumer<PaymentSucceededEvent>
    {
        private readonly ILogger<PaymentSucceededConsumer> _logger;

        public PaymentSucceededConsumer(ILogger<PaymentSucceededConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<PaymentSucceededEvent> context)
        {

            var command = context.Message;

            _logger.LogInformation($"[UsersAPI] O usuário {command.UserId} pagou, pode liberar o jogo {command.GameId}");

            return Task.CompletedTask;
        }
    }
}
