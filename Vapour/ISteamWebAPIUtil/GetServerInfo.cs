using Vapour.Models.Responses;

namespace Vapour.ISteamWebAPIUtil;

/// <summary>
/// See https://api.steampowered.com/ISteamWebAPIUtil/GetServerInfo/v0001/
/// </summary>
public class GetServerInfo
{
    public readonly static Delegate Handler = (HttpContext httpContext) =>
    {
        var now = DateTime.UtcNow;
        // if the day is less than the 10th, an extra space is added so that servertimestring has a fixed width
        var day = now.Day < 10 ? " " + now.Day.ToString() : now.Day.ToString();

        return Results.Ok(new ServerInfoResponse
        {
            servertime = ((DateTimeOffset)now).ToUnixTimeSeconds(),
            servertimestring = now.ToString("ddd") + " " + now.ToString("MMM") + " " + day + " " + now.ToString("HH:mm:ss") + " " + now.Year
        });
    };
}
