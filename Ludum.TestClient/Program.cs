using System.Net.Sockets;
using System.Net;
using System.Text;

int remotePort = 27015;

if (args.Length > 0)
{
    remotePort = Int32.Parse(args[0]);

    Console.WriteLine("Connecting to port {0}", remotePort);
}

IPAddress remoteServerAddr = IPAddress.Parse("127.0.0.1");
IPEndPoint remoteEndPoint = new IPEndPoint(remoteServerAddr, remotePort);

UdpClient udpClient = new UdpClient();
udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 11000));

// send a PlayerInfo request and get the response
udpClient.Send([0xFF, 0xFF, 0xFF, 0xFF, 0x55, 0xFF, 0xFF, 0xFF, 0xFF], remoteEndPoint);
byte[] playerInfoResponse = udpClient.Receive(ref remoteEndPoint);

Console.WriteLine(Encoding.ASCII.GetString(playerInfoResponse));
