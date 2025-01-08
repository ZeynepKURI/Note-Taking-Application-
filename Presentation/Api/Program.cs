
using AutoMapper;
using Domain.Mapping;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.DependencyInjection; // ServiceContainer'ın bulunduğu namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.InfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

