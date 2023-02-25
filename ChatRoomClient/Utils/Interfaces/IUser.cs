using ChatRoomClient.DomainLayer.Models;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface IUser
    {
        Guid UserID { get; set; }
        string Username { get; set; }

        void AcceptInvite(ServerCommunicationInfo serverCommunicationInfo);

        void RejectInvite();

        void SendMessageToChatRoom(ServerCommunicationInfo serverCommunicationInfo);

        void LeaveChatRoom();
    }
}
