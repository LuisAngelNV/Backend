using FluentValidation;
using FluentValidation.AspNetCore;
using InventariosApi.Repositories;
using InventariosApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Repositories and Services
builder.Services.AddSingleton<VehiculosRepository>();
builder.Services.AddScoped<VehiculosService>();


var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
