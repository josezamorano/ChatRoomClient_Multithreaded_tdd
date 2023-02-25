using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;

namespace ChatRoomClient.DomainLayer
{
    public class UserChatRoomAssistant : IUserChatRoomAssistant
    {
        private const string CRLF = "\r\n";
        private IUser _ActiveMainUser;
        private List<ServerUser> _allActiveServerUsers;
        private UserChatRoomAssistant userChatRoomAssistant;

        private ChatRoomUpdateDelegate _chatRoomUpdateCallback;
        private List<ChatRoom> _allActiveChatRooms;
        private List<ControlInvite> _allReceivedPendingChatRoomInvites;
        
        IObjectCreator _objectCreator;
        IServerAction _serverAction;
        public UserChatRoomAssistant(IObjectCreator objectCreator, IServerAction serverAction)
        {
            _objectCreator = objectCreator;
            _serverAction = serverAction;

            _allActiveServerUsers = new List<ServerUser>();
            _allActiveChatRooms = new List<ChatRoom>();
            _allReceivedPendingChatRoomInvites = new List<ControlInvite>(); 
        }

        public IUserChatRoomAssistant GetInstance()
        {
            
            if (userChatRoomAssistant == null)
            {
                userChatRoomAssistant = new UserChatRoomAssistant(_objectCreator, _serverAction);
            }
            return userChatRoomAssistant;
        }

        public void SetChatRoomUpdateCallback(ChatRoomUpdateDelegate chatRoomUpdateCallback)
        {
            _chatRoomUpdateCallback = chatRoomUpdateCallback;
        }

        public void SetActiveMainUser(IUser user)
        {
            _ActiveMainUser = user;
        }

        public IUser GetActiveMainUser()
        {
            return _ActiveMainUser;
        }

        public void SetAllActiveServerUsers(List<ServerUser> allActiveServerUsers)
        {
            _allActiveServerUsers= allActiveServerUsers;
        }

        public List<ServerUser> GetAllActiveServerUsers()
        {
            return _allActiveServerUsers;
        }
        
        public void CreateChatRoomAndSendInvites(ServerCommunicationInfo serverCommunicationInfo)
        {
            string chatRoomName = serverCommunicationInfo.ChatRoomName;
            ServerUser mainServerUser = GetMainUserAsServerUser();
            var allInvitesForGuests = _objectCreator.CreateInvitesForAllGuestServerUsers(mainServerUser, chatRoomName, serverCommunicationInfo.SelectedGuestUsers);
            var chatRoom = _objectCreator.CreateChatRoom(mainServerUser, chatRoomName , allInvitesForGuests );
            Payload payload = _objectCreator.CreatePayload(MessageActionType.ManagerCreateChatRoomAndSendInvites,mainServerUser.Username,mainServerUser.ServerUserID,chatRoom);
            _serverAction.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);
        }


        public void AddChatRoomToAllActiveChatRooms(ChatRoom chatRoom)
        {
            var existingChatRoom = _allActiveChatRooms.Where(x => x.ChatRoomId == chatRoom.ChatRoomId).FirstOrDefault();
            if(existingChatRoom == null)
            {
                _allActiveChatRooms.Add(chatRoom);
                _chatRoomUpdateCallback(_allActiveChatRooms);
            }            
        }


        public void AddMessageToChatRoomConversation(Guid chatRoomId, string message)
        {
            ChatRoom targetChatRoom = _allActiveChatRooms.Where(a=>a.ChatRoomId == chatRoomId).FirstOrDefault();
            if(targetChatRoom != null) 
            {
                targetChatRoom.ConversationRecord += CRLF + message;
                _chatRoomUpdateCallback(_allActiveChatRooms);
            }
        }


        public List<ChatRoom> GetAllActiveChatRooms()
        {
            return _allActiveChatRooms;
        }


        public void AddInviteToAllReceivedPendingChatRoomInvites(Invite invite)
        {
            ControlInvite duplicatedInvite = _allReceivedPendingChatRoomInvites.Where(a=>a.InviteObject.InviteId == invite.InviteId).FirstOrDefault();
            if(duplicatedInvite == null)
            {
                ControlInvite controlInvite = new ControlInvite() 
                {
                    ControlActionType = ControlActionType.Create,
                    InviteObject = invite
                };
                _allReceivedPendingChatRoomInvites.Add(controlInvite);
            }
        }

        #region Private Methods

        private ServerUser GetMainUserAsServerUser()
        {
            ServerUser chatRoomCreatorServerUser = new ServerUser()
            {
                ServerUserID = _ActiveMainUser.UserID,
                Username = _ActiveMainUser.Username
            };
            return chatRoomCreatorServerUser;
        }

       

        #endregion Private Methods


        public void AddUserToChatRoom()
        {
            throw new NotImplementedException();
        }

        public void ProcessRespondedInvites()
        {
            throw new NotImplementedException();
        }

        public void RemoveChatRoom()
        {
            throw new NotImplementedException();
        }

        public void RemoveUserFromChatRoom()
        {
            throw new NotImplementedException();
        }


        public void SetListOfUsersToChatRoom()
        {
            throw new NotImplementedException();
        }
    }
}
