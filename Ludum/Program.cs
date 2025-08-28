using Udp;

namespace Ludum;

class Program
{
    public static void Main()
    {
        var server = new UdpServer(27015);
        server.Listen(new Server());
    }
}