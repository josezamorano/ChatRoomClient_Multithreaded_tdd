
using ChatRoomClient.Utils.Interfaces;
using System.Net.Sockets;


namespace ChatRoomClient.DataAccessLayer.IONetwork
{
    public class TcpClientProvider :ITcpClientProvider
    {

        public TcpClient CreateTcpClient(string serverIpAddress, int port)
        {
            
            var newTcpClient = new TcpClient( serverIpAddress, port);
            return newTcpClient;
        }
    }
}
