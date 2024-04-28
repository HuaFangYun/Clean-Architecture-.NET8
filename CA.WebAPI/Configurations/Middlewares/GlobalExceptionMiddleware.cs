using CA.CrossCuttingConcerns.Constants;
using CA.CrossCuttingConcerns.Exceptions;
using System.Net;
using System.Text.Json;
using ValidationException = CA.CrossCuttingConcerns.Exceptions.ValidationException;

namespace CA.WebAPI.Configurations.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var response = httpContext.Response;
                response.ContentType = ConfigurationConstants.DefaultContentType;

                switch (ex)
                {
                    case ValidationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case NotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        _logger.LogError(ex, $"{DateTimeOffset.Now.Ticks}-{Environment.CurrentManagedThreadId}-{ex.Message}");
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new
                {
                    IsError = true,
                    Message = ex.Message,
                    MessageDetail = ex.InnerException?.Message
                });

                await response.WriteAsync(result);
            }
        }
    }
}
