using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;

namespace ChatRoomClient.DomainLayer
{
    public class UserChatRoomAssistant : IUserChatRoomAssistant
    {
        private IUser _ActiveMainUser;
        List<ServerUser> _allActiveServerUsers;

        private List<Invite> _allReceivedPendingChatRoomInvites;
        private List<ChatRoom> _allActiveChatRooms;

        IObjectCreator _objectCreator;
        IServerAction _serverAction;
        public UserChatRoomAssistant(IObjectCreator objectCreator, IServerAction serverAction)
        {
            _objectCreator = objectCreator;
            _serverAction = serverAction;
        }

        public IUserChatRoomAssistant GetInstance()
        {
            UserChatRoomAssistant userChatRoomAssistant = null;
            if (userChatRoomAssistant == null)
            {
                userChatRoomAssistant = new UserChatRoomAssistant(_objectCreator, _serverAction);
            }
            return userChatRoomAssistant;
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
