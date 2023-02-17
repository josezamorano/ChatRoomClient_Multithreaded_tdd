using ChatRoomClient.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
