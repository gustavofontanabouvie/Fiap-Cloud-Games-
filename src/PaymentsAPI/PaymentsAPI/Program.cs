using MassTransit;
using Payments.Application.Interfaces;
using Payments.Application.Services;
using PaymentsAPI.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region "DI"

builder.Services.AddTransient<IPaymentGatewayService, PaymentGatewayService>();

#endregion
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PaymentRequestConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("amqp://host.docker.internal:5672"), host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        //cria as filas automaticamente com base nos consumidores
        cfg.ConfigureEndpoints(context);
    });

});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
