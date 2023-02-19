using ChatRoomClient.Services;
using ChatRoomClient.Utils.Interfaces;
using System.Net.Sockets;

namespace ChatRoomClient.DomainLayer
{
    public class Transmitter : ITransmitter
    {
        IStreamProvider _streamProvider;

        public Transmitter(IStreamProvider streamProvider)
        {
            _streamProvider = streamProvider;
        }

        public string SendMessageToServer(TcpClient tcpClient, string payloadAsMessageLine)
        {
            try
            {
                if (tcpClient.Connected)
                {
                    string messageLine = Notification.ClientPayload + payloadAsMessageLine;
                    StreamWriter streamWriter = _streamProvider.CreateStreamWriter(tcpClient.GetStream());
                    streamWriter.WriteLine(messageLine);
                    streamWriter.Flush();
                    return Notification.MessageSentOk;
                }
                return Notification.CRLF + "ERROR. Tcp client Disconnected from server. Message Not sent";
            }
            catch(Exception ex)
            {
                string log = Notification.CRLF + Notification.Exception + "Problem Sending message to the server..." + Notification.CRLF + ex.ToString();
                return log;
            }
        }

        public void ReceiveMessageFromServer(TcpClient tcpClient, MessageFromServerDelegate messageFromServerCallback)
        {
            try
            {
                if (tcpClient == null) { return;}

                StreamReader reader = _streamProvider.CreateStreamReader(tcpClient.GetStream());
                while (tcpClient.Connected)
                {
                    string message = reader.ReadLine(); // block here until we receive something from the server.
                    messageFromServerCallback(message);
                    if (message == null)
                    {
                        break;
                    }
                }               
            }
            catch(Exception ex)
            {
                string log = Notification.CRLF + Notification.Exception + "Problem Receiving message from the server..." + Notification.CRLF + ex.ToString();
                messageFromServerCallback(log);
            }
        }
    }
}
