
using System.Net.Sockets;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface ITcpClientProvider
    {
        TcpClient CreateTcpClient(string serverIpAddress, int port);
    }
}
