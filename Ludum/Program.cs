using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Ludum;

class Program
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