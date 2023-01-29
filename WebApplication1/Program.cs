using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.Application;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Web.Http;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Interface;
using WebApplication1.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EKAMA"));

});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    var xmlFilename = @"C:\Users\Hp\source\repos\WebApplication1\WebApplication1\bin\Debug\net6.0\SwaggerDemoApi.XML";
    var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFilename);
    x.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<ICategory, CategoryRepo>();
builder.Services.AddScoped<ILocation, LocationRepo>();
builder.Services.AddScoped<IProduct, ProductRepo>();
builder.Services.AddScoped<IAuthentication, AuthenticationRepo>();
builder.Services.AddIdentityCore<Customer>().AddEntityFrameworkStores<AppDbContext>();
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
