
using ChatRoomClient.DomainLayer;
using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services;
using ChatRoomClient.Utils.Interfaces;
using System.Net.Sockets;

namespace ChatRoomClientTests.MockClasses
{
    public class Mock_ServerAction : IServerAction
    {
        public void ExecuteCommunicationSendMessageToServer(Payload payload, ServerCommunicationInfo serverCommunicationInfo)
        {

            serverCommunicationInfo.LogReportCallback(Notification.MessageSentOk);
        }

        public void ExecuteDisconnectFromServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            throw new NotImplementedException();
        }

        public void ResolveCommunicationFromServer(ServerCommunicationInfo serverCommunicationInfo, ServerActionReportDelegate serverActionReportCallback)
        {
            throw new NotImplementedException();
        }

        public void SetActiveTcpClient(TcpClient activeTcpClient)
        {
            
        }
    }
}
