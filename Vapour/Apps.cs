using Vapour.Models;

namespace Vapour;

public class App
{
    public List<GameServer>? servers { get; set; } = null;

    public List<Achievement>? achievements { get; set; } = null;

    public int appid { get; set; }
}

public class AppBuilder
{
    private readonly App app = new App();

    public AppBuilder WithAchievements(List<Achievement> achievements)
    {
        app.achievements = achievements;
        return this;
    }

    public AppBuilder WithAppid(int appid)
    {
        app.appid = appid;
        return this;
    }

    public AppBuilder WithServers(List<GameServer> servers)
    {
        app.servers = servers;
        return this;
    }

    public App Build() => app;
}

public class Apps
{
    public static Dictionary<int, App> apps = new Dictionary<int, App>
    {
        {
            50,
            new AppBuilder()
                .WithAppid(50)
                .WithServers(new List<GameServer>
                {
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
                })
            .Build()
        },
        {
            70,
            new AppBuilder()
                .WithAppid(70)
                .WithServers(new List<GameServer>
                {
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
                    }
                })
            .Build()
        },
        {
            220,
            new AppBuilder()
                .WithAppid(220)
                .WithAchievements(new List<Achievement>
                {
                    new Achievement()
                    {
                        name = "HL2_ESCAPE_APARTMENTRAID",
                        percent = "70.4"
                    }
                })
                .Build()
        }
    };

    public static bool IsValidAppId(string appId, out int parsed)
    {
        try
        {
            parsed = int.Parse(appId);
        }
        catch (FormatException)
        {
            // need to set parsed to something...
            parsed = 0;

            return false;
        }

        return true;
    }
}
