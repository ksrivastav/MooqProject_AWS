using Catalog.Infrastrcture.DBContext;
using Microsoft.EntityFrameworkCore;
using Catalog.Infrastrcture.Repository;
using Catalog.Core.RepositoryContracts;
using Catalog.Core.Entities;
using Asp.Versioning;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Catalog.Application.Handler;
using Catalog.Application;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var i = Assembly.GetExecutingAssembly();
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(GetAllProductRequestHandler)));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllProductRequestHandler).Assembly));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly([typeof(GetAllProductRequestHandler).Assembly]));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AppCls>());

builder.Services.AddDbContext<DaffECommerceDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnectionString"));
});

//builder.Services.AddTransient<IProductCategoryService<ProductCategory>, ProductCategoryService>();
//builder.Services.AddTransient<IProductSubCategoryService<ProductSubCategory>, ProductSubCategoryService>();
//builder.Services.AddTransient<IProductService<Product>, ProductService>();
builder.Services.AddTransient<IProductCategoryRepository<ProductCategory>, ProductCategoryRepository>();
builder.Services.AddTransient<IProductSubCategoryRepository<ProductSubCategory>, ProductSubCategoryRepository>();
builder.Services.AddTransient<IProductRepository<Product>, ProductRepository>();
builder.Services.AddTransient<Product>();
builder.Services.AddTransient<ProductSubCategory>();
builder.Services.AddTransient<ProductCategory>();



builder.Services.AddApiVersioning(
    o =>
    {
        o.AssumeDefaultVersionWhenUnspecified = true;
        o.DefaultApiVersion = new ApiVersion(1, 0);
        o.ReportApiVersions = true;
        o.ApiVersionReader = ApiVersionReader.Combine(
            new QueryStringApiVersionReader("api-version"),
            new HeaderApiVersionReader("X-Version"),
            new MediaTypeApiVersionReader("ver"));
    }
).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddAutoMapper(typeof(Program));
//var assembleToScan = 
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

//builder.Services.AddMediatR(typeof(CreateProductRequestHandler).GetTypeInfo().Assembly);
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
