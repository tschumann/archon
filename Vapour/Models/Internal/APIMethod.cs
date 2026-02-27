namespace Vapour.Models.Internal;

public class APIMethod
{
    public required string name { get; set; }

    public required int version { get; set; }

    public required string httpmethod { get; set; }

    public required APIParameter[] parameters { get; set; }
}
