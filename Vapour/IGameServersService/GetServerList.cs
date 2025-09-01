namespace Vapour.IGameServersService;

public class GetServerList
{
    public static Delegate Handler = (HttpContext httpContext) =>
    {
        return new Models.Server()
        {
            Name = "Server",
            Address = "127.0.0.1:27015"
        };
    };
}
