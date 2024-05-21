using Application.Exceptions;
using EduFlex.API.Enums;
using EduFlex.API.Models;
using System.Net;
using System.Text.Json;

namespace EduFlex.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception => {ex.Message}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        switch (exception) 
        {
            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case BadRequestException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var errorDetails = new ErrorDetails 
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message
        };

        var responseModel = new ResponseModel(Status.Error, "Exception", errorDetails);

        await context.Response.WriteAsync(JsonSerializer.Serialize(responseModel));
    }
}