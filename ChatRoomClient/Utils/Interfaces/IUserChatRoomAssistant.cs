using ChatRoomClient.DomainLayer;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface IUserChatRoomAssistant
    {
        void SetActiveUser(User user);

        IUserChatRoomAssistant GetInstance();

        IUser GetActiveUser();
    }
}
