using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Udp;

public class UdpServer
{
    private readonly int _port;

    private readonly UdpClient _listener;

    private IPEndPoint _endpoint;

    public UdpServer(int port)
    {
        _port = port;

        _listener = new UdpClient(_port);
        _endpoint = new IPEndPoint(IPAddress.Any, _port);
    }

    public void Listen(IUdpRequestHandlerServer handler)
    {
        Console.WriteLine("Listening on port {0}", _port);

        try
        {
            while (true)
            {
                byte[] bytes = _listener.Receive(ref _endpoint);

                Console.WriteLine($"Received from {_endpoint}: {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");

                byte[]? response = handler.HandleRequest(bytes);

                if (response != null)
                {
                    _listener.Send(response, _endpoint);
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
            _listener.Close();
        }
    }
}
