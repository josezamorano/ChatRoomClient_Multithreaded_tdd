using ChatRoomClient.DomainLayer;
using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;

namespace ChatRoomClient.Services
{
    public class ObjectCreator :IObjectCreator
    {
        public IUser CreateMainUser(ServerUser serverUserForActivation)
        {
            IUser activeUser = new User()
            {
                Username = serverUserForActivation.Username,
                UserID = (Guid)serverUserForActivation.ServerUserID
            };
            return activeUser;
        }


        public Payload CreatePayload(MessageActionType messageActionType, string username)
        {
            Payload payload = new Payload()
            {
                MessageActionType = messageActionType,
                ClientUsername = username
            };
            return payload;
        }


        public Payload CreatePayload(MessageActionType messageActionType, string username, Guid? mainUserId, ChatRoom chatRoom)
        {

            Payload payload = new Payload()
            {
                MessageActionType = messageActionType,
                ClientUsername = username,
                UserGuid = mainUserId,
                ChatRoomCreated = chatRoom
            };
            return payload;
        }

        public ChatRoom CreateChatRoom(ServerUser chatRoomCreatorMainServerUser,string chatRoomName, List<Invite> allInvitesSentToGuestUsers)
        {
            List<ServerUser> allActiveUsersInChatRoom = new List<ServerUser>() { chatRoomCreatorMainServerUser };
            ChatRoom chatRoom = new ChatRoom()
            {
                ChatRoomName = chatRoomName,
                Creator = chatRoomCreatorMainServerUser,
                ConversationRecord = string.Empty,
                AllActiveUsersInChatRoom = allActiveUsersInChatRoom,
                AllInvitesSentToGuestUsers = allInvitesSentToGuestUsers

            };

            return chatRoom;
        }

        public List<Invite> CreateInvitesForAllGuestServerUsers(ServerUser chatRoomCreatorMainServerUser , string chatRoomName, List<ServerUser> allSelectedGuestUsers)
        {
            List<Invite> allInvitesForAllGuests = new List<Invite>();
            foreach (ServerUser serverUser in allSelectedGuestUsers)
            {
                var invite = new Invite()
                {
                    ChatRoomCreator = chatRoomCreatorMainServerUser,
                    GuestServerUser = serverUser,
                    ChatRoomName = chatRoomName,
                    InviteStatus = InviteStatus.CreatedNotSent
                };
                allInvitesForAllGuests.Add(invite);
            }
            return allInvitesForAllGuests;
        }

        #region Private Methods 

        #endregion Private Methods

    }
}
