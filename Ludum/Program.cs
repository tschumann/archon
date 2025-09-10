using UdpServer;

namespace Ludum;

class Program
{
    public static void Main()
    {
        var server = new UdpListener(27015);
        server.Listen(new Server());
    }
}