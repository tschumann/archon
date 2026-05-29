using Microsoft.Extensions.Primitives;
using Vapour.Models;
using Vapour.Models.Internal;

namespace Vapour.IGameServersService;

/// <summary>
/// See https://api.steampowered.com/IGameServersService/GetServerList/v1/?key=foo&filter=appid\70
/// Undocumented by Valve but documented at https://developer.valvesoftware.com/wiki/Talk:Master_Server_Query_Protocol
/// </summary>
public class GetServerList
{
    public readonly static Delegate Handler = [AuthMetadata("unused")] (HttpContext httpContext) =>
    {
        var filter = httpContext.Request.Query["filter"];
        ServerListFilter? filters = null;

        if (!StringValues.IsNullOrEmpty(filter))
        {
            filters = new ServerListFilter();
            // the filters are stored as attr1\val1\attr2\val2 etc
            var filterValues = filter.ToString().Split("\\");

            // TODO: how is validation handled?
            for (var i = 0; i < filterValues.Length; i++)
            {
                // every even-numbered entry is a filter attribute and every odd-numbered entry is the filter value
                if (i % 2 == 0 && filterValues[i] == "appid")
                {
                    filters.appid = Convert.ToInt32(filterValues[i + 1]);
                }
            }

            Console.WriteLine("Using filters {0}", filters);
        }

        var servers = Apps.apps.SelectMany(app => (app.Value.servers != null) ? app.Value.servers : []).ToList();

        if (filters?.appid != null)
        {
            int filterAppid = (int)filters?.appid;

            if (Apps.apps.ContainsKey(filterAppid))
            {
                servers = Apps.apps.Where(app => app.Key == filterAppid).SelectMany(app => (app.Value.servers != null) ? app.Value.servers : []).ToList();
            }
            else
            {
                servers = [];
            }
        }

        return new Dictionary<string, Dictionary<string, List<GameServer>>>
        {
            {
                "response", new Dictionary<string, List<GameServer>>
                {
                    {
                        "servers", servers
                    }
                }
            }
        };
    };
}
