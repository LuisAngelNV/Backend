using System.Net;
using System.Text.Json;

namespace InventariosApi.Middlewares
{
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
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var error = new
                {
                    mensaje = ex.Message,
                    detalle = ex.StackTrace
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
        }
    }
}
