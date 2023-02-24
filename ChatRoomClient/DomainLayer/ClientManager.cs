using ChatRoomClient.Utils.Interfaces;
using System.Net.Sockets;
using System.Net;
using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services;
using ChatRoomClient.Utils.Enumerations;

namespace ChatRoomClient.DomainLayer
{

    public delegate void ServerActionReportDelegate(Payload payload);
    internal class ClientManager : IClientManager
    {
        //Variables
        private IPAddress _serverIpAddress;
        private TcpClient _tcpClient;
        private bool _ClientIsActive = false;
        private string _currentUsername;

        IServerAction _serverAction;
        IUserChatRoomAssistant _userChatRoomAssistantInstance;
        IObjectCreator _objectCreator;
        public ClientManager(IServerAction serverAction, IUserChatRoomAssistant userChatRoomAssistant, IObjectCreator objectCreator)
        {
            _serverAction = serverAction;
            _userChatRoomAssistantInstance = userChatRoomAssistant.GetInstance();
            _objectCreator = objectCreator;
        }

        public void ConnectToServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            string log = string.Empty;
            try
            {
                _serverIpAddress = IPAddress.Parse(serverCommunicationInfo.IPAddress);
                log = Notification.CRLF + "Attempting to Connect to Server...";
                serverCommunicationInfo.LogReportCallback(log);
                _tcpClient = new TcpClient(_serverIpAddress.ToString(), serverCommunicationInfo.Port);

                Thread threadClientConnection = new Thread(() =>
                {
                    _ClientIsActive = true;                   
                    serverCommunicationInfo.ConnectionReportCallback(_ClientIsActive);
                    _serverAction.SetActiveTcpClient(_tcpClient);
                    log = Notification.CRLF + "Client connected to server Successfully.";
                    serverCommunicationInfo.LogReportCallback(log);
                    ProcessCommunicationSendMessageToServer(MessageActionType.ClientConnectToServer, serverCommunicationInfo);
                    ExecuteCommunicationReceiveMessageFromServer(serverCommunicationInfo);
                });
                threadClientConnection.IsBackground = true;
                threadClientConnection.Name = "BackgroundThreadConnection";
                threadClientConnection.Start();
            }
            catch (Exception ex)
            {
                _ClientIsActive = false;
                serverCommunicationInfo.ConnectionReportCallback(_ClientIsActive);
                log = Notification.CRLF + Notification.Exception + "Problem connecting to server... " + Notification.CRLF + ex.ToString();
                serverCommunicationInfo.LogReportCallback(log);
            }
        }

        public void SendMessageToServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            ProcessCommunicationSendMessageToServer(MessageActionType.CreateUser, serverCommunicationInfo);
        }

        public void DisconnectFromServer(ClientLogReportDelegate logReportCallback, ClientConnectionReportDelegate connectionReportCallback)
        {
            _serverAction.ExecuteDisconnectFromServer(logReportCallback, connectionReportCallback);
        }


        #region Private Methods

        private void ProcessCommunicationSendMessageToServer(MessageActionType messageActionType, ServerCommunicationInfo serverCommunicationInfo)
        {
            Payload payload = new Payload();
            switch (messageActionType)
            {
                case MessageActionType.ClientConnectToServer:
                case MessageActionType.CreateUser:
                    payload = _objectCreator.CreatePayload(messageActionType, serverCommunicationInfo.Username);
                    _currentUsername = serverCommunicationInfo.Username;
                    break;
                    
            }
           
            _serverAction.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);
        }

        private void ExecuteCommunicationReceiveMessageFromServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            void GetPayloadFromServerAction(Payload payload)
            {
                switch (payload.MessageActionType)
                {
                    case MessageActionType.RetryUsernameTaken:
                        serverCommunicationInfo.UsernameStatusReportCallback( payload.MessageActionType );
                        break;

                    case MessageActionType.UserActivated:
                        ServerUser? userForActivation = payload.ActiveServerUsers.Where(a => a.Username.ToLower() == _currentUsername.ToLower()).FirstOrDefault();

                        if (userForActivation != null)
                        {
                            SetActiveUserInUserChatAssistant(_objectCreator.CreateMainUser(userForActivation));
                            payload.ActiveServerUsers.Remove(userForActivation);
                        }

                        _userChatRoomAssistantInstance.SetAllActiveServerUsers(payload.ActiveServerUsers);
                        serverCommunicationInfo.UsernameStatusReportCallback(payload.MessageActionType);
                        serverCommunicationInfo.OtherServerUsersReportCallback(payload.ActiveServerUsers);
                        break;

                    case MessageActionType.ServerChatRoomCreated:
                        _userChatRoomAssistantInstance.AddChatRoomToAllActiveChatRooms( payload.ChatRoomCreated );
                        serverCommunicationInfo.UsernameStatusReportCallback( payload.MessageActionType );
                        serverCommunicationInfo.OtherServerUsersReportCallback( payload.ActiveServerUsers );
                        break;

                    case MessageActionType.ServerBroadcastMessageToChatRoom:

                        List<ChatRoom> allActiveChatRooms = _userChatRoomAssistantInstance.GetAllActiveChatRooms();
                        var targetChatRoom = allActiveChatRooms.Where(a=>a.ChatRoomId == payload.ChatRoomCreated.ChatRoomId).FirstOrDefault();
                        if(targetChatRoom != null)
                        {
                            _userChatRoomAssistantInstance.AddMessageToChatRoomConversation(targetChatRoom.ChatRoomId, payload.MessageToChatRoom);
                        }

                        serverCommunicationInfo.OtherServerUsersReportCallback(payload.ActiveServerUsers);
                        break;

                    case MessageActionType.ServerInviteSent:
                        break;
                    
                }
            }
           

            ServerActionReportDelegate serverActionReportCallback = new ServerActionReportDelegate(GetPayloadFromServerAction);

            Thread ThreadServerCommunication = new Thread(() =>
            {
                _serverAction.ResolveCommunicationFromServer(serverCommunicationInfo, serverActionReportCallback);
            });

            ThreadServerCommunication.IsBackground = true;
            ThreadServerCommunication.Name = "ThreadServerCommunication";
            ThreadServerCommunication.Start();
        }
           

        private void SetActiveUserInUserChatAssistant(IUser userForActivation)
        {
            IUser currentActiveMainUser = _userChatRoomAssistantInstance.GetActiveMainUser();
            if (currentActiveMainUser == null)
            {
                _userChatRoomAssistantInstance.SetActiveMainUser(userForActivation);
            }
        }

        #endregion Private Methods
    }
}
