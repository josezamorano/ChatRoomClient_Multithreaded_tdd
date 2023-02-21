using ChatRoomClient.Utils.Interfaces;

namespace ChatRoomClient.DomainLayer
{
    public class User : IUser
    {

        public Guid UserID { get; set; }

        public string Username { get; set; }



        public void AcceptInvite()
        {
        }
               
        public void RejectInvite()
        {
            throw new NotImplementedException();
        }

        public void SendMessageToChatRoom()
        {
            throw new NotImplementedException();
        }

        public void LeaveChatRoom()
        {
            throw new NotImplementedException();
        }

    }
}
