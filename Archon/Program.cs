using Archon.MasterServers;
using UdpServer;

namespace Archon;

class Program
{
    public static void Main()
    {
        var server = new UdpListener(27010);
        server.Listen(new SourceMasterServer());
    }
}
