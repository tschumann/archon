using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Archon;

class Archon
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

                if (bytes.Length >= 5 && bytes[0] == 0xFF && bytes[1] == 0xFF && bytes[2] == 0xFF && bytes[3] == 0xFF)
                {
                    if (bytes.Length == 9 && bytes[4] == 0x55 && bytes[5] == 0xFF && bytes[6] == 0xFF && bytes[7] == 0xFF && bytes[8] == 0xFF)
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