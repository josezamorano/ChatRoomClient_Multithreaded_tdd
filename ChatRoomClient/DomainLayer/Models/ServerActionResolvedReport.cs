using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;

namespace ChatRoomClient.DomainLayer.Models
{
    public class ServerActionResolvedReport
    {
        public MessageActionType MessageActionType { get; set; }

        public string MessageFromServer { get; set; }

        public IUser MainUser { get; set; }
        public List<ServerUser> AllActiveServerUsers { get; set; }

    }
}
