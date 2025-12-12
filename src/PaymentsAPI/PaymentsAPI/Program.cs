using MassTransit;
using Microsoft.EntityFrameworkCore;
using Payments.Application.Interfaces;
using Payments.Application.Services;
using Payments.Data.Context;
using Payments.Data.Repositories.Implementation;
using Payments.Data.Repositories.Interface;
using PaymentsAPI.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region "DI"

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddTransient<IPaymentGatewayService, PaymentGatewayService>();


#endregion

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Connection String DefaultConnection not found");

builder.Services.AddDbContext<PaymentsDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();

});

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
