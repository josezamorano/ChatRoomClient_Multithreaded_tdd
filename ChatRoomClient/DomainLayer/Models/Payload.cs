using ChatRoomClient.Utils.Enumerations;

namespace ChatRoomClient.DomainLayer.Models
{
    public class Payload
    {
        public MessageActionType MessageActionType { get; set; }

        public string ClientUsername { get; set; }

        public Guid? UserGuid { get; set; }

        public List<ServerUser> ActiveServerUsers { get; set; }
    }
}
