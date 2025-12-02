using System.Diagnostics.Metrics;
using InventariosApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/api/productos", () =>
{
    var productos = new[]
    {
        new { Id = 1, Nombre = "Laptop", Precio = 15000 },
        new { Id = 2, Nombre = "Mouse", Precio = 250 },
        new { Id = 3, Nombre = "Teclado", Precio = 600 }
    };

    return productos;
});

app.MapGet("/api/usuarios", () =>
{
    var usuarios = new[]
    {
        new { Id = 1, Nombre = "Luis" },
        new { Id = 2, Nombre = "Ana" }
    };
    return usuarios;
});
app.MapPost("/api/usuarios", (Usuario nuevoUsuario) =>
{
    if (string.IsNullOrWhiteSpace(nuevoUsuario.Nombre))
        return Results.BadRequest("El nombre del usuario es obligatorio.");
    if(nuevoUsuario.Nombre.Length < 3)
        return Results.BadRequest("El nombre del usuario debe tener al menos 3 caracteres.");    
    nuevoUsuario.Id = 99; // Simulación de "ID generado"
    return Results.Created($"/api/usuarios/{nuevoUsuario.Id}", nuevoUsuario);
});
app.MapGet("/api/usuarios/{id:int}", (int id) =>
{
    var usuarios = new[]
    {
        new { Id = 1, Nombre = "Luis" },
        new { Id = 2, Nombre = "Ana" }
    };

    var usuario = usuarios.FirstOrDefault(p => p.Id == id);

    if (usuario is null)
        return Results.NotFound("Usuario no encontrado.");

    return Results.Ok(usuario);
});

app.MapPost("/api/productos", (Producto nuevoProducto) =>
{
    if (string.IsNullOrWhiteSpace(nuevoProducto.Nombre))
        return Results.BadRequest("El nombre del producto es obligatorio.");

    if (nuevoProducto.Precio <= 0)
        return Results.BadRequest("El precio debe ser mayor a 0.");

    nuevoProducto.Id = 123;
    return Results.Created($"/api/productos/{nuevoProducto.Id}", nuevoProducto);
});
app.MapGet("/api/productos/{id:int}", (int id) =>
{
    var productos = new[]
    {
        new { Id = 1, Nombre = "Laptop", Precio = 15000 },
        new { Id = 2, Nombre = "Mouse", Precio = 250 },
        new { Id = 3, Nombre = "Teclado", Precio = 600 }
    };

    var producto = productos.FirstOrDefault(p => p.Id == id);

    if (producto is null)
        return Results.NotFound("Producto no encontrado.");

    return Results.Ok(producto);
});
app.MapGet("/api/error", () =>
{
    throw new Exception("Ups, ocurrió un error inesperado");
});




app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
