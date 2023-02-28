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
        private List<ControlChatRoom> _allActiveChatRooms;
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
            _allActiveChatRooms = new List<ControlChatRoom>();
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

        //SERVERUSER BEGIN****
        public List<ServerUser> GetAllActiveServerUsers()
        {
            return _allActiveServerUsers;
        }

        public void UpdateAllActiveServerUsers(List<ServerUser> allActiveServerUsers)
        {
            _allActiveServerUsers = allActiveServerUsers;
            var itemForRemoval = _allActiveServerUsers.Where(a => a.ServerUserID == _ActiveMainUser.UserID).FirstOrDefault();

            if (itemForRemoval != null)
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
        
        //SERVERUSER END******
        //CHATROOM BEGIN******
        public void CreateChatRoomAndSendInvites(ServerCommunicationInfo serverCommunicationInfo)
        {
            string chatRoomName = serverCommunicationInfo.ChatRoomName;
            ServerUser mainServerUser = GetMainUserAsServerUser();
            var allInvitesForGuests = _objectCreator.CreateAllInvitesForAllGuestServerUsers(mainServerUser, chatRoomName, serverCommunicationInfo.SelectedGuestUsers);
            var chatRoom = _objectCreator.CreateChatRoom(mainServerUser, chatRoomName, allInvitesForGuests);
            Payload payload = _objectCreator.CreatePayload(MessageActionType.ManagerCreateChatRoomAndSendInvites, mainServerUser.Username, mainServerUser.ServerUserID, chatRoom);
            _serverAction.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);
        }

       
        public bool AddChatRoomToAllActiveChatRooms(ChatRoom chatRoom)
        {
            var existingChatRoom = _allActiveChatRooms.Where(x => x.ChatRoomObject.ChatRoomId == chatRoom.ChatRoomId).FirstOrDefault();
            if (existingChatRoom == null)
            {
                ControlChatRoom newControlChatRoom = new ControlChatRoom() 
                {
                    ControlActionType = ControlActionType.Create,
                    ChatRoomObject = chatRoom,
                };

                _allActiveChatRooms.Add(newControlChatRoom);
                _chatRoomUpdateCallback(_allActiveChatRooms);

                return true;
            }

            return false;
        }

        public bool RemoveChatRoomFromAllActiveChatRooms(Guid chatRoomId)
        {
            bool taskExecuted = false;
            ControlChatRoom chatRoomForDeletion = _allActiveChatRooms.Where(x => x.ChatRoomObject .ChatRoomId == chatRoomId).FirstOrDefault();
            if (chatRoomForDeletion != null)
            {
                chatRoomForDeletion.ControlActionType = ControlActionType.Delete;
                taskExecuted = true;
            }
            _chatRoomUpdateCallback(_allActiveChatRooms);
            return taskExecuted;
        }

        public bool UpdateActiveUsersInChatRoom(Guid chatRoomId, List<ServerUser> updatedActiveUsersInChatRoom)
        {
            ControlChatRoom targetChatRoom = _allActiveChatRooms.Where(a => a.ChatRoomObject.ChatRoomId == chatRoomId).FirstOrDefault();
            if (targetChatRoom == null) { return false; }
            targetChatRoom.ChatRoomObject.AllActiveUsersInChatRoom = updatedActiveUsersInChatRoom;
            targetChatRoom.ControlActionType = ControlActionType.Update;

            _chatRoomUpdateCallback(_allActiveChatRooms);

            return true;
        }

        public List<ControlChatRoom> GetAllActiveChatRooms()
        {            
            return _allActiveChatRooms;
        }

        public void AddMessageToChatRoomConversation(Guid chatRoomId, string message)
        {
            ControlChatRoom targetChatRoom = _allActiveChatRooms.Where(a => a.ChatRoomObject.ChatRoomId == chatRoomId).FirstOrDefault();
            if (targetChatRoom != null)
            {
                targetChatRoom.ChatRoomObject.ConversationRecord += CRLF + message;
                targetChatRoom.ControlActionType = ControlActionType.Update;

                _chatRoomUpdateCallback(_allActiveChatRooms);
            }
        }

        public void RemoveAllChatRooms()
        {
            foreach (ControlChatRoom controlChatRoom in _allActiveChatRooms)
            {
                controlChatRoom.ControlActionType = ControlActionType.Delete;
            }

            _chatRoomUpdateCallback(_allActiveChatRooms);
        }


        public void RemoveUserFromAllChatRooms(Guid serverUserId)
        {
            foreach(ControlChatRoom chatRoom in _allActiveChatRooms)
            {
                var serverUserForDeletion = chatRoom.ChatRoomObject.AllActiveUsersInChatRoom.Where(a=>a.ServerUserID == serverUserId).FirstOrDefault();
                if(serverUserForDeletion != null)
                {
                    chatRoom.ChatRoomObject.AllActiveUsersInChatRoom.Remove(serverUserForDeletion);
                    chatRoom.ControlActionType = ControlActionType.Update;
                }
            }

            _chatRoomUpdateCallback(_allActiveChatRooms);
        }


        public void RemoveUserFromSingleChatRoom(Guid chatRoomId, Guid serverUserId)
        {
            ControlChatRoom selectedControlChatRoom = _allActiveChatRooms.Where(a => a.ChatRoomObject.ChatRoomId == chatRoomId).FirstOrDefault();
            if (selectedControlChatRoom == null) { return; }
            
            ServerUser targetUser = selectedControlChatRoom.ChatRoomObject.AllActiveUsersInChatRoom.Where(a=>a.ServerUserID == serverUserId).FirstOrDefault();
            if (targetUser == null) { return; }
            selectedControlChatRoom.ChatRoomObject.AllActiveUsersInChatRoom.Remove(targetUser);
            selectedControlChatRoom.ControlActionType = ControlActionType.Update;
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
        
    }
}
