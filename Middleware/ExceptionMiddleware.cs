using System.Net;
using System.Reflection.Metadata;
using System.Text.Json;
using ShiftManager.Api.Core;

namespace ShiftManager.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger
        )
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception
        )
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                ArgumentException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;

            var response = ApiResponse<object>.Fail(
                message: GetMessage(exception),
                errors: new
                {
                    type = exception.GetType().Name,
                    detail = exception.Message
                }
            );

            var json = JsonSerializer.Serialize(response,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
            );

            await context.Response.WriteAsync(json);
        }

        private static string GetMessage(Exception exception)
        {
            return exception switch
            {
                ArgumentException => "Invalid request",
                UnauthorizedAccessException => "Unauthorized access",
                KeyNotFoundException => "Resource not found",
                _ => "Something went wrong. Please try again later."
            };
        }
    }
}