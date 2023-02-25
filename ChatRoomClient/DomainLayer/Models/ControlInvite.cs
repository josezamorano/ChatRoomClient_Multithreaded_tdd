using ChatRoomClient.Utils.Enumerations;

namespace ChatRoomClient.DomainLayer.Models
{
    public class ControlInvite
    {

        public ControlActionType ControlActionType { get; set; }

        public Invite InviteObject { get; set; }
    }
}
