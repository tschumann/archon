using Vapour.Models;

namespace Vapour.ISteamUserStats;

/// <summary>
/// See https://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v2/?gameid=220
/// </summary>
public class GetGlobalAchievementPercentagesForAppV2 : BaseGetGlobalAchievementPercentagesForApp
{
    public readonly static Delegate Handler = (HttpContext httpContext, string? gameid) =>
    {
        (List<Achievement>? achievements, IResult? response) = GetAchievementsOrResponse(gameid);

        if (achievements == null)
        {
            return response;
        }
        else
        {
            return Results.Ok(new Dictionary<string, Dictionary<string, List<Achievement>>>
            {
                {
                    "achievementpercentages", new Dictionary<string, List<Achievement>>
                    {
                        {
                            "achievements", achievements
                        }
                    }
                }
            });
        }
    };
}
