using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;
using System.Net.Sockets;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface IServerAction
    {
        string ResolveCommunicationToServer(TcpClient tcpClient, MessageActionType messageActionType, string username);

        ServerActionResolvedReport ResolveCommunicationFromServer(TcpClient tcpClient);

        ServerActionResolvedReport ResolveActionRequestedByServer(Payload payload);
    }
}
