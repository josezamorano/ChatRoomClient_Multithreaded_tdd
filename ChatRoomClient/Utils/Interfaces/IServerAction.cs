using ChatRoomClient.DomainLayer;
using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;
using System.Net.Sockets;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface IServerAction
    {
        void SetActiveTcpClient(TcpClient activeTcpClient);

        void ExecuteDisconnectFromServer(ClientLogReportDelegate logReportCallback, ClientConnectionReportDelegate connectionReportCallback);

        void ExecuteCommunicationSendMessageToServer(Payload payload, ServerCommunicationInfo serverCommunicationInfo);
               
        void ResolveCommunicationFromServer(ServerCommunicationInfo serverCommunicationInfo, ServerActionReportDelegate serverActionReportCallback);

    }
}
