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
        public ServerAction(IUserChatRoomAssistant userChatRoomAssistant, ISerializationProvider serializationProvider, ITransmitter transmitter)
        {
            _userChatRoomAssistantInstance = userChatRoomAssistant.GetInstance();
            _serializationProvider = serializationProvider;
            _transmitter = transmitter;
        }


        public string ResolveCommunicationToServer(TcpClient tcpClient, MessageActionType messageActionType, string username)
        {
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

        public void ResolveCommunicationFromServer(TcpClient tcpClient , ServerActionReportDelegate serverActionReportCallback)
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

        public ServerActionResolvedReport ResolveActionRequestedByServer(Payload payload)
        {
            ServerActionResolvedReport serverActionResolvedReport = new ServerActionResolvedReport();

            switch (payload.MessageActionType)
            {
                case MessageActionType.UserActivated:

                    User user = new User()
                    { 
                        Username = payload.ClientUsername,
                        UserID = (Guid)payload.UserGuid
                    };

                    _userChatRoomAssistantInstance.SetActiveUser(user);
                    ServerUser? userForRemoval = payload.ActiveServerUsers.Where(a => a.ServerUserID == user.UserID).FirstOrDefault();
                    if (userForRemoval != null) 
                    {
                        payload.ActiveServerUsers.Remove(userForRemoval);
                    }                    

                    serverActionResolvedReport.MessageActionType = payload.MessageActionType;
                    serverActionResolvedReport.AllActiveServerUsers = payload.ActiveServerUsers;
                    //add user to the chatroom assistant
                    //send list of server users to client manager
                    break;

                case MessageActionType.RetryUsernameTaken:

                    serverActionResolvedReport.MessageActionType = payload.MessageActionType;
                    break;
            }
            return serverActionResolvedReport;
        }
    }
}
