using MassTransit;
using Microsoft.EntityFrameworkCore;
using UserAPI.Consumers;
using Users.Data.Context;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<UsersDbContext>
    (options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    });

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PaymentSucceededConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(new Uri("amqp://host.docker.internal:5672"), host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ConfigureEndpoints(ctx);
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
