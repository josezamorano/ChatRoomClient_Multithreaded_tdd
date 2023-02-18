using ChatRoomClient.DomainLayer;
using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;
using System.Net.Sockets;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface IServerAction
    {
        string ResolveCommunicationToServer(TcpClient tcpClient, MessageActionType messageActionType, string username);

        void ResolveCommunicationFromServer(TcpClient tcpClient, ServerActionReportDelegate serverActionReportCallback);

        ServerActionResolvedReport ResolveActionRequestedByServer(Payload payload);
    }
}
