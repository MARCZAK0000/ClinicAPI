using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ApplicationClinicAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var problemDetails = new ProblemDetails()
                {
                    Title = "Internal Server Error",
                    Type = "Server Error",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = ex.Message
                };

                var json = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(json);
            }
        }
     
    }
}
