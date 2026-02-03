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
        // kind of hacky, but figure out whether to run this middleware based on attributes attached to the endpoints
        // ideally this would all be done with custom authorisation but ASP.Net Core doesn't allow much customisation
        // of authorisation - at least the error side of it - so we have to use a middleware to do the authorisation
        // to be able to return the error response that we want to
        // Steam also combines authorisation and authentication into a single step whereas ASP.Net core has it as two
        // separate steps
        if (context.GetEndpoint()?.Metadata.GetMetadata<AuthMetadata>() != null)
        {
            _logger.LogInformation("Got metadata for {0}", context.GetEndpoint()?.DisplayName);
        }
        else
        {
            await _next(context);

            return;
        }

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

public class AuthMetadata : Attribute
{
    public string MetadataValue { get; }

    // TODO: limit the possible values?
    public AuthMetadata(string metadataValue)
    {
        MetadataValue = metadataValue;
    }
}

public static class AuthMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthMiddleware>();
    }
}