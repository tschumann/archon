namespace Vapour.IGameServersService;

using Microsoft.Extensions.Primitives;
using Models;

/// <summary>
/// Undocumented by Valve but documented at https://developer.valvesoftware.com/wiki/Talk:Master_Server_Query_Protocol
/// </summary>
public class GetServerList
{
    private static List<Server> _servers = new List<Server>
    {
        new Server()
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
        new Server()
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
    };

    public static Delegate Handler = (HttpContext httpContext) =>
    {
        var filter = httpContext.Request.Query["filter"];
        Dictionary<string, string> filters = null;

        if (!StringValues.IsNullOrEmpty(filter))
        {
            filters = new Dictionary<string, string>();
            var filterValues = filter.ToString().Split("\\");

            // TODO: add some validation
            for (var i = 0; i < filterValues.Length; i++)
            {
                if (i % 2 == 0)
                {
                    filters.Add(filterValues[i], filterValues[i + 1]);
                }
            }

            Console.WriteLine("Using filters {0}", String.Join(Environment.NewLine, filters));
        }

        var servers = _servers;

        if (filters != null)
        {
            if (filters.ContainsKey("appid"))
            {
                servers = servers.Where(server => server.appid == Convert.ToInt32(filters["appid"])).ToList();
            }
        }

        return new Dictionary<string, Dictionary<string, List<Server>>>
        {
            {
                "response", new Dictionary<string, List<Server>>
                {
                    {
                        "servers", servers
                    }
                }
            }
        };
    };
}
