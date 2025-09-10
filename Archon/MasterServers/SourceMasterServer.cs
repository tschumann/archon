using UdpServer;

namespace Archon.MasterServers;

public class SourceMasterServer : IUdpRequestHandlerServer
{
    public byte[]? HandleRequest(byte[] request)
    {
        if (IsServerQuery(request))
        {
            return [0xFF, 0xFF, 0xFF, 0xFF, 0x66, 0x0A, 0x01, 0x02, 0x06, 0x08, 0x69, 0x87, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];
        }

        return null;
    }

    public static bool IsServerQuery(byte[] request)
    {
        // TODO: check the port and filter stuff too
        return request != null && request.Length >= 13 && request[0] == 0x31 && IsValidRegionCode(request[1]);
    }

    public static bool IsValidRegionCode(byte regionCode)
    {
        return regionCode == 0x00 || regionCode == 0x01 || regionCode == 0x02 || regionCode == 0x03 || regionCode == 0x04 || regionCode == 0x05 || regionCode == 0x06 || regionCode == 0x07 || regionCode == 0xFF;
    }
}

