namespace Vapour;

using Vapour.IGameServersService;

public class Program
{
    public static void Main()
    {
        var app = WebApplication.Create();

        var gameServersService = app.MapGroup("/IGameServersService");

        gameServersService.MapGet("/GetServerList/v1/", GetServerList.Handler);

        app.UseAuthMiddleware();

        app.Run();
    }
}