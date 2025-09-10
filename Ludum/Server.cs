using UdpServer;

namespace Ludum;

class Server : IUdpRequestHandlerServer
{
    public byte[]? HandleRequest(byte[] request)
    {
        if (Valve.A2S.Query.IsA2SQuery(request))
        {
            if (Valve.A2S.Query.IsPlayerRequest(request))
            {
                return [0xFF, 0xFF, 0xFF, 0xFF, 0x44, 0x01, 0x00, 0x50, 0x6c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];
            }
        }

        return null;
    }
}
