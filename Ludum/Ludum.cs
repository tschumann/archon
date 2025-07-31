using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Ludum;

class Ludum
{
    private static void StartListener()
    {
        UdpClient listener = new UdpClient(27015);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 27015);

        try
        {
            while (true)
            {
                Console.WriteLine("Waiting for broadcast");
                byte[] bytes = listener.Receive(ref groupEP);

                Console.WriteLine($"Received broadcast from {groupEP} :");
                Console.WriteLine($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");

                if (Valve.A2S.Query.IsA2SQuery(bytes))
                {
                    if (Valve.A2S.Query.IsPlayerRequest(bytes))
                    {
                        Console.WriteLine("Responding to player info request");
                        listener.Send([0xFF, 0xFF, 0xFF, 0xFF, 0x44, 0x01, 0x00, 0x50, 0x6c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00], groupEP);
                    }
                }
                else
                {
                    Console.WriteLine("Unrecognised request");
                }
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            listener.Close();
        }
    }

    public static void Main()
    {
        StartListener();
    }
}