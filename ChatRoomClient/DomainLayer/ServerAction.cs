using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;
using System.Net.Sockets;



namespace ChatRoomClient.DomainLayer
{
    public delegate void MessageFromServerDelegate(string messageFromServer);
    public class ServerAction : IServerAction
    {

        private string _currentUsername;
        private bool _ClientIsActive;
        private TcpClient _activeTcpClient;


        ISerializationProvider _serializationProvider;
        ITransmitter _transmitter;
        IObjectCreator _objectCreator;
       
        public ServerAction( ISerializationProvider serializationProvider, ITransmitter transmitter, IObjectCreator objectCreator)
        {
            _serializationProvider = serializationProvider;
            _transmitter = transmitter;
            _objectCreator = objectCreator;
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
                ExecuteDisconnectFromServer(serverCommunicationInfo.LogReportCallback, serverCommunicationInfo.ConnectionReportCallback);
                return;
            }

            serverCommunicationInfo.LogReportCallback(messageSent);
        }

        public void ExecuteDisconnectFromServer(ClientLogReportDelegate logReportCallback, ClientConnectionReportDelegate connectionReportCallback)
        {
            string log = string.Empty;
            try
            {
                _ClientIsActive = false;
                connectionReportCallback(_ClientIsActive);
                _activeTcpClient.Close();
                log = Notification.CRLF + "Disconnected from the server!";
                logReportCallback(log);
            }
            catch (Exception ex)
            {
                connectionReportCallback(_ClientIsActive);
                log = Notification.CRLF + Notification.Exception + "Problem disconnecting from the server..." + Notification.CRLF + ex.ToString();
                logReportCallback(log);
            }
        }


        public void ResolveCommunicationFromServer(ServerCommunicationInfo serverCommunicationInfo, ServerActionReportDelegate serverActionReportCallback)
        {
            void ProcessMessageFromServerCallback(string messageReceived)
            {
                ServerActionResolvedReport serverActionResolvedReport = new ServerActionResolvedReport();

                if (string.IsNullOrEmpty(messageReceived) || messageReceived.Contains(Notification.Exception))
                {
                    serverCommunicationInfo.LogReportCallback(messageReceived);
                    ExecuteDisconnectFromServer(serverCommunicationInfo.LogReportCallback, serverCommunicationInfo.ConnectionReportCallback);
                }
                else if (messageReceived.Contains(Notification.ServerMessage))
                {
                    serverActionResolvedReport.MessageFromServer = messageReceived;
                }
                else if (messageReceived.Contains(Notification.ServerPayload))
                {
                    string serializedPayload = messageReceived.Replace(Notification.ServerPayload, "");
                    Payload payloadFromServer = _serializationProvider.DeserializeObject<Payload>(serializedPayload);
                    serverActionResolvedReport = ResolveActionRequestedByServer(payloadFromServer);
                    serverActionResolvedReport.MessageFromServer = messageReceived;
                }
                serverActionReportCallback(serverActionResolvedReport);
            }

            MessageFromServerDelegate messageFromServerCallback = new MessageFromServerDelegate(ProcessMessageFromServerCallback);
            _transmitter.ReceiveMessageFromServer(_activeTcpClient , messageFromServerCallback);
        }


        private ServerActionResolvedReport ResolveActionRequestedByServer(Payload payload )
        {
            ServerActionResolvedReport serverActionResolvedReport = new ServerActionResolvedReport();

            switch (payload.MessageActionType)
            {
                case MessageActionType.RetryUsernameTaken:

                    serverActionResolvedReport.MessageActionType = payload.MessageActionType;
                    break;

                case MessageActionType.UserActivated:
                    ServerUser? userForActivation = payload.ActiveServerUsers.Where(a => a.Username.ToLower() == _currentUsername.ToLower()).FirstOrDefault();
                   
                    if (userForActivation != null) 
                    {                        
                        serverActionResolvedReport.MainUser = _objectCreator.CreateMainUser(userForActivation);
                        payload.ActiveServerUsers.Remove(userForActivation);
                    }                    

                    serverActionResolvedReport.MessageActionType = payload.MessageActionType;
                    serverActionResolvedReport.AllActiveServerUsers = payload.ActiveServerUsers;                   
                    break;

               
            }
            return serverActionResolvedReport;
        }

        #region Private Methods


        private string ResolveCommunicationToServer(Payload payload)
        {
            _currentUsername = payload.ClientUsername;
            string serializedPayload = _serializationProvider.SerializeObject(payload);
            string notificationMessage = _transmitter.SendMessageToServer(_activeTcpClient, serializedPayload);
            return notificationMessage;
        }


        
        #endregion Private Methods 
    }
}
