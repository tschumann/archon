using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Ludum;

class Program
{
    private static void StartListener()
    {
        int port = 27015;

        UdpClient listener = new UdpClient(port);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, port);

        try
        {
            Console.WriteLine("Archon waiting for broadcast on port {0}", port);

            while (true)
            {
                byte[] bytes = listener.Receive(ref groupEP);

                Console.WriteLine($"Received broadcast from {groupEP} :");
                Console.WriteLine($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");

                byte[]? response = Server.HandleRequest(bytes);

                if (response != null)
                {
                    listener.Send(response, groupEP);
                }
                else
                {
                    Console.WriteLine("Unhandled request");
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