using ChatRoomClient.DomainLayer.Models;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface IUserChatRoomAssistant
    {
        IUserChatRoomAssistant GetInstance();

        void SetChatRoomUpdateCallback(ChatRoomUpdateDelegate chatRoomUpdateCallback);

        void SetActiveMainUser(IUser user);
        
        IUser GetActiveMainUser();

        void SetAllActiveServerUsers(List<ServerUser> allActiveServerUsers);

        List<ServerUser> GetAllActiveServerUsers();

        void CreateChatRoomAndSendInvites(ServerCommunicationInfo serverCommunicationInfo);

        void AddChatRoomToAllActiveChatRooms(ChatRoom chatRoom);

        List<ChatRoom> GetAllActiveChatRooms();

        void AddMessageToChatRoomConversation(Guid chatRoomId, string message);
    }
}
