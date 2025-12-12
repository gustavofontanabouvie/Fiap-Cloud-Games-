using MassTransit;
using Payments.Application.DTOs;
using Payments.Application.Interfaces;


namespace PaymentsAPI.Consumers
{
    public class PaymentRequestConsumer : IConsumer<ProcessPaymentCommand>
    {
        private readonly IPaymentGatewayService _paymentGatewayService;
        private readonly ILogger<PaymentRequestConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public PaymentRequestConsumer(IPaymentGatewayService paymentGatewayService, ILogger<PaymentRequestConsumer> logger, IPublishEndpoint publishEndpoint)
        {
            _paymentGatewayService = paymentGatewayService;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }


        public async Task Consume(ConsumeContext<ProcessPaymentCommand> context)
        {
            //salvar todos os dados em command
            var command = context.Message;

            _logger.LogInformation($"Inicio de processamento do pedido {command.OrderId}");

            try
            {
                //Será necessário implementar um gRPC para buscar o valor real do jogo no CatalogAPI

                //Simulando que fomos no CatalogAPI e buscamos o valor real do jogo
                decimal realPrice = await GetPriceFromCatalogGrpcAsync(command.GameId);

                if (realPrice <= 0)
                {
                    throw new Exception("Preço do jogo inválido.");
                }

                var charged = await _paymentGatewayService.ChargeCreditCardAsync(command);

                if (charged)
                {
                    //Se o pagamento foi carregado, vai publicar o evento de pagamento aprovado para avisar UserAPI e NotificationsAPI
                    _logger.LogInformation($"Pagamento aprovado para o pedido {command.OrderId}");

                    await _publishEndpoint.Publish(new PaymentSucceededEvent
                    {
                        OrderId = command.OrderId,
                        UserId = command.UserId,
                        GameId = command.GameId,
                        AmountPaid = realPrice,
                        PaidAt = DateTime.UtcNow

                    });
                }
                else
                {
                    _logger.LogWarning($"Pagamento recusado para o pedido {command.OrderId}");

                    //publica falha para os interessados

                    await _publishEndpoint.Publish(new PaymentFailedEvent
                    {
                        OrderId = command.OrderId,
                        Reason = "Operadora recusou o cartão"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro crítico no processamento");

                throw;
            }

        }

        private async Task<decimal> GetPriceFromCatalogGrpcAsync(Guid gameId)
        {
            //TODO: implementar a chamada gRPC para o CatalogAPI

            return await Task.FromResult(60.99m);
        }
    }
}

