using Vapour.Models;

namespace Vapour.ISteamWebAPIUtil;

public class GetServerInfo
{
    public readonly static Delegate Handler = (HttpContext httpContext) =>
    {
        var now = DateTime.UtcNow;

        return Results.Ok(new ServerInfo
        {
            servertime = ((DateTimeOffset)now).ToUnixTimeSeconds(),
            servertimestring = now.ToString("ddd") + " " + now.ToString("MMM") + " " + now.Day + " " + now.ToString("HH:mm:ss") + " " + now.Year
        });
    };
}
