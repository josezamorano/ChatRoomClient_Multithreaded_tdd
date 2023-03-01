using ChatRoomClient.Utils.Interfaces;
using ChatRoomClient.DomainLayer;

using Xunit;
using ChatRoomClientTests.MockClasses;
using ChatRoomClient.Services;
using ChatRoomClient.DataAccessLayer.IONetwork;
using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient;

namespace ChatRoomClientTests.DomainLayer
{
    public class ClientManagerTest
    {
        IServerAction _serverAction;
        IUser _user;
        IObjectCreator _objectCreator;
        ITcpClientProvider _tcpClientProvider;

        IUserChatRoomAssistant _userChatRoomAssistant;
        IClientManager _clientManager;
        public ClientManagerTest()
        {
            _serverAction = new Mock_ServerAction();
            _user = new User();
            _userChatRoomAssistant = new Mock_UserChatRoomAssistant();
            _objectCreator = new ObjectCreator();
            _tcpClientProvider = new TcpClientProvider();

            _clientManager = new ClientManager(_serverAction,_user, _userChatRoomAssistant, _objectCreator , _tcpClientProvider);

        }

        [Fact]
        public void ConnectToServer_CorrectInputs_ReturnOk()
        {
            //Arrange
            //Arrange
            void ClientLogReportCallback(string report)
            {
                //Assert
                Assert.Contains(Notification.Exception, report);
            }

            void ClientConnectionReportCallback(bool isConnecte)
            {
                var stop = "here";
            }

            void UsernameActivationStatusCallback(MessageActionType messageActionType)
            {
                var stop = "here";
            }


            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);
            ClientConnectionReportDelegate connectionReportCallback = new ClientConnectionReportDelegate(ClientConnectionReportCallback);
            UsernameStatusReportDelegate usernameStatusReportCallback = new UsernameStatusReportDelegate(UsernameActivationStatusCallback);

            ServerCommunicationInfo serverCommunicationInfo = new ServerCommunicationInfo()
            {
                IPAddress = "127.0.0.1",
                Port = 56789,
                Username = "test",
                ChatRoomName = "ChatA",
                SelectedGuestUsers = new List<ServerUser>(),
                LogReportCallback = logReportCallback,
                ConnectionReportCallback = connectionReportCallback,
                UsernameStatusReportCallback = usernameStatusReportCallback
            };
            //Act
            _clientManager.ConnectToServer(serverCommunicationInfo);
            //Assert


        }
    }
}
