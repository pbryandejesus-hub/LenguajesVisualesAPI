using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace LenguajesVisualesAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public ErrorHandlerMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                int status = (int)HttpStatusCode.InternalServerError;
                var result = new
                {
                    mensaje = "Ocurrió un error interno.",
                    detail = _env.IsDevelopment() ? ex.ToString() : null
                };
                response.StatusCode = status;
                await response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }
    }
}
