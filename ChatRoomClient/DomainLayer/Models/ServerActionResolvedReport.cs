using ChatRoomClient.Utils.Enumerations;

namespace ChatRoomClient.DomainLayer.Models
{
    public class ServerActionResolvedReport
    {
        public MessageActionType MessageActionType { get; set; }

        public string MessageFromServer { get; set; }
        public List<ServerUser> AllActiveServerUsers { get; set; }

    }
}
