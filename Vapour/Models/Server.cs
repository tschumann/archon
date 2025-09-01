namespace Models;

class Server
{
    public required string name { get; set; }

    public required string address { get; set; }

    public required string map { get; set; }

    public required int players { get; set; }

    public required int max_players { get; set; }
}