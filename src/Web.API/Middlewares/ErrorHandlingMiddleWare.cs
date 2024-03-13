namespace Web.API.Middlewares;
internal sealed class ErrorHandlingMiddleWare : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleWare> _logger;
    
    public ErrorHandlingMiddleWare(ILogger<ErrorHandlingMiddleWare> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception occurred: {Message}", e.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(e);
        }
    }
}
