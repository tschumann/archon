namespace Vapour.Models.Internal;

public class APIParameter
{
    public required string name { get; set; }

    public required string type { get; set; }

    public required bool optional { get; set; }

    public required string description { get; set; }
}
