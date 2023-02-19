namespace ChatRoomClient.DomainLayer.Models
{
    public class ChatRoom
    {
        public Guid ChatRoomId { get; set; }

        public string ChatRoomName { get; set; }

        public  string ChatRoomIdentifierNameId { get; set; }

        public string ConversationRecord { get; set; }

        public List<User> ActiveUsersInChatRoom { get; set; }

        public List<User> InvitedUsers { get; set; }
    }
}
