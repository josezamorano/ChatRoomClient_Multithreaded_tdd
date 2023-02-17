
namespace ChatRoomClient.Services.Models
{
    public class ClientInputsValidationReport
    {
        public bool InputsAreValid { get; set; }

        public string UsernameReport { get; set; }

        public string IPAddressReport { get; set; }

        public string PortReport { get; set; }
    }
}
