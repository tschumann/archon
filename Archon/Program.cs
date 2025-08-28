using Archon.MasterServers;
using Udp;

namespace Archon;

class Program
{
    public static void Main()
    {
        var server = new UdpServer(27010);
        server.Listen(new SourceMasterServer());
    }
}
