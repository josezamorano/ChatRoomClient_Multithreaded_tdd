using ChatRoomClient.Utils.Interfaces;
using System.Net.Sockets;
using System.Net;
using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services;
using ChatRoomClient.Utils.Enumerations;
using Microsoft.VisualBasic.Logging;

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
        IUser _mainUser;
        IUserChatRoomAssistant _userChatRoomAssistantInstance;
        IObjectCreator _objectCreator;
        public ClientManager(IServerAction serverAction, IUser mainUser, IUserChatRoomAssistant userChatRoomAssistant, IObjectCreator objectCreator)
        {
            _serverAction = serverAction;
            _mainUser = mainUser;
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

        public void DisconnectFromServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            ProcessCommunicationSendMessageToServer(MessageActionType.ClientDisconnect, serverCommunicationInfo);
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

                case MessageActionType.ClientDisconnect:
                    payload = _objectCreator.CreatePayload(messageActionType, _mainUser.Username, _mainUser.UserID);
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
                            _mainUser.UserID = (Guid)userForActivation.ServerUserID;
                            _mainUser.Username = userForActivation.Username;
                            SetActiveUserInUserChatAssistant(_mainUser);
                        }

                        serverCommunicationInfo.UsernameStatusReportCallback(payload.MessageActionType);
                        _userChatRoomAssistantInstance.UpdateAllActiveServerUsers(payload.ActiveServerUsers);
                        serverCommunicationInfo.LogReportCallback("User Activate");
                        break;

                    case MessageActionType.ServerClientDisconnectAccepted:
                        _userChatRoomAssistantInstance.RemoveAllActiveServerUsers();
                        _userChatRoomAssistantInstance.RemoveAllChatRooms();
                        _userChatRoomAssistantInstance.RemoveAllInvites();

                        _serverAction.ExecuteDisconnectFromServer(serverCommunicationInfo);
                        break;

                    case MessageActionType.ServerUserIsDisconnected:

                        //TO DO
                        break;

                    case MessageActionType.ServerChatRoomCreated:
                        _userChatRoomAssistantInstance.AddChatRoomToAllActiveChatRooms( payload.ChatRoomCreated );
                        serverCommunicationInfo.UsernameStatusReportCallback( payload.MessageActionType );
                        _userChatRoomAssistantInstance.UpdateAllActiveServerUsers( payload.ActiveServerUsers );
                        serverCommunicationInfo.LogReportCallback($"Chat Room {payload.ChatRoomCreated.ChatRoomName} created");
                        break;

                    case MessageActionType.ServerBroadcastMessageToChatRoom:
                        List<ChatRoom> allActiveChatRooms = _userChatRoomAssistantInstance.GetAllActiveChatRooms();
                        var targetChatRoom = allActiveChatRooms.Where(a=>a.ChatRoomId == payload.ChatRoomCreated.ChatRoomId).FirstOrDefault();
                        if(targetChatRoom != null)
                        {
                            _userChatRoomAssistantInstance.AddMessageToChatRoomConversation(targetChatRoom.ChatRoomId, payload.MessageToChatRoom);
                        }

                        _userChatRoomAssistantInstance.UpdateAllActiveServerUsers(payload.ActiveServerUsers);

                        serverCommunicationInfo.LogReportCallback($"Main User in Chat Room: {payload.ChatRoomCreated.ChatRoomName} Sent Message:{payload.MessageToChatRoom}");
                        break;

                    case MessageActionType.ServerInviteSent:
                        //Add the invite to the list of invites and display it in the client view
                        _userChatRoomAssistantInstance.AddInviteToAllReceivedPendingChatRoomInvites(payload.InviteToGuestUser);
                        serverCommunicationInfo.LogReportCallback("User Sent Invite");
                        break;

                    case MessageActionType.ServerUserAcceptInvite:

                        Guid chatRoomId = payload.ChatRoomCreated.ChatRoomId;
                        ChatRoom chatRoomUpdated = payload.ChatRoomCreated;
                        Invite inviteReceived = payload.InviteToGuestUser;
                        ChatRoom chatRoomForUpdate = _userChatRoomAssistantInstance.GetAllActiveChatRooms().Where(a=>a.ChatRoomId == chatRoomId).FirstOrDefault();
                        if(chatRoomForUpdate == null)
                        {
                            _userChatRoomAssistantInstance.AddChatRoomToAllActiveChatRooms(chatRoomUpdated);                          
                        }
                        else
                        {
                            _userChatRoomAssistantInstance.UpdateActiveUsersInChatRoom(chatRoomId, chatRoomUpdated.AllActiveUsersInChatRoom);
                        }

                        if(inviteReceived != null)
                        {
                            _userChatRoomAssistantInstance.RemoveInviteFromAllReceivedPendingChatRoomInvites(inviteReceived.InviteId);
                        }
                        serverCommunicationInfo.LogReportCallback("Guest User accepted Invite");
                        break;

                    case MessageActionType.ServerUserRejectInvite:
                        Invite inviteReceivedForDeletion = payload.InviteToGuestUser;
                        if(inviteReceivedForDeletion != null)
                        {
                            _userChatRoomAssistantInstance.RemoveInviteFromAllReceivedPendingChatRoomInvites(inviteReceivedForDeletion.InviteId);
                        }
                        serverCommunicationInfo.LogReportCallback($"User {payload.InviteToGuestUser.GuestServerUser.Username} Rejected Invite");
                        break;

                    case MessageActionType.ServerExitChatRoomAccepted:
                        //We remove the chat room from the list of chat rooms
                        ChatRoom chatRoom  = payload.ChatRoomCreated;
                        _userChatRoomAssistantInstance.RemoveChatRoomFromAllActiveChatRooms(chatRoom.ChatRoomId);
                        serverCommunicationInfo.LogReportCallback($"User Exited {chatRoom.ChatRoomName} Successfully");
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
