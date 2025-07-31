namespace Ludum.Valve.A2S;

public class Query
{
    public static bool IsA2SQuery(byte[] request)
    {
        return request != null && request.Length >= 5 && request[0] == 0xFF && request[1] == 0xFF && request[2] == 0xFF && request[3] == 0xFF;
    }

    public static bool IsInfoRequest(byte[] request)
    {
        // Source Engine Query is 19 characters (not including NULL terminator)
        return request.Length >= 25 && request[4] == 0x54 && System.Text.Encoding.ASCII.GetString(request, 5, "Source Engine Query".Length) == "Source Engine Query";
    }

    public static bool IsPlayerRequest(byte[] request)
    {
        return request.Length == 9 && request[4] == 0x55 && request[5] == 0xFF && request[6] == 0xFF && request[7] == 0xFF && request[8] == 0xFF;
    }

    public static bool IsRulesRequest(byte[] request)
    {
        return request.Length == 9 && request[4] == 0x56 && request[5] == 0xFF && request[6] == 0xFF && request[7] == 0xFF && request[8] == 0xFF;
    }

    public static bool IsPingRequest(byte[] request)
    {
        return request.Length == 5 && request[4] == 0x69;
    }

    public static bool IsServerQueryGetChallengeRequest(byte[] request)
    {
        return request.Length == 5 && request[4] == 0x57;
    }
}

