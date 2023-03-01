using ChatRoomClient;
using ChatRoomClient.DataAccessLayer.IONetwork;
using ChatRoomClient.DomainLayer;
using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;
using ChatRoomClientTests.MockClasses;
using Xunit;



namespace ChatRoomClientTests.DomainLayer
{
    public class ServerActionTest
    {
        ISerializationProvider _serializationProvider;
        ITransmitter _transmitter;
        IServerAction _serverAction;
        public ServerActionTest()
        {
            _serializationProvider = new SerializationProvider();
            _transmitter = new Mock_Transmitter();

            _serverAction = new ServerAction(_serializationProvider,_transmitter);
        }

        [Fact]
        public void ExecuteCommunicationSendMessageToServer_CorrectInputs_ReturnOk()
        {
            //Arrange
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
                IPAddress = "127.0.0.1",
                Port = 56789,
                Username = "test",
                ChatRoomName = "ChatA",
                SelectedGuestUsers = new List<ServerUser>(),
                LogReportCallback = logReportCallback,
                ConnectionReportCallback = connectionReportCallback,
                UsernameStatusReportCallback = usernameStatusReportCallback
            };

            Payload payload = new Payload();
            //Act
             _serverAction.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);

            //Assert
        }
    }
}
