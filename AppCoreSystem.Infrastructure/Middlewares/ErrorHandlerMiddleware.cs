namespace UniverstySystem.Infrastructure.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Net;
    using System.Text.Json;
    using AppCoreSystem.Application.Common.Bases;
    using AppCoreSystem.Application.Common.Exceptions;

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var correlationId = Guid.NewGuid().ToString();

            context.Response.Headers["X-Correlation-Id"] = correlationId;

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception. CorrelationId: {CorrelationId}", correlationId);

                if (!context.Response.HasStarted)
                {
                    await HandleExceptionAsync(context, ex, correlationId);
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, string correlationId)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode;
            string message;
            Dictionary<string, List<string>> errors = new();

            switch (ex)
            {
                case ValidationAppException validationEx:
                    statusCode = HttpStatusCode.BadRequest;
                    message = validationEx.Message;
                    errors = validationEx.Errors;
                    break;

                case NotFoundAppException:
                    statusCode = HttpStatusCode.NotFound;
                    message = ex.Message;
                    break;

                case BadRequestAppException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "Unauthorized access";
                    break;

                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;

                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;

                case DbUpdateException:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = ex.Message;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Internal Server Error";
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = ResponseHandler.Failure<string>(
                message: message,
                errors: errors,
                statusCode: (int)statusCode,
                correlationId: correlationId
            );

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}