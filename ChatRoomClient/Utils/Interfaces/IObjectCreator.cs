﻿using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface IObjectCreator
    {
        Payload CreatePayload(MessageActionType messageActionType, string username);

        Payload CreatePayload(MessageActionType messageActionType, string username, Guid? mainUserId, ChatRoom chatRoom);

        Payload CreatePayload(MessageActionType messageActionType, ChatRoom chatRoom, string messageToChatRoom);

        Payload CreatePayload(MessageActionType messageActionType, ChatRoom chatRoom, Invite invite);

        ChatRoom CreateChatRoom(ServerUser chatRoomCreatorMainServerUser, string chatRoomName, List<Invite> allInvitesSentToGuestUsers);

        ChatRoom CreateChatRoom(string username, Guid userId, string chatRoomName, Guid chatRoomId);

        

        List<Invite> CreateInvitesForAllGuestServerUsers(ServerUser chatRoomCreatorMainServerUser, string chatRoomName, List<ServerUser> allSelectedGuestUsers);

    }
}
