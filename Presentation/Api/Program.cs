using Application.Mapping;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Persistence.DependencyInjection; // ServiceContainer'ın bulunduğu namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // This is to generate the Default UI of Swagger Documentation
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ASP.NET 8 Web API",
        Description = "Authentication with JWT"
    });
});

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

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
