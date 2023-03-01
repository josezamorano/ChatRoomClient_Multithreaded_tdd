using ChatRoomClient;
using ChatRoomClient.DomainLayer;
using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services;
using ChatRoomClient.Utils.Interfaces;
using ChatRoomClientTests.MockClasses;
using Xunit;



namespace ChatRoomClientTests.DomainLayer
{
    public class UserChatRoomAssistantTest
    {

        IObjectCreator _objectCreator;
        IServerAction _serverAction;
        IUserChatRoomAssistant _userChatRoomAssistant;
        public UserChatRoomAssistantTest()
        {
            _objectCreator = new ObjectCreator();
            _serverAction = new Mock_ServerAction();
            _userChatRoomAssistant = new UserChatRoomAssistant(_objectCreator, _serverAction);
        }



        [Fact]
        public void UpdateAllActiveServerUsers_CorrectInputs_ReturnOK()
        {
            //Arrange
            Guid mainUserId = Guid.NewGuid();
            string mainUserUsername = "Main-user";
            IUser mainUser = new User() 
            {
                UserID = mainUserId,  
                Username = mainUserUsername,
            };
            _userChatRoomAssistant.SetActiveMainUser(mainUser);
            void OtherActiveServerUsersUpdateCallback(List<ServerUser> allServerUsers)
            {
                //Assert
                int expected = 2;
                int count = allServerUsers.Count;
                Assert.Equal(expected, count);
            }
            _userChatRoomAssistant.SetOtherActiveServerUsersUpdate(OtherActiveServerUsersUpdateCallback);

            ServerUser serverUser1 = new ServerUser() 
            {
                Username = "serverUserTest1",
                ServerUserID = Guid.NewGuid(),
            };
            ServerUser serverUser2 = new ServerUser()
            {
                Username = "serverUserTest2",
                ServerUserID = Guid.NewGuid(),
            };
            ServerUser serverUser3 = new ServerUser()
            {
                Username = mainUserUsername,
                ServerUserID = mainUserId,
            };
            List<ServerUser> allServerUsers = new List<ServerUser>() { serverUser1,serverUser2,serverUser3};
            //Act
            _userChatRoomAssistant.UpdateAllActiveServerUsers(allServerUsers);
            //Assert

        }
    }
}
