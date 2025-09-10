namespace Vapour.Models;

class GameServer
{
    // TODO: is it addr or address?
    public required string address { get; set; }

    public required int gameport { get; set; }

    public required string steamid { get; set; }

    public required string name { get; set; }

    public required uint appid { get; set; }

    public required string gamedir { get; set; }

    public required string version { get; set; }

    public required string product { get; set; }

    public required uint region { get; set; }

    public required uint players { get; set; }

    public required uint max_players { get; set; }

    public required uint bots { get; set; }

    public required string map { get; set; }

    public required bool secure { get; set; }

    public required bool dedicated { get; set; }

    public required string os { get; set; }

    public required string gametype { get; set; }
}