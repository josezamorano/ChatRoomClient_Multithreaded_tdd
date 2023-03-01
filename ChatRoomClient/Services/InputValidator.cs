using ChatRoomClient.Services.Models;
using ChatRoomClient.Utils.Interfaces;
using System.Net;

namespace ChatRoomClient.Services
{
    public class InputValidator : IInputValidator
    {
        ClientInputsValidationReport clientInputsValidationReport;

        //Tested
        public ClientInputsValidationReport ValidateClientConnectToServerInputs(ClientInputs clientInputs)
        {
            clientInputsValidationReport = new ClientInputsValidationReport();
            clientInputsValidationReport.InputsAreValid = true;

            clientInputsValidationReport.UsernameReport = ResolveUsername(clientInputs.Username.Trim());
            clientInputsValidationReport.IPAddressReport = ResolveIPAddress(clientInputs.IPAddress.Trim());
            clientInputsValidationReport.PortReport = ResolvePortNumberForClients(clientInputs.Port.Trim());

            if( !string.IsNullOrEmpty(clientInputsValidationReport.UsernameReport) ||
                !string.IsNullOrEmpty(clientInputsValidationReport.IPAddressReport) ||
                !string.IsNullOrEmpty(clientInputsValidationReport.PortReport))
            {
                clientInputsValidationReport.InputsAreValid = false;
            }

            return clientInputsValidationReport;
        }

        public ClientInputsValidationReport ValidateUserCreateChatRoomAndSendInvitesInputs(ClientInputs clientInputs)
        {
            clientInputsValidationReport= new ClientInputsValidationReport();
            clientInputsValidationReport.InputsAreValid= true;
            clientInputsValidationReport.ChatRoomNameReport = ResolveChatRoom(clientInputs.ChatRoomName.Trim());
            clientInputsValidationReport.GuestSelectorReport = ResolveGuestSelector(clientInputs.GuestSelectorStatus);

            if(!string.IsNullOrEmpty(clientInputsValidationReport.ChatRoomNameReport) ||
                !string.IsNullOrEmpty(clientInputsValidationReport.GuestSelectorReport)
                )
            {
                clientInputsValidationReport.InputsAreValid = false;
            }
            return clientInputsValidationReport;
        }


        #region Private Methods 

        private string ResolveUsername(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                return Notification.UsernameWarningInsert;
            }
            return string.Empty;
        }

        private string ResolveIPAddress(string ipAddress)
        {
            
            if (string.IsNullOrEmpty(ipAddress) || string.IsNullOrWhiteSpace(ipAddress))
            {
                return Notification.IPAddressWarningInsert;
            }

            var messageIPV4 = ResolveIPV4Address(ipAddress);
            var messageIPV6 = ResolveIPV6Address(ipAddress);
            if (!string.IsNullOrEmpty(messageIPV4) && !string.IsNullOrEmpty(messageIPV6))
            {
                return messageIPV4;
            }

            IPAddress defaultValue;
            var isValid = IPAddress.TryParse(ipAddress, out defaultValue);
            if (!isValid)
            {
                return Notification.IPAddressWarningInsert;
            }
            return string.Empty;
        }

        private string ResolveIPV4Address(string ipAddress)
        {
            string[] octets = ipAddress.Split('.');
            if (octets.Length != 4)
            {
                return Notification.IPAddressWarningInsert;
            }

            foreach (var octet in octets)
            {
                int defaultVal = 0;
                bool isValidOcted = Int32.TryParse(octet, out defaultVal);
                if (!isValidOcted || defaultVal < 0 || defaultVal > 255)
                {
                    return Notification.IPAddressWarningInsert;
                }
            }
            return string.Empty;
        }

        private string ResolveIPV6Address(string ipAddress)
        {
            string[] octets = ipAddress.Split(':');
            if(octets.Length != 8)
            {
                return Notification.IPAddressWarningInsert;
            }

            foreach(string octet in octets)
            {
                if(octet.Length != 4)
                {
                    return Notification.IPAddressWarningInsert;
                }
            }

            return string.Empty;
        }

        private string ResolvePortNumberForClients(string port)
        {
            int portNumber = 0;
            bool isValidNumber = int.TryParse(port, out portNumber);
            if (isValidNumber && portNumber >= 49152 && portNumber <= 65535)
            {
                return string.Empty;
            }

            return Notification.PortWarningInsert;
        }


        private string ResolveChatRoom(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                return Notification.ChatRoomWarning;
            }
            return string.Empty;
        }


        private string ResolveGuestSelector(bool statusIsActive)
        {
            if (!statusIsActive)
            {
                return Notification.GuestSelectorUnChecked;
            }
            return string.Empty;
        }

        #endregion Private Methods
    }
}
