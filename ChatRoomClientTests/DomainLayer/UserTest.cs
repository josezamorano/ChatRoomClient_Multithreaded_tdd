using ChatRoomClient;
using ChatRoomClient.DomainLayer;
using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;
using ChatRoomClientTests.MockClasses;
using Xunit;



namespace ChatRoomClientTests.DomainLayer
{
    public class UserTest
    {
        IServerAction _serverAction;
        IObjectCreator _objectCreator;


        IUser _user;
        public UserTest()
        {
            _serverAction = new Mock_ServerAction();
            _objectCreator = new ObjectCreator();
            _user = new User(_serverAction, _objectCreator);
        }

        [Fact]
        public void AcceptInvite_CorrectInputs_ReturnOK()
        {
            //Arrange
            void ClientLogReportCallback(string report)
            {
                //Assert
                Assert.Equal(Notification.MessageSentOk, report);
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
                IPAddress ="127.0.0.1",
                Port = 56789,
                Username = "test",
                ChatRoomName = "ChatA",
                SelectedGuestUsers = new List<ServerUser>(),
                LogReportCallback = logReportCallback,
                ConnectionReportCallback = connectionReportCallback,
                UsernameStatusReportCallback = usernameStatusReportCallback
            };
            //Act
            _user.AcceptInvite(serverCommunicationInfo);
            //Assert
        }

    }
}
