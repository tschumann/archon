using Vapour.Models;

namespace Vapour.ISteamUserStats;

/// <summary>
/// See https://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v1/?gameid=220
/// </summary>
public class GetGlobalAchievementPercentagesForAppV1
{
    public static string MissingGameidErrorMessage = "<html><head><title>Bad Request</title></head><body><h1>Bad Request</h1>Required parameter 'gameid' is missing</body></html>";

    public static string BadRequestErrorMessage = "<html><head><title>Bad Request</title></head><body><h1>Bad Request</h1>Unknown problem determining WebApi request destination.</body></html>";

    public readonly static Delegate Handler = (HttpContext httpContext, string? gameid) =>
    {
        // if the gameid parameter is missing altogether, it's an error
        if (gameid == null)
        {
            return Results.Content(MissingGameidErrorMessage, "text/html", statusCode: StatusCodes.Status400BadRequest);
        }

        int appid = 0;

        if (!Apps.IsValidAppId(gameid, out appid))
        {
            return Results.Content(BadRequestErrorMessage, "text/html", statusCode: StatusCodes.Status400BadRequest);
        }

        if (!Apps.apps.ContainsKey(appid))
        {
            return Results.Ok(new Dictionary<string, Dictionary<string, Dictionary<string, List<Achievement>>>>());
        }

        List<Achievement>? achievements = Apps.apps[appid].achievements;

        if (achievements == null)
        {
            return Results.Ok(new Dictionary<string, Dictionary<string, Dictionary<string, List<Achievement>>>>());
        }

        return Results.Ok(new Dictionary<string, Dictionary<string, Dictionary<string, List<Achievement>>>>
        {
            {
                "achievementpercentages", new Dictionary<string, Dictionary<string,  List<Achievement>>>
                {
                    {
                        "achievements", new Dictionary<string, List<Achievement>>
                        {
                            {
                                "achievement", achievements
                            }
                        }
                    }
                }
            }
        });
    };
}
