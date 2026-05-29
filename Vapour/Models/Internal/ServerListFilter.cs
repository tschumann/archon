namespace Vapour.Models.Internal;

public class ServerListFilter
{
    public int? appid { get; set; }

    public override string ToString()
    {
        string output = string.Empty;

        if (appid != null)
        {
            output += "appid=" + appid + " ";
        }

        return output.Length > 1 ? output[..^1] : "";
    }
}