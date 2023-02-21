using ChatRoomClient.Services.Models;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface IInputValidator
    {
        ClientInputsValidationReport ValidateClientConnectToServerInputs(ClientInputs clientInputs);

        ClientInputsValidationReport ValidateUserCreateChatRoomAndSendInvitesInputs(ClientInputs clientInputs);
    }
}
