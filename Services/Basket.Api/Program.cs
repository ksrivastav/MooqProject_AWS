using Basket.Application.Handler;
using Basket.Core.Domain.Entities;
using Basket.Core.RepositoryContracts;
using Basket.Infrastructure.Repository;
//using MassTransit;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetBasketByUsernameHandler>());

builder.Services.AddScoped<BasketCheckout>();
builder.Services.AddScoped<ShoppingCart>();
builder.Services.AddScoped<ShoppingCartItem>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSetting:ConnectionString");
});

//builder.Services.AddMassTransit(config =>
//{
//    //config.AddConsumers(Assembly.GetExecutingAssembly());
//    config.UsingRabbitMq((ctx, cfg) =>
//    {
//        cfg.Host(new Uri(builder.Configuration["ServiceBus:Uri"]),
//        h =>
//        {
//            h.Username(builder.Configuration["ServiceBus:Username"]);
//            h.Password(builder.Configuration["ServiceBus:Password"]);
//        });
//        //cfg.ConfigureEndpoints(ctx);
//        //cfg.ReceiveEndpoint(builder.Configuration["ServiceBus:Queue"];
//        //    c => c.ConfigureConsumer<OrderConsumer>(ctx)


//        //    ); 
//    });
//});

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
