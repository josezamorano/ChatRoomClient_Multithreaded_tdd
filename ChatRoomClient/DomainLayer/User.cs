using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;
using System.Windows.Forms;

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

        public void AcceptInvite(ServerCommunicationInfo serverCommunicationInfo)
        {            
            ServerUser serverUser = new ServerUser() { ServerUserID = UserID, Username = Username };
            ChatRoom chatRoom = _objectCreator.CreateChatRoom(serverUser, string.Empty, new List<Invite>());
            chatRoom.ChatRoomId = serverCommunicationInfo.ChatRoomId;
            Invite invite = new Invite() { InviteId = serverCommunicationInfo.InviteId, InviteStatus=InviteStatus.Accepted};
            Payload payload = _objectCreator.CreatePayload(MessageActionType.ServerUserAcceptInvite , chatRoom, invite);
            _serverAction.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);
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
