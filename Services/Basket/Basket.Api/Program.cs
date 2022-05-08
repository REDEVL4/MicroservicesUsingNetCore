using StackExchange.Redis.Configuration;
using Basket.Api.Repository;
using Basket.Api.Services;
using MassTransit;
using Discount.Grpc.Protos;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<DServices>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddStackExchangeRedisCache(c =>
{
    c.InstanceName = "_micro";
    c.Configuration = builder.Configuration.GetValue<String>("Secrets:RedisCS");
}
);
builder.Services.AddMassTransit((context) =>
{
    context.UsingRabbitMq((ctx, config) =>
    {
        config.Host(builder.Configuration.GetValue<string>("Secrets:RabbitMqHost"));
    });
});
builder.Services.AddGrpcClient<DiscountServices.DiscountServicesClient>(c => c.Address = new Uri(builder.Configuration.GetValue<string>("Secrets:GRPC_Address")));
//builder.Services.AddGrpcClient<DiscountService.DiscountServiceClient>(c=>c.Address=new Uri(builder.Configuration.GetValue<string>("Secrets:GRPC_Address")));
builder.Services.AddSingleton<IBasketRepository,BasketRepository>();
builder.Services.AddScoped<DServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
