public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var error = new
            {
                message = "Ocurrió un error inesperado.",
                exception = ex.GetType().Name,
                detail = ex.Message,
                stackTrace = ex.StackTrace,
                path = context.Request.Path.Value,
                method = context.Request.Method,
                timestamp = DateTime.UtcNow
            };

            // Log en consola
            Console.WriteLine("===== ERROR DETECTADO =====");
            Console.WriteLine($"Tipo: {ex.GetType().Name}");
            Console.WriteLine($"Mensaje: {ex.Message}");
            Console.WriteLine($"Ruta: {context.Request.Path}");
            Console.WriteLine($"Método: {context.Request.Method}");
            Console.WriteLine("============================");

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
