using FluentValidation;
using FluentValidation.AspNetCore;
using InventariosApi.Middlewares;
using InventariosApi.Repositories;
using InventariosApi.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Repositories and Services
builder.Services.AddSingleton<VehiculosRepository>();
builder.Services.AddScoped<VehiculosService>();

builder.Services.AddScoped<IClientesRepository, ClientesRepository>();
builder.Services.AddScoped<IClientesService, ClientesService>();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventarios API v1");
    options.RoutePrefix = string.Empty;
});

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.MapControllers();

try
{
    app.Run();
}
catch (ReflectionTypeLoadException ex)
{
    foreach (var e in ex.LoaderExceptions)
    {
        Console.WriteLine(e.Message);
    }
    throw;
}
