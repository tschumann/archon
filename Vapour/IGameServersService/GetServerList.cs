namespace Vapour.IGameServersService;

using Models;

/// <summary>
/// Undocumented by Valve but documented at https://developer.valvesoftware.com/wiki/Talk:Master_Server_Query_Protocol
/// </summary>
public class GetServerList
{
    public static Delegate Handler = (HttpContext httpContext) =>
    {
        return new Dictionary<string, Dictionary<string, List<Server>>>
        {
            {
                "response", new Dictionary<string, List<Server>>
                {
                    {
                        "servers", new List<Server>
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
                            }
                        }
                    }
                }
            }
        };
    };
}
