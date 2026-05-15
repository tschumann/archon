using Vapour.Models;

namespace Vapour.ISteamUserStats;

public abstract class BaseGetGlobalAchievementPercentagesForApp
{
    public static string MissingGameidErrorMessage = "<html><head><title>Bad Request</title></head><body><h1>Bad Request</h1>Required parameter 'gameid' is missing</body></html>";

    public static string BadRequestErrorMessage = "<html><head><title>Bad Request</title></head><body><h1>Bad Request</h1>Unknown problem determining WebApi request destination.</body></html>";

    /// <summary>
    /// Get achievements for a given appid or an empty or failure response. Not ideal but it reduces duplication by handling the
    /// identical empty and failure responses here and letting the controllers themselves handle the different non-empty responses.
    /// </summary>
    /// <param name="gameid"></param>
    /// <returns></returns>
    protected static (List<Achievement>? achievements, IResult? response) GetAchievementsOrResponse(string? gameid)
    {
        // if the gameid parameter is missing altogether, it's an error
        if (gameid == null)
        {
            return (null, Results.Content(MissingGameidErrorMessage, "text/html", statusCode: StatusCodes.Status400BadRequest));
        }

        int appid = 0;

        if (!Apps.IsValidAppId(gameid, out appid))
        {
            return (null, Results.Content(BadRequestErrorMessage, "text/html", statusCode: StatusCodes.Status400BadRequest));
        }

        if (!Apps.apps.ContainsKey(appid))
        {
            // the Dictionary's types depend on what API version is in use but it doesn't actually matter here as it's empty
            return (null, Results.Ok(new Dictionary<dynamic, dynamic>()));
        }

        List<Achievement>? achievements = Apps.apps[appid].achievements;

        if (achievements == null)
        {
            // the Dictionary's types depend on what API version is in use but it doesn't actually matter here as it's empty
            return (null, Results.Ok(new Dictionary<dynamic, dynamic>()));
        }
        else
        {
            return (achievements, null);
        }
    }
}
