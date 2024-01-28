using Order.Core.Repository;
using Order.Infrastructure.AppDbContext;
using Order.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Order.Application.Querry;
using MassTransit;
using System.Reflection;
using Amazon.SQS;
using Amazon.Extensions.NETCore.Setup;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetOrderByUsernameQuerry>());

builder.Services.AddDbContext<DaffECommerceDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnectionString"));
});


builder.Services.AddTransient<IOrderRepository<Order.Core.Entities.Order>, OrderRepository>();
builder.Services.AddTransient<Order.Core.Entities.Order>();
builder.Services.AddAWSService<IAmazonSQS>();
AWSOptions option = new AWSOptions();
option.Profile = "default";
builder.Services.AddDefaultAWSOptions(option);

//builder.Services.AddMassTransit(config =>
//{
//    config.AddConsumers(Assembly.GetExecutingAssembly());
//    config.UsingRabbitMq((ctx, cfg) =>
//    {
//        cfg.Host(new Uri(builder.Configuration["ServiceBus:Uri"]),
//        h =>
//        {
//            h.Username(builder.Configuration["ServiceBus:Username"]);
//            h.Password(builder.Configuration["ServiceBus:Password"]);
//        });
//        cfg.ConfigureEndpoints(ctx);
//        //cfg.ReceiveEndpoint(builder.Configuration["ServiceBus:Queue"],
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
