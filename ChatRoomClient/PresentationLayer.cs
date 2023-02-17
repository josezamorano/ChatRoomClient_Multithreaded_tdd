using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services.Models;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;

namespace ChatRoomClient
{

    public delegate void ClientLogReportDelegate( string statusReport );

    public delegate void ClientConnectionReportDelegate( bool isConnected );

    public delegate void UsernameStatusReportDelegate( MessageActionType messageActionType );
    public partial class PresentationLayer : Form
    {
        private string _clientConnected;
        private string _clientDisconnected;
        private IClientManager _clientManager;
        private IInputValidator _inputValidator;
        public PresentationLayer(IClientManager clientManager, IInputValidator inputValidator)
        {
            InitializeComponent();
            _clientManager = clientManager;
            _inputValidator = inputValidator;
            _clientConnected = Enum.GetName(typeof(ClientStatus), ClientStatus.Connected);
            _clientDisconnected = Enum.GetName(typeof(ClientStatus), ClientStatus.Disconnected);            
        }

        #region Event Handlers
        private void WinFormOnLoad_Event(object sender, EventArgs e)
        {
            txtClientStatus.Text = _clientDisconnected;
            txtClientStatus.ForeColor= Color.Red;
            txtClientStatus.BackColor = txtClientStatus.BackColor;
            lblWarningUsername.Text = string.Empty;
            lblWarningIPAddress.Text = string.Empty;
            lblWarningPort.Text = string.Empty;
            lblUsernameStatus.Text = string.Empty;

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnUsernameRetry.Enabled = false;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            lblWarningUsername.Text = string.Empty;
            var filteredText = string.Concat(txtUsername.Text.Where(char.IsLetterOrDigit));
            txtUsername.Text = filteredText;
            
            txtUsernameChatRoom.Text = filteredText.Trim();
            txtUsernameChatRoom.BackColor= txtUsernameChatRoom .BackColor;
            txtUsernameChatRoom.ForeColor = Color.Blue;

        }

        private void txtServerIPAddress_TextChanged(object sender, EventArgs e)
        {
            lblWarningIPAddress.Text = string.Empty;
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            lblWarningPort.Text = string.Empty;
            var filteredText = string.Concat(txtPort.Text.Where(char.IsDigit));
            txtPort.Text = filteredText;
        }

        private void BtnClientConnectToServer_ClickEvent(object sender, EventArgs e)
        {
            bool isValid = RevolveClientInputValidation();
            if (!isValid) { return; }

            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            _clientManager.ConnectToServer(serverCommunicationInfo);
        }

        private void BtnClientDisconnectFromServer_ClickEvent(object sender, EventArgs e)
        {
            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);
            ClientConnectionReportDelegate connectionReportCallback = new ClientConnectionReportDelegate(ClientConnectionReportCallback);
            _clientManager.DisconnectFromServer(logReportCallback , connectionReportCallback);
        }

        private void btnUsernameRetry_Click(object sender, EventArgs e)
        {
            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            _clientManager.SendMessageToServer(serverCommunicationInfo);
        }
        #endregion Event Handlers

        #region Callbacks

        private void ClientLogReportCallback(string report)
        {
            Action action = () => {
                txtClientLog .Text += report;
                txtClientLog.AppendText(Environment.NewLine);
                txtClientLog.Refresh();
            };
            txtClientLog.BeginInvoke(action);
        }

        private void ClientConnectionReportCallback(bool clientIsConnected)
        {
            Action actionTxtClientStatus = () =>
            {               
                string clientStatus = (clientIsConnected) ? _clientConnected : _clientDisconnected;
                txtClientStatus.Text = clientStatus;
                txtClientStatus.ForeColor = (clientIsConnected) ? Color.Blue : Color.Red;
                txtClientStatus.BackColor = txtClientStatus.BackColor;
                txtClientStatus.Refresh();
            };
            txtClientStatus.BeginInvoke(actionTxtClientStatus);

            Action actionBtnConnect = () => 
            {
                btnConnect.Enabled = (clientIsConnected) ? false : true;
                btnConnect.Refresh();
            };
            btnConnect.BeginInvoke(actionBtnConnect);

            Action actionBtnDisconnect = () =>
            {
                btnDisconnect.Enabled = (clientIsConnected) ? true : false;
                btnDisconnect.Refresh();
            };
            btnDisconnect.BeginInvoke(actionBtnDisconnect);

            Action actionBtnUsernameRetry = () =>
            {
                btnUsernameRetry.Enabled = (clientIsConnected) ? true : false;
                btnUsernameRetry.Refresh();
            };
            btnDisconnect.BeginInvoke(actionBtnUsernameRetry);
        }

