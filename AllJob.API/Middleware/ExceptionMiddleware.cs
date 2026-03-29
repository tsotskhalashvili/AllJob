using AllJob.API.Services.Interfaces;
using System.Text.Json;

namespace AllJob.API.Middleware;

public class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger,
    IExceptionResponseService exceptionResponseService,
    IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);

        }
        catch (Exception ex)
        {

            logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var response = exceptionResponseService
            .GetErrorResponse(ex, env.IsDevelopment());

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;

        var json = JsonSerializer.Serialize(response,
           new JsonSerializerOptions
           {
               PropertyNamingPolicy = JsonNamingPolicy.CamelCase
           });

        await context.Response.WriteAsync(json);
    }


}
