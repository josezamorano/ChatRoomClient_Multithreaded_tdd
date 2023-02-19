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
        
        IUserChatRoomAssistant _userChatRoomAssistantInstance;
        ISerializationProvider _serializationProvider;
        ITransmitter _transmitter;
        private string _currentUsername;
        public ServerAction(IUserChatRoomAssistant userChatRoomAssistant, ISerializationProvider serializationProvider, ITransmitter transmitter)
        {
            _userChatRoomAssistantInstance = userChatRoomAssistant.GetInstance();
            _serializationProvider = serializationProvider;
            _transmitter = transmitter;
        }


        public string ResolveCommunicationToServer(TcpClient tcpClient, MessageActionType messageActionType, string username)
        {
            _currentUsername= username;

            Payload payload = new Payload()
            {
                MessageActionType = messageActionType,
                ClientUsername = username,
                UserGuid = null
            };
            string serializedPayload = _serializationProvider.SerializeObject(payload);
            string notificationMessage = _transmitter.SendMessageToServer(tcpClient, serializedPayload);
            return notificationMessage;
        }

        public void ResolveCommunicationFromServer(TcpClient tcpClient , ServerCommunicationInfo serverCommunicationInfo, ServerActionReportDelegate serverActionReportCallback)
        {            

            void ProcessMessageFromServerCallback(string messageReceived)
            {
                ServerActionResolvedReport serverActionResolvedReport = new ServerActionResolvedReport();

                if (string.IsNullOrEmpty(messageReceived) ||
                     messageReceived.Contains(Notification.Exception) ||
                     messageReceived.Contains(Notification.ServerMessage))
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

            _transmitter.ReceiveMessageFromServer(tcpClient , messageFromServerCallback);
        }

        public ServerActionResolvedReport ResolveActionRequestedByServer(Payload payload )
        {
            ServerActionResolvedReport serverActionResolvedReport = new ServerActionResolvedReport();

            switch (payload.MessageActionType)
            {
                case MessageActionType.UserActivated:
                    ServerUser? userForActivation = payload.ActiveServerUsers.Where(a => a.Username.ToLower() == _currentUsername.ToLower()).FirstOrDefault();
                   
                    if (userForActivation != null) 
                    {
                        SetActiveUserInUserChatAssistantInstance(userForActivation);
                        payload.ActiveServerUsers.Remove(userForActivation);
                    }                    

                    serverActionResolvedReport.MessageActionType = payload.MessageActionType;
                    serverActionResolvedReport.AllActiveServerUsers = payload.ActiveServerUsers;                   
                    break;

                case MessageActionType.RetryUsernameTaken:

                    serverActionResolvedReport.MessageActionType = payload.MessageActionType;
                    break;
            }
            return serverActionResolvedReport;
        }

        #region Private Methods
        private void SetActiveUserInUserChatAssistantInstance(ServerUser userForActivation)
        {
            if (userForActivation == null) { return; }
            
            IUser currentActiveUser = _userChatRoomAssistantInstance.GetActiveUser();
            if (currentActiveUser == null)
            {
                User activeUser = new User()
                {
                    Username = userForActivation.Username,
                    UserID = (Guid)userForActivation.ServerUserID
                };
                _userChatRoomAssistantInstance.SetActiveUser(activeUser);
            }            
        }
        #endregion Private Methods 
    }
}
