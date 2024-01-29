using Order.Core.Repository;
using Order.Infrastructure.AppDbContext;
using Order.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Order.Application.Querry;
using MassTransit;
using System.Reflection;
using Amazon.SQS;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
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

builder.Services.AddCognitoIdentity();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = $"https://cognito-idp.us-east-1.amazonaws.com/us-east-1_JqsS6MxYl";   //builder.Configuration["Cognito:Authority"];
    options.RequireHttpsMetadata = false;
    //var key = Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]);
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
       // ValidateIssuerSigningKey = true,
        ValidIssuer = $"https://cognito-idp.us-east-1.amazonaws.com/us-east-1_JqsS6MxYl",
        ValidateAudience = false,
        ValidateIssuer = true
        //IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
