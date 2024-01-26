using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using IdentityServerCognito;
using IdentityServerCognito.ConfigData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using IdentityServerCognito.CustomMiddleware;
using Amazon.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Amazon.Extensions.NETCore.Setup;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCognitoIdentity();
builder.Services.AddSingleton<IdentityCognitoConfigretriever>();
builder.Services.AddTransient<CacheMiddleware>();
builder.Services.AddMemoryCache();
builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AWS"));

AWSOptions option = new AWSOptions();
option.Profile = "default";
builder.Services.AddDefaultAWSOptions(option);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Cognito:Authority"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false
    };
});
//builder.Services.AddScoped<IAmazonCognitoIdentity>();
//builder.Services.AddScoped<CognitoUserPool>();

//builder.Services.AddSingleton<IAmazonCognitoIdentityProvider>(cognitoIdentityProvider);
//builder.Services.AddSingleton<CognitoUserPool>(cognitoUserPool);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CacheMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
