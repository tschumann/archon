using Microsoft.Extensions.Primitives;
using Vapour.Models;
using Vapour.Models.Internal;

namespace Vapour.IGameServersService;

/// <summary>
/// Undocumented by Valve but documented at https://developer.valvesoftware.com/wiki/Talk:Master_Server_Query_Protocol
/// </summary>
public class GetServerList
{
    private static readonly List<GameServer> _servers =
    [
        new GameServer()
        {
            address = "127.0.0.1:27015",
            gameport = 27015,
            steamid = "123",
            name = "Server",
            appid = 70,
            gamedir = "valve",
            version = "1.0.0.0",
            product = "Half-Life",
            region = 255,
            players = 16,
            max_players = 32,
            bots = 0,
            map = "map",
            secure = true,
            dedicated = true,
            os = "l",
            gametype = "deathmatch"
        },
        new GameServer()
        {
            address = "127.0.0.1:27015",
            gameport = 27015,
            steamid = "123",
            name = "Server",
            appid = 50,
            gamedir = "gearbox",
            version = "1.0.0.0",
            product = "Half-Life: Opposing Force",
            region = 255,
            players = 16,
            max_players = 32,
            bots = 0,
            map = "of",
            secure = true,
            dedicated = true,
            os = "l",
            gametype = "deathmatch"
        }
    ];

    public readonly static Delegate Handler = (HttpContext httpContext) =>
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
                    filters.appid = Convert.ToUInt32(filterValues[i + 1]);
                }
            }

            Console.WriteLine("Using filters {0}", filters);
        }

        var servers = _servers;

        if (filters?.appid != null)
        {
            servers = servers.Where(server => server.appid == filters.appid).ToList();
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
