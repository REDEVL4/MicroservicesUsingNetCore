using MassTransit;
using MessagingBrokerDefaults.Core;
using MessagingBrokerDefaults.Models;
using Order.Api.EventConsumer;
using Order.Api.Extensions;
using Order.Application;
using Order.Infastructure;
using Order.Infastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMassTransit((context) =>
{
    context.AddConsumer<OrderEventConsumer>();
    context.UsingRabbitMq((ctx, config) =>
    {
        config.Host(builder.Configuration.GetValue<string>("Secrets:RabbitMqHost"));
        config.ReceiveEndpoint(Constants.QueueName, configure => configure.ConfigureConsumer(ctx,typeof(OrderEventConsumer))); ;
    });
});
builder.Services.InfastructureServices(builder.Configuration);
builder.Services.ApplicationServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 app.MigrateDatabase<OrderContext>( (context,service) =>
{
    var logger=service.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
