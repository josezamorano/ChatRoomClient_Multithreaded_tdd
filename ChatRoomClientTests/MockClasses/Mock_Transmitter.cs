

using ChatRoomClient.DomainLayer;
using ChatRoomClient.Services;
using ChatRoomClient.Utils.Interfaces;
using System.Net.Sockets;

namespace ChatRoomClientTests.MockClasses
{
    public class Mock_Transmitter : ITransmitter
    {
        public void ReceiveMessageFromServer(TcpClient tcpClient, MessageFromServerDelegate messageFromServerCallback)
        {
            throw new NotImplementedException();
        }

        public string SendMessageToServer(TcpClient tcpClient, string payloadAsMessageLine)
        {
            return Notification.MessageSentOk;
        }
    }
}
