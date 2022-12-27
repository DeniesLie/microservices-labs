using System.Net;
using TransactionService.Exceptions;

namespace TransactionService.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception ex)
    {
        _logger.LogError($"Exception is catched in middleware. StatusCode: {statusCode}. Message: {ex.Message}. StackTrace: {ex.StackTrace}");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync($"Path: {context.Request.Path}. {ex.Message}");
    }
}