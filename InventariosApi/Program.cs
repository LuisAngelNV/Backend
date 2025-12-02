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
    // Aquí normalmente lo guardaríamos en BD
    // Por ahora solo devolveremos lo que recibimos

    nuevoUsuario.Id = 99; // Simulación de "ID generado"
    return Results.Created($"/api/usuarios/{nuevoUsuario.Id}", nuevoUsuario);
});

app.MapPost("/api/productos", (Producto nuevoProducto) =>
{
    nuevoProducto.Id = 123;
    return Results.Created($"/api/productos/{nuevoProducto.Id}", nuevoProducto);
});



app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
