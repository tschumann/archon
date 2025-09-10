namespace UdpServer;

public interface IUdpRequestHandlerServer
{
    public byte[]? HandleRequest(byte[] request);
}
