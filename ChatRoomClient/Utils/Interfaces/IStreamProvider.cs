using System.Net.Sockets;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface IStreamProvider
    {
        StreamReader CreateStreamReader(NetworkStream networkStream);
        StreamWriter CreateStreamWriter(NetworkStream networkStream);
    }
}
