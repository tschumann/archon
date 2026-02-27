using Vapour.IGameServersService;
using Vapour.ISteamUserStats;
using Vapour.ISteamWebAPIUtil;

namespace Vapour;

public class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        var app = builder.Build();

        var gameServersService = app.MapGroup("/IGameServersService");

        gameServersService.MapGet("/GetServerList/v1/", GetServerList.Handler);

        var steamUserStats = app.MapGroup("/ISteamUserStats");

        steamUserStats.MapGet("/GetNumberOfCurrentPlayers/v1", GetNumberOfCurrentPlayers.Handler);

        var steamWebAPIUtil = app.MapGroup("/ISteamWebAPIUtil");

        steamWebAPIUtil.MapGet("/GetServerInfo/v0001", GetServerInfo.Handler);
        steamWebAPIUtil.MapGet("/GetSupportedAPIList/v0001", GetSupportedAPIList.Handler);

        app.UseAuthMiddleware();

        app.Run();
    }
}