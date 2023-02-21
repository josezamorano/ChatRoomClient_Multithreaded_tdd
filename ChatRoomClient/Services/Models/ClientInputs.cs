namespace ChatRoomClient.Services.Models
{
    public class ClientInputs
    {
        public string Username { get; set; }

        public string IPAddress { get; set; }

        public string Port { get; set; }

        public string ChatRoomName { get; set; }

        public bool GuestSelectorStatus { get; set; }
    }
}
