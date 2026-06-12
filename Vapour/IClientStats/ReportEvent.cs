using Vapour.Models;

namespace Vapour.IClientStats;

/// <summary>
/// See https://api.steampowered.com/IClientStats_1046930/ReportEvent/v1/
/// </summary>
public class ReportEvent
{
    public readonly static Delegate Handler = (HttpContext httpContext) =>
    {
        // TODO: what does a successful request look like?

        return Results.BadRequest(new ReportEventResponse
        {
            success = false,
            message = ""
        });
    };
}
