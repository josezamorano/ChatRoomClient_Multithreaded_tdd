using System.Net.Sockets;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface ITransmitter
    {
        string SendMessageToServer(TcpClient tcpClient, string payloadAsMessageLine);

        string ReceiveMessageFromServer(TcpClient tcpClient);
    }
}
