using ChatRoomClient.Services.Models;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface IInputValidator
    {
        ClientInputsValidationReport ValidateClientInputs(ClientInputs clientInputs);
    }
}
