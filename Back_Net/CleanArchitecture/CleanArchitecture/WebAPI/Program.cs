using Application.Interfaces;
using Application.Interfaces.Products;
using Application.UseCases.Products;
using Domain.Interfaces;
using Infrastructure.DataContext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// ------------------------------------------------------------------------------------------------------------------
// Register CORS policies
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200") // Ruta de frontend - Angular
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//--- add connection string
builder.Services.AddDbContext<SmartTalentContext>
        (options => options.UseSqlServer(builder.Configuration.GetConnectionString("_connectionString")));


//--- add repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//--- add services and use cases
builder.Services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
builder.Services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
builder.Services.AddScoped<IGetAllProductUseCase, GetAllProductUseCase>();
builder.Services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
builder.Services.AddScoped<IGetProductPagedUseCase, GetProductPagedUseCase>();
builder.Services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();

// ------------------------------------------------------------------------------------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

//Add CORS policies ---------------------------------------------------------------------------------
app.UseCors("DevPolicy");

app.MapControllers();

app.Run();