        private void UsernameActivationStatusCallback(MessageActionType messageActionType)
        {
            if(messageActionType == MessageActionType.RetryUsernameTaken)
            {
                Action actionLblUsernameStatus = GetActionLblUsernameStatus("Username taken, please retry", Color.Red);
                lblUsernameStatus.BeginInvoke(actionLblUsernameStatus);

                Action actionBtnUsernameRetry = GetActionButtonRetry(true);
                btnUsernameRetry.BeginInvoke(actionBtnUsernameRetry);
            }

            else if (messageActionType == MessageActionType.RetryUsernameError)
            {
                Action actionLblUsernameStatus = GetActionLblUsernameStatus("Username Error, please retry", Color.Red);
                lblUsernameStatus.BeginInvoke(actionLblUsernameStatus);

                Action actionBtnUsernameRetry = GetActionButtonRetry(true);
                btnUsernameRetry.BeginInvoke(actionBtnUsernameRetry);
            }

            else if (messageActionType == MessageActionType.UserActivated)
            {
                Action actionLblUsernameStatus = GetActionLblUsernameStatus("Username Activated", Color.Blue);
                lblUsernameStatus.BeginInvoke(actionLblUsernameStatus);

                Action actionBtnUsernameRetry = GetActionButtonRetry(false);
                btnUsernameRetry.BeginInvoke(actionBtnUsernameRetry);
            }
        }

        private Action GetActionLblUsernameStatus(string text, Color color) 
        {
            Action actionLblUsernameStatus = () =>
            {
                lblUsernameStatus.Text = text;
                lblUsernameStatus.BackColor = lblUsernameStatus.BackColor;
                lblUsernameStatus.ForeColor = color;
                lblUsernameStatus.Refresh();
            };
            return actionLblUsernameStatus;
        }

        private Action GetActionButtonRetry(bool enabled)
        {
            Action actionBtnUsernameRetry = () =>
            {
                btnUsernameRetry.Enabled = enabled;
                btnUsernameRetry.Refresh();
            };
            return actionBtnUsernameRetry;
        }
        #endregion Callbacks


        #region Private Methods 

        private ServerCommunicationInfo CreateServerCommunicationInfo()
        {
            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);
            ClientConnectionReportDelegate connectionReportCallback = new ClientConnectionReportDelegate(ClientConnectionReportCallback);
            UsernameStatusReportDelegate usernameStatusReportCallback = new UsernameStatusReportDelegate(UsernameActivationStatusCallback);

            ServerCommunicationInfo serverCommunicationInfo = new ServerCommunicationInfo()
            {
                IPAddress = txtServerIPAddress.Text.Trim(),
                Port = Int32.Parse(txtPort.Text.Trim()),
                Username = txtUsername.Text.Trim(),
                LogReportCallback = logReportCallback,
                ConnectionReportCallback = connectionReportCallback,
                UsernameStatusReportCallback = usernameStatusReportCallback
            };
            return serverCommunicationInfo;
        }

        private bool RevolveClientInputValidation()
        {
            lblWarningUsername.Text = string.Empty;
            lblWarningIPAddress.Text = string.Empty;
            lblWarningPort.Text = string.Empty;

            txtUsername.Text = txtUsername.Text.Trim();
            txtServerIPAddress.Text = txtServerIPAddress.Text.Trim();
            txtPort.Text = txtPort.Text.Trim();
            
            ClientInputs clientInputs = new ClientInputs() 
            {
                Username = txtUsername.Text,
                IPAddress = txtServerIPAddress.Text,
                Port = txtPort.Text,
            };
            var inputsReport = _inputValidator.ValidateClientInputs(clientInputs);
            if (!inputsReport.InputsAreValid)
            {
                lblWarningUsername.Text = inputsReport.UsernameReport;
                lblWarningUsername.BackColor = lblWarningUsername.BackColor;
                lblWarningUsername.ForeColor = Color.Red;
                lblWarningUsername.Refresh();
                
                lblWarningIPAddress.Text = inputsReport.IPAddressReport;
                lblWarningIPAddress.BackColor = lblWarningIPAddress.BackColor;
                lblWarningIPAddress.ForeColor = Color.Red;
                lblWarningIPAddress.Refresh();
                
                lblWarningPort.Text = inputsReport.PortReport;
                lblWarningPort.BackColor = lblWarningPort.BackColor;
                lblWarningPort.ForeColor = Color.Red;
                lblWarningPort.Refresh();
            }
            return inputsReport.InputsAreValid;
        }


        #endregion Private Methods

       
    }
}