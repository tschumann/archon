namespace Vapour;

class Vapour
{
    public static void Main()
    {
        var app = WebApplication.Create();

        var gameServersService = app.MapGroup("/IGameServersService");

        gameServersService.MapGet("/GetServerList/v1/", () =>
        {
            return new Models.Server()
            {
                Name = "Server",
                Address = "127.0.0.1:27015"
            };
        });

        app.Run();
    }
}