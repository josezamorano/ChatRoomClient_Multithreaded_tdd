using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;

namespace ChatRoomClient.DomainLayer
{
    public class UserChatRoomAssistant : IUserChatRoomAssistant
    {
        private const string CRLF = "\r\n";
        private IUser _ActiveMainUser;

        private UserChatRoomAssistant userChatRoomAssistant;

        private List<ServerUser> _allActiveServerUsers;
        private List<ChatRoom> _allActiveChatRooms;
        private List<ControlInvite> _allReceivedPendingChatRoomInvites;
        private OtherActiveServerUsersUpdateDelegate _otherActiveServerUsersUpdateCallback;
        private ChatRoomUpdateDelegate _chatRoomUpdateCallback;
        private InviteUpdateDelegate _inviteUpdateCallback;
      
        
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

        public void SetOtherActiveServerUsersUpdate(OtherActiveServerUsersUpdateDelegate otherActiveServerUsersUpdateCallback)
        {
            _otherActiveServerUsersUpdateCallback = otherActiveServerUsersUpdateCallback;
        }
        public void SetChatRoomUpdateCallback(ChatRoomUpdateDelegate chatRoomUpdateCallback)
        {
            _chatRoomUpdateCallback = chatRoomUpdateCallback;
        }

        public void SetInviteUpdateCallback(InviteUpdateDelegate inviteUpdateCallback)
        {
            _inviteUpdateCallback = inviteUpdateCallback;
        }

        public void SetActiveMainUser(IUser user)
        {
            _ActiveMainUser = user;
        }

        public IUser GetActiveMainUser()
        {
            return _ActiveMainUser;
        }

        
        public List<ServerUser> GetAllActiveServerUsers()
        {
            return _allActiveServerUsers;
        }

        public void UpdateAllActiveServerUsers(List<ServerUser> allActiveServerUsers)
        {
            _allActiveServerUsers = allActiveServerUsers;
            var itemForRemoval = _allActiveServerUsers.Where(a=>a.ServerUserID == _ActiveMainUser.UserID).FirstOrDefault();
          
            if (itemForRemoval !=null)
            {
                var itemRemoved = _allActiveServerUsers.Remove(itemForRemoval);               
            }
            _otherActiveServerUsersUpdateCallback(_allActiveServerUsers);
        }

        public void RemoveAllActiveServerUsers()
        {
            _allActiveServerUsers.Clear();
            _otherActiveServerUsersUpdateCallback(_allActiveServerUsers);
        }

        //CHATROOM BEGIN******
        public void CreateChatRoomAndSendInvites(ServerCommunicationInfo serverCommunicationInfo)
        {
            string chatRoomName = serverCommunicationInfo.ChatRoomName;
            ServerUser mainServerUser = GetMainUserAsServerUser();
            var allInvitesForGuests = _objectCreator.CreateAllInvitesForAllGuestServerUsers(mainServerUser, chatRoomName, serverCommunicationInfo.SelectedGuestUsers);
            var chatRoom = _objectCreator.CreateChatRoom(mainServerUser, chatRoomName , allInvitesForGuests );
            Payload payload = _objectCreator.CreatePayload(MessageActionType.ManagerCreateChatRoomAndSendInvites,mainServerUser.Username,mainServerUser.ServerUserID,chatRoom);
            _serverAction.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);
        }

        public bool AddChatRoomToAllActiveChatRooms(ChatRoom chatRoom)
        {
            var existingChatRoom = _allActiveChatRooms.Where(x => x.ChatRoomId == chatRoom.ChatRoomId).FirstOrDefault();
            if(existingChatRoom == null)
            {
                _allActiveChatRooms.Add(chatRoom);
                _chatRoomUpdateCallback(_allActiveChatRooms);
                
                return true;
            }

            return false;
        }

        public bool RemoveChatRoomFromAllActiveChatRooms(Guid chatRoomId)
        {
            bool taskExecuted = false;
            ChatRoom chatRoomForDeletion = _allActiveChatRooms.Where(x => x.ChatRoomId == chatRoomId).FirstOrDefault();
            if (chatRoomForDeletion != null)
            {
                _allActiveChatRooms.Remove(chatRoomForDeletion);

                taskExecuted = true;
            }
            _chatRoomUpdateCallback(_allActiveChatRooms);
            return taskExecuted;
        }

        public bool UpdateActiveUsersInChatRoom(Guid chatRoomId, List<ServerUser> updatedActiveUsersInChatRoom)
        {
            var targetChatRoom = _allActiveChatRooms.Where(a=>a.ChatRoomId == chatRoomId).FirstOrDefault();
            if(targetChatRoom == null) { return false; }
            targetChatRoom.AllActiveUsersInChatRoom = updatedActiveUsersInChatRoom;
           

            _chatRoomUpdateCallback(_allActiveChatRooms);

            return true;
        }

        public List<ChatRoom> GetAllActiveChatRooms()
        {
            return _allActiveChatRooms;
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

        public void RemoveAllChatRooms()
        {
            _allActiveChatRooms.Clear();

            _chatRoomUpdateCallback(_allActiveChatRooms);
        }
        //CHATROOM END********


        //INVITES BEGIN*******

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

                _inviteUpdateCallback(_allReceivedPendingChatRoomInvites);
            }
        }

        public void RemoveInviteFromAllReceivedPendingChatRoomInvites(Guid inviteId)
        {
            ControlInvite inviteForRemoval = _allReceivedPendingChatRoomInvites.Where(a => a.InviteObject.InviteId == inviteId).FirstOrDefault();
            if (inviteForRemoval != null)
            {
                inviteForRemoval.ControlActionType = ControlActionType.Delete;
                _inviteUpdateCallback(_allReceivedPendingChatRoomInvites);
            }
        }

        public void RemoveAllInvites()
        {
           foreach(ControlInvite controlInvite in  _allReceivedPendingChatRoomInvites)
           {
                controlInvite.ControlActionType = ControlActionType.Delete;
           }

           _inviteUpdateCallback( _allReceivedPendingChatRoomInvites);
        }
        //INVITES END*********
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
