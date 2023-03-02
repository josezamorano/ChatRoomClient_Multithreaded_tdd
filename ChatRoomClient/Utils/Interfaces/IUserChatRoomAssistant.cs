using ChatRoomClient.DomainLayer.Models;



namespace ChatRoomClient.Utils.Interfaces
{
    public interface IUserChatRoomAssistant
    {
        IUserChatRoomAssistant GetInstance();

        void SetActiveMainUser(ServerUser user);

        IUser GetActiveMainUser();

        void SetOtherActiveServerUsersUpdate(OtherActiveServerUsersUpdateDelegate otherActiveServerUsersUpdateCallback);

        void SetChatRoomUpdateCallback(ChatRoomUpdateDelegate chatRoomUpdateCallback);

        void SetInviteUpdateCallback(InviteUpdateDelegate inviteUpdateCallback);

        List<ServerUser> GetAllActiveServerUsers();

        void UpdateAllActiveServerUsers(List<ServerUser> allActiveServerUsers);

        void RemoveAllActiveServerUsers();

        //**Chat Room**
        void CreateChatRoomAndSendInvites(ServerCommunicationInfo serverCommunicationInfo);

        bool AddChatRoomToAllActiveChatRooms(ChatRoom chatRoom);

        bool RemoveChatRoomFromAllActiveChatRooms(Guid chatRoomId);

        bool UpdateActiveUsersInChatRoom(Guid chatRoomId, List<ServerUser> updatedActiveUsersInChatRoom);

        List<ControlChatRoom> GetAllActiveChatRooms();

        void AddMessageToChatRoomConversation(Guid chatRoomId, string message);

        void RemoveAllChatRooms();

        void RemoveUserFromAllChatRooms(Guid serverUserId);

        void RemoveUserFromSingleChatRoom(Guid chatRoomId, Guid serverUserId);

        //**Invites**
        void AddInviteToAllReceivedPendingChatRoomInvites(Invite invite);

        void RemoveInviteFromAllReceivedPendingChatRoomInvites(Guid inviteId);

        void RemoveAllInvites();
    }
}
