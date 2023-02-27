using ChatRoomClient.DomainLayer.Models;



namespace ChatRoomClient.Utils.Interfaces
{
    public interface IUserChatRoomAssistant
    {
        IUserChatRoomAssistant GetInstance();

        void SetOtherActiveServerUsersUpdate(OtherActiveServerUsersUpdateDelegate otherActiveServerUsersUpdateCallback);

        void SetChatRoomUpdateCallback(ChatRoomUpdateDelegate chatRoomUpdateCallback);

        void SetInviteUpdateCallback(InviteUpdateDelegate inviteUpdateCallback);

        void SetActiveMainUser(IUser user);
        
        IUser GetActiveMainUser();

        List<ServerUser> GetAllActiveServerUsers();

        void UpdateAllActiveServerUsers(List<ServerUser> allActiveServerUsers);

        void RemoveAllActiveServerUsers();

        //**Chat Room**
        void CreateChatRoomAndSendInvites(ServerCommunicationInfo serverCommunicationInfo);

        bool AddChatRoomToAllActiveChatRooms(ChatRoom chatRoom);

        bool RemoveChatRoomFromAllActiveChatRooms(Guid chatRoomId);

        bool UpdateActiveUsersInChatRoom(Guid chatRoomId, List<ServerUser> updatedActiveUsersInChatRoom);

        List<ChatRoom> GetAllActiveChatRooms();

        void AddMessageToChatRoomConversation(Guid chatRoomId, string message);

        void RemoveAllChatRooms();

        //**Invites**
        void AddInviteToAllReceivedPendingChatRoomInvites(Invite invite);

        void RemoveInviteFromAllReceivedPendingChatRoomInvites(Guid inviteId);

        void RemoveAllInvites();
    }
}
