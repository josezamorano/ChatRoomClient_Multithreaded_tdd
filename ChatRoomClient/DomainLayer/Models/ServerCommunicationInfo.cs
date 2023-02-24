namespace ChatRoomClient.DomainLayer.Models
{
    public class ServerCommunicationInfo
    {
        public string IPAddress { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string ChatRoomName { get; set; }

        public Guid ChatRoomId { get; set; }

        public string MessageToChatRoom { get; set; }
        public List<ServerUser> SelectedGuestUsers { get; set; }

        public ClientLogReportDelegate LogReportCallback { get; set; }

        public ClientConnectionReportDelegate ConnectionReportCallback { get; set; }

        public UsernameStatusReportDelegate UsernameStatusReportCallback { get; set; }

        public OtherServerUsersReportDelegate OtherServerUsersReportCallback { get; set; }
    }
}
