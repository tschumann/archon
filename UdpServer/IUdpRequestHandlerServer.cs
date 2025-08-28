namespace Udp;

public interface IUdpRequestHandlerServer
{
    public byte[]? HandleRequest(byte[] request);
}
