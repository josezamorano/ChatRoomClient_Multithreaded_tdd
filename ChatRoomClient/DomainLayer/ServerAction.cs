using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services;
using ChatRoomClient.Utils.Interfaces;
using System.Net.Sockets;



namespace ChatRoomClient.DomainLayer
{
    public delegate void MessageFromServerDelegate(string messageFromServer);
    public class ServerAction : IServerAction
    {

        private bool _ClientIsActive;
        private TcpClient _activeTcpClient;

        ISerializationProvider _serializationProvider;
        ITransmitter _transmitter;
       
        public ServerAction( ISerializationProvider serializationProvider, ITransmitter transmitter)
        {
            _serializationProvider = serializationProvider;
            _transmitter = transmitter;
        }

        public void SetActiveTcpClient(TcpClient activeTcpClient)
        {
            _activeTcpClient = activeTcpClient;
        }

        public void ExecuteCommunicationSendMessageToServer(Payload payload, ServerCommunicationInfo serverCommunicationInfo)
        {
            string messageSent = ResolveCommunicationToServer(payload);
            if (messageSent.Contains(Notification.Exception))
            {
                serverCommunicationInfo.LogReportCallback(messageSent);
                ExecuteDisconnectFromServer(serverCommunicationInfo );
                return;
            }

            serverCommunicationInfo.LogReportCallback(messageSent);
        }

        public void ExecuteDisconnectFromServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            try
            {
                _ClientIsActive = false;
                serverCommunicationInfo.ConnectionReportCallback(_ClientIsActive);
                _activeTcpClient.Close();
                string log = Notification.CRLF + "Disconnected from the server!";
                serverCommunicationInfo.LogReportCallback(log);
            }
            catch (Exception ex)
            {
                serverCommunicationInfo.ConnectionReportCallback(_ClientIsActive);
                string log = Notification.CRLF + Notification.Exception + "Problem disconnecting from the server..." + Notification.CRLF + ex.ToString();
                serverCommunicationInfo.LogReportCallback(log);
            }
        }

        public void ResolveCommunicationFromServer(ServerCommunicationInfo serverCommunicationInfo, ServerActionReportDelegate serverActionReportCallback)
        {
            void ProcessMessageFromServerCallback(string messageReceived)
            {
                bool messageIsInvalid = VerifyIfMessageIsNullOrContainsException(messageReceived, serverCommunicationInfo , serverActionReportCallback);
                if (messageIsInvalid) { return; }

                if (messageReceived.Contains(Notification.ServerPayload))
                {
                    string serializedPayload = messageReceived.Replace(Notification.ServerPayload, "");
                    Payload payload = _serializationProvider.DeserializeObject<Payload>(serializedPayload);
                    serverActionReportCallback(payload);
                }
            }

            MessageFromServerDelegate messageFromServerCallback = new MessageFromServerDelegate(ProcessMessageFromServerCallback);
            _transmitter.ReceiveMessageFromServer(_activeTcpClient , messageFromServerCallback);
        }


        #region Private Methods
        private bool VerifyIfMessageIsNullOrContainsException(string message, ServerCommunicationInfo serverCommunicationInfo , ServerActionReportDelegate serverActionReportCallback)
        {
            if (string.IsNullOrEmpty(message) || message.Contains(Notification.Exception))
            {                
                serverCommunicationInfo.LogReportCallback(message);
                Payload exceptionPayload = new Payload();
                exceptionPayload.MessageActionType = Utils.Enumerations.MessageActionType.ServerClientDisconnectAccepted;
                serverActionReportCallback(exceptionPayload);
                return true;
            }

            return false;
        }             

        private string ResolveCommunicationToServer(Payload payload)
        {
            string serializedPayload = _serializationProvider.SerializeObject(payload);
            string notificationMessage = _transmitter.SendMessageToServer(_activeTcpClient, serializedPayload);
            return notificationMessage;
        }
        
        #endregion Private Methods 
    }
}
