using CardActionsService.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CardActionsService.Api.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception,
                "Unhandled exception occurred. Path: {Path}, Method: {Method}, User: {User}, TraceId: {TraceId}",
                httpContext.Request.Path,
                httpContext.Request.Method,
                httpContext.User?.Identity?.Name ?? "Anonymous",
                httpContext.TraceIdentifier);

            var problemDetails = CreateProblemDetails(httpContext, exception);

            httpContext.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private ProblemDetails CreateProblemDetails(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var problemDetails = new ProblemDetails
            {
                Title = GetProblemTitle(exception),
                Status = statusCode,
                Detail = GetErrorDetails(exception),
                Instance = httpContext.Request.Path
            };
            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
            problemDetails.Extensions["timestamp"] = DateTime.UtcNow;

            if (_environment.IsDevelopment())
            {
                problemDetails.Extensions["exceptionType"] = exception.GetType().Name;
                problemDetails.Extensions["stackTrace"] = exception.StackTrace;
            }

            return problemDetails;
        }

        private int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                TimeoutException => (int)HttpStatusCode.RequestTimeout,
                CardNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError,
            };
        }

        private string GetProblemTitle(Exception exception)
        {
            return exception switch
            {
                ArgumentNullException => "Bad Request",
                ArgumentException => "Bad Request",
                TimeoutException => "Request Timeout",
                CardNotFoundException => "Not Found",
                _ => "Internal Server Error"
            };
        }

        private string GetErrorDetails(Exception exception)
        {
            if (_environment.IsDevelopment())
            {
                return exception.Message;
            }

            return exception switch
            {
                ArgumentNullException => "A required parameter was not provided",
                ArgumentException => "A provided parameter was not correct",
                TimeoutException => "The request took too long to process",
                CardNotFoundException => "The specified card could not be found.",
                _ => "An unexpected error occurred. Please contact support if the issue persists"
            };
        }
    }
}
