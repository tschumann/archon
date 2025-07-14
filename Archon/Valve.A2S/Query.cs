namespace Archon.Valve.A2S;

public class Query
{
    public static bool IsA2SQuery(byte[] request)
    {
        return request != null && request.Length >= 5 && request[0] == 0xFF && request[1] == 0xFF && request[2] == 0xFF && request[3] == 0xFF;
    }

    public static bool IsPlayerInfoRequest(byte[] request)
    {
        return request.Length == 9 && request[4] == 0x55 && request[5] == 0xFF && request[6] == 0xFF && request[7] == 0xFF && request[8] == 0xFF;
    }
}

