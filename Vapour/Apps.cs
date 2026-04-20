using Vapour.Models;

namespace Vapour;

public class App
{
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

    public App Build() => app;
}

public class Apps
{
    public static Dictionary<int, App> apps = new Dictionary<int, App>
    {
        {
            70,
            new AppBuilder()
                .WithAppid(70)
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
