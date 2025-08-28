using Microsoft.Extensions.Primitives;

namespace Vapour;

public class AuthMiddleware
{
    public static string UnathenticatedErrorMessage = "<html><head><title>Forbidden</title></head><body><h1>Forbidden</h1>Access is denied. Retrying will not help. Please verify your <pre>key=</pre> parameter.</body></html>";

    private readonly RequestDelegate _next;

    private readonly ILogger _logger;

    public AuthMiddleware(RequestDelegate next, ILoggerFactory logger)
    {
        _next = next;
        _logger = logger.CreateLogger("Vapour.AuthMiddleware");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var key = context.Request.Query["key"];

        if (StringValues.IsNullOrEmpty(key))
        {
            context.Response.ContentType = "text/html";
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            // this is what gets returned by curl https://api.steampowered.com/IGameServersService/GetServerList/v1/
            await context.Response.WriteAsync(UnathenticatedErrorMessage);

            return;
        }
        else
        {
            _logger.LogInformation("Got key {0}; accepting", key.ToString());
        }

        await _next(context);
    }
}

public static class AuthMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthMiddleware>();
    }
}