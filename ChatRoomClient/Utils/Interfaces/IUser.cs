namespace ChatRoomClient.Utils.Interfaces
{
    public interface IUser
    {
        void AcceptInvite();

        void RejectInvite();

        void SendMessageToChatRoom();

        void LeaveChatRoom();
    }
}
