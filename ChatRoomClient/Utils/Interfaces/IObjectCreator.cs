using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface IObjectCreator
    {

        IUser CreateMainUser(ServerUser serverUserForActivation);

        Payload CreatePayload(MessageActionType messageActionType, string username);

        Payload CreatePayload(MessageActionType messageActionType, string username, Guid? mainUserId, ChatRoom chatRoom);

        ChatRoom CreateChatRoom(ServerUser chatRoomCreatorMainServerUser, string chatRoomName, List<Invite> allInvitesSentToGuestUsers);

        List<Invite> CreateInvitesForAllGuestServerUsers(ServerUser chatRoomCreatorMainServerUser, string chatRoomName, List<ServerUser> allSelectedGuestUsers);


    }
}
