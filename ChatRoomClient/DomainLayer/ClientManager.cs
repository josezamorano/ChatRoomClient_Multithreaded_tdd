using ChatRoomClient.Utils.Interfaces;
using System.Net.Sockets;
using System.Net;
using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services;
using ChatRoomClient.Utils.Enumerations;



namespace ChatRoomClient.DomainLayer
{

    public delegate void ServerActionReportDelegate(Payload payload);
    public class ClientManager : IClientManager
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
        ITcpClientProvider _tcpClientProvider;
        public ClientManager(IServerAction serverAction, IUser mainUser, IUserChatRoomAssistant userChatRoomAssistant, IObjectCreator objectCreator, ITcpClientProvider tcpClientProvider)
        {
            _serverAction = serverAction;
            _mainUser = mainUser;
            _userChatRoomAssistantInstance = userChatRoomAssistant.GetInstance();
            _objectCreator = objectCreator;
            _tcpClientProvider = tcpClientProvider;
        }

        //Tested
        public void ConnectToServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            string log = string.Empty;
            try
            {
                _serverIpAddress = IPAddress.Parse(serverCommunicationInfo.IPAddress);
                log = Notification.CRLF + "Attempting to Connect to Server...";
                serverCommunicationInfo.LogReportCallback(log);
                _tcpClient = _tcpClientProvider.CreateTcpClient(_serverIpAddress.ToString(), serverCommunicationInfo.Port); // new TcpClient(_serverIpAddress.ToString(), serverCommunicationInfo.Port);

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
                        ResolveDisconnection(serverCommunicationInfo);
                        break;

                    case MessageActionType.ServerUserIsDisconnected:

                        Guid serverUserId = (Guid)payload.ServerUserDisconnected.ServerUserID;
                        _userChatRoomAssistantInstance.RemoveUserFromAllChatRooms(serverUserId);
                        _userChatRoomAssistantInstance.UpdateAllActiveServerUsers(payload.ActiveServerUsers);
                        serverCommunicationInfo.LogReportCallback("Guest User Disconnected from Server");
                        break;

                    case MessageActionType.ServerChatRoomCreated:
                        _userChatRoomAssistantInstance.AddChatRoomToAllActiveChatRooms( payload.ChatRoomCreated );
                        serverCommunicationInfo.UsernameStatusReportCallback( payload.MessageActionType );
                        _userChatRoomAssistantInstance.UpdateAllActiveServerUsers( payload.ActiveServerUsers );
                        serverCommunicationInfo.LogReportCallback($"Chat Room {payload.ChatRoomCreated.ChatRoomName} created");
                        break;

                    case MessageActionType.ServerBroadcastMessageToChatRoom:
                        List<ControlChatRoom> allActiveChatRooms = _userChatRoomAssistantInstance.GetAllActiveChatRooms();
                        ControlChatRoom targetChatRoom = allActiveChatRooms.Where(a=>a.ChatRoomObject.ChatRoomId == payload.ChatRoomCreated.ChatRoomId).FirstOrDefault();
                        if(targetChatRoom != null)
                        {
                            _userChatRoomAssistantInstance.AddMessageToChatRoomConversation(targetChatRoom.ChatRoomObject.ChatRoomId, payload.MessageToChatRoom);
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
                        ControlChatRoom chatRoomForUpdate = _userChatRoomAssistantInstance.GetAllActiveChatRooms().Where(a=>a.ChatRoomObject.ChatRoomId == chatRoomId).FirstOrDefault();
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
                        serverCommunicationInfo.LogReportCallback($"Chat Room {chatRoom.ChatRoomName} Removed Successfully");
                        break;                       

                    case MessageActionType.ServerUserRemovedFromChatRoom:
                        ChatRoom chatRoomInfo = payload.ChatRoomCreated;
                        Guid targetServerUserId = (Guid)payload.ServerUserRemovedFromChatRoom.ServerUserID;
                        _userChatRoomAssistantInstance.RemoveUserFromSingleChatRoom(chatRoomInfo.ChatRoomId, targetServerUserId);
                        serverCommunicationInfo.LogReportCallback($"User Removed from Chat Room");
                        break;

                    case MessageActionType.ServerStopped:
                        ResolveDisconnection(serverCommunicationInfo);
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
           
        private void ResolveDisconnection(ServerCommunicationInfo serverCommunicationInfo)
        {
            _userChatRoomAssistantInstance.RemoveAllActiveServerUsers();
            _userChatRoomAssistantInstance.RemoveAllChatRooms();
            _userChatRoomAssistantInstance.RemoveAllInvites();

            _serverAction.ExecuteDisconnectFromServer(serverCommunicationInfo);
            serverCommunicationInfo.LogReportCallback("Client Disconnected from Server");
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
