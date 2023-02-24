using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;

namespace ChatRoomClient.DomainLayer
{
    public class User : IUser
    {

        public Guid UserID { get; set; }

        public string Username { get; set; }

        IServerAction _serverAction;
        IObjectCreator _objectCreator;

        public User()
        {            
        }

        public User(IServerAction serverAction, IObjectCreator objectCreator)
        {
            _serverAction = serverAction;
            _objectCreator = objectCreator;

        }

        public void AcceptInvite()
        {
        }
               
        public void RejectInvite()
        {
            throw new NotImplementedException();
        }


        
        public void SendMessageToChatRoom(ServerCommunicationInfo serverCommunicationInfo)
        {   
            ChatRoom chatRoom = _objectCreator.CreateChatRoom(Username, UserID, serverCommunicationInfo.ChatRoomName, serverCommunicationInfo.ChatRoomId);
            string message = $"{Username} : {serverCommunicationInfo.MessageToChatRoom}";
            Payload payload = _objectCreator.CreatePayload(MessageActionType.ClientSendMessageToChatRoom, chatRoom, message);
            _serverAction.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);
        }

        public void LeaveChatRoom()
        {
            throw new NotImplementedException();
        }

    }
}
