using ChatRoomClient.Services;
using ChatRoomClient.Utils.Interfaces;
using Xunit;

namespace ChatRoomClientTests.Services
{
    public class ObjectCreatorTest
    {
        IObjectCreator _objectCreator;
        public ObjectCreatorTest()
        {
            _objectCreator = new ObjectCreator();
        }

        [Fact]
        public void CreatePayload_CorrectInputs_ReturnOk()
        {
            //Arrange
            string username = "test";
            //Act
            var actualResult = _objectCreator.CreatePayload(ChatRoomClient.Utils.Enumerations.MessageActionType.CreateUser , username);
            //Assert
            var payloadUsername = actualResult.ClientUsername;
            Assert.Equal(username, payloadUsername);
        }
    }
}
