using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Interfaces;

namespace ChatRoomClient.DomainLayer
{
    public class UserChatRoomAssistant : IUserChatRoomAssistant
    {
        private IUser _ActiveUser;

        private List<ChatRoom> _receivedPendingChatRoomInvites;

        private List<ChatRoom> _allActiveChatRooms;


        public UserChatRoomAssistant()
        {
        }

        public void SetActiveUser(User user)
        {
            _ActiveUser = user;
        }

        public IUser GetActiveUser()
        {
            return _ActiveUser;
        }

        public IUserChatRoomAssistant GetInstance()
        {
            UserChatRoomAssistant userChatRoomAssistant = null;
            if(userChatRoomAssistant == null)
            {
                userChatRoomAssistant= new UserChatRoomAssistant();
            }
            return userChatRoomAssistant;
        }

        public void AddUserToChatRoom()
        {
            throw new NotImplementedException();
        }

        public void CreateChatRoom()
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

        public void SendInvitesToUsers()
        {
            throw new NotImplementedException();
        }

        public void SetListOfUsersToChatRoom()
        {
            throw new NotImplementedException();
        }
    }
}
