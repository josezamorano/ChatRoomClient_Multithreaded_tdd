using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface IUserChatRoomAssistant
    {
        IUserChatRoomAssistant GetInstance();

        void SetChatRoomUpdateCallback(ChatRoomUpdateDelegate chatRoomUpdateCallback);

        void SetInviteUpdateCallback(InviteUpdateDelegate inviteUpdateCallback);

        void SetActiveMainUser(IUser user);
        
        IUser GetActiveMainUser();

        void SetAllActiveServerUsers(List<ServerUser> allActiveServerUsers);

        List<ServerUser> GetAllActiveServerUsers();


        //**Chat Room**
        void CreateChatRoomAndSendInvites(ServerCommunicationInfo serverCommunicationInfo);

        bool AddChatRoomToAllActiveChatRooms(ChatRoom chatRoom);

        bool UpdateActiveUsersInChatRoom(Guid chatRoomId, List<ServerUser> updatedActiveUsersInChatRoom);

        List<ChatRoom> GetAllActiveChatRooms();

        void AddMessageToChatRoomConversation(Guid chatRoomId, string message);


        //**Invites**
        void AddInviteToAllReceivedPendingChatRoomInvites(Invite invite);

        void RemoveInviteFromAllReceivedPendingChatRoomInvites(Guid inviteId);
    }
}
