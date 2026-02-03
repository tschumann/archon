namespace Vapour.ISteamUserStats;

public class GetNumberOfCurrentPlayers
{
    public static string MissingAppidErrorMessage = "<html><head><title>Bad Request</title></head><body><h1>Bad Request</h1>Required parameter 'appid' is missing</body></html>";

    public readonly static Delegate Handler = (HttpContext httpContext) =>
    {
        // if the appid parameter is missing altogether, it's an error
        if (!httpContext.Request.Query.ContainsKey("appid"))
        {
            return Results.Content(MissingAppidErrorMessage, "text/html", statusCode: StatusCodes.Status400BadRequest);
        }

        var appid = httpContext.Request.Query["appid"];

        // TODO: if the appid parameter is specified but invalid in any way, it just seems to return the Steam-wide total player count rather than for the current game

        return Results.Ok(new Dictionary<string, Dictionary<string, int>>
        {
            {
                "response", new Dictionary<string, int>
                {
                    {
                        "player_count", 1
                    },
                    {
                        "result", 1
                    }
                }
            }
        });
    };
}
