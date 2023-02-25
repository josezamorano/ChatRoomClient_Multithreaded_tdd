using ChatRoomClient.Utils.Enumerations;

namespace ChatRoomClient.DomainLayer.Models
{
    public class ControlChatRoom
    {
        public ControlActionType ControlActionType { get; set; }

        public ChatRoom ChatRoomObject { get; set; }
    }
}
