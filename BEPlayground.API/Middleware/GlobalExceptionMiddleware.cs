using System.Net;
using System.Text.Json;
using FluentValidation;

namespace BEPlayground.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    message = "Validation failed",
                    errors = ex.Errors.Select(e => e.ErrorMessage)
                };

                await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
            }
        }

    }
}

