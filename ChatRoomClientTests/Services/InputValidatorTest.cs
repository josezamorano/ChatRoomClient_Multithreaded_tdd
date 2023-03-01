using ChatRoomClient.Services;
using ChatRoomClient.Services.Models;
using ChatRoomClient.Utils.Interfaces;
using Xunit;
namespace ChatRoomClientTests.Services
{
    public class InputValidatorTest
    {

        IInputValidator _inputValidator;
        public InputValidatorTest()
        {
            _inputValidator = new InputValidator();
        }

        [Fact]
        public void ValidateClientConnectToServerInputs_CorrectInputs_ReturnOK()
        {
            //Arramge
            ClientInputs clientInputs = new ClientInputs() 
            {
                ChatRoomName="test",
                GuestSelectorStatus=true,
                IPAddress="127.0.0.1",
                Port = "56789",
                Username="username",
                
            
            };
            //Act
            var actualResult = _inputValidator.ValidateClientConnectToServerInputs(clientInputs);
            //Assert
            Assert.True(actualResult.InputsAreValid);
        }
    }
}
