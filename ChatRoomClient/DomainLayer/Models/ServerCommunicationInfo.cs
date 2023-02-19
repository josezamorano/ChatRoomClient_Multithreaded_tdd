namespace ChatRoomClient.DomainLayer.Models
{
    public class ServerCommunicationInfo
    {
        public string IPAddress { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }
    
        public ClientLogReportDelegate LogReportCallback { get; set; }

        public ClientConnectionReportDelegate ConnectionReportCallback { get; set; }

        public UsernameStatusReportDelegate UsernameStatusReportCallback { get; set; }

        public OtherServerUsersReportDelegate OtherServerUsersReportCallback { get; set; }
    }
}
