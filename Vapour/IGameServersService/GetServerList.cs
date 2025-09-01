namespace Vapour.IGameServersService;

using Models;

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
                                    name = "Server",
                                    address = "127.0.0.1:27015",
                                    map = "map",
                                    players = 16,
                                    max_players = 32
                                }
                        }
                    }
                }
            }
        };
    };
}
