using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services.Models;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;
using System.Data;
using System.Windows.Forms;

namespace ChatRoomClient
{

    public delegate void ClientLogReportDelegate(string statusReport);

    public delegate void ClientConnectionReportDelegate(bool isConnected);

    public delegate void UsernameStatusReportDelegate(MessageActionType messageActionType);

    public delegate void OtherServerUsersReportDelegate(List<ServerUser> otherActiveUsers);

    public delegate void ChatRoomUpdateDelegate(List<ChatRoom> allActiveChatrooms);
    public partial class PresentationLayer : Form
    {
        private string _clientConnected;
        private string _clientDisconnected;

        private List<ServerUser> _otherActiveUsers;
        private TableLayoutPanel _tlpCanvas;


        private IClientManager _clientManager;
        private IUserChatRoomAssistant _userChatRoomAssistantInstance;
        private IUser _user;
        private IInputValidator _inputValidator;
        public PresentationLayer(IClientManager clientManager, IInputValidator inputValidator, IUserChatRoomAssistant userChatRoomAssistant, IUser user)
        {
            InitializeComponent();
            _otherActiveUsers = new List<ServerUser>();
            _tlpCanvas = new TableLayoutPanel();
            _clientManager = clientManager;
            _inputValidator = inputValidator;
            _clientConnected = Enum.GetName(typeof(ClientStatus), ClientStatus.Connected);
            _clientDisconnected = Enum.GetName(typeof(ClientStatus), ClientStatus.Disconnected);
            _userChatRoomAssistantInstance = userChatRoomAssistant.GetInstance();
            _user = user;
        }

        #region Event Handlers
        private void WinFormOnLoad_Event(object sender, EventArgs e)
        {
            txtClientStatus.Text = _clientDisconnected;
            txtClientStatus.ForeColor = Color.Red;
            txtClientStatus.BackColor = txtClientStatus.BackColor;

            lblWarningUsername.Text = string.Empty;
            lblWarningIPAddress.Text = string.Empty;
            lblWarningPort.Text = string.Empty;
            lblWarningChatRoomName.Text = string.Empty;
            lblWarningGuests.Text = string.Empty;

            lblUsernameStatus.Text = string.Empty;

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnUsernameRetry.Enabled = false;
            btnCreateChatRoomSendInvites.Enabled = false;

            chkBoxAddGuests.Enabled = false;

            ChatRoomUpdateDelegate chatRoomUpdateCallback = new ChatRoomUpdateDelegate(ChatRoomUpdate_ThreadCallback);
            _userChatRoomAssistantInstance.SetChatRoomUpdateCallback(chatRoomUpdateCallback);
            //var chatRooms = GetAllChatRoomsTest();
            //ChatRoomUpdate_ThreadCallback(chatRooms);
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            lblWarningUsername.Text = string.Empty;
            var filteredText = string.Concat(txtUsername.Text.Where(char.IsLetterOrDigit));
            txtUsername.Text = filteredText;

            txtUsernameChatRoom.Text = filteredText.Trim();
            txtUsernameChatRoom.BackColor = txtUsernameChatRoom.BackColor;
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

        private void txtChatRoomName_TextChanged(object sender, EventArgs e)
        {
            lblWarningChatRoomName.Text = string.Empty;
            var filteredText = string.Concat(txtChatRoomName.Text.Where(char.IsLetterOrDigit));
            txtChatRoomName.Text = filteredText;
        }

        private void chkBoxAddGuests_CheckedChanged(object sender, EventArgs e)
        {
            checkedListServerUsers.Enabled = (chkBoxAddGuests.Checked) ? true : false;

            if (!chkBoxAddGuests.Checked)
            {
                for (int i = 0; i < checkedListServerUsers.Items.Count; i++)
                {
                    checkedListServerUsers.SetItemCheckState(i, (chkBoxAddGuests.Checked ? CheckState.Checked : CheckState.Unchecked));
                }


                listBoxAllGuests.Items.Clear();
            }
        }

        private void checkedListServerUsers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string item = checkedListServerUsers.Items[e.Index].ToString();
            if (e.NewValue == CheckState.Checked)
            {
                listBoxAllGuests.Items.Add(item);
            }
            else
            {
                listBoxAllGuests.Items.Remove(item);
            }
        }


        //BUTTONS Begin*************
        private void BtnClientConnectToServer_ClickEvent(object sender, EventArgs e)
        {
            bool isValid = RevolveClientConnectToServerInputValidation();
            if (!isValid) { return; }

            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            _clientManager.ConnectToServer(serverCommunicationInfo);
        }

        private void BtnClientDisconnectFromServer_ClickEvent(object sender, EventArgs e)
        {
            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);
            ClientConnectionReportDelegate connectionReportCallback = new ClientConnectionReportDelegate(ClientConnectionReportCallback);
            _clientManager.DisconnectFromServer(logReportCallback, connectionReportCallback);
        }

        private void BtnUsernameRetry_ClickEvent(object sender, EventArgs e)
        {
            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            _clientManager.SendMessageToServer(serverCommunicationInfo);
        }

        private void BtnCreateChatRoomAndSendInvite_ClickEvent(object sender, EventArgs e)
        {
            bool isValid = ResolveUserCreateChatRoomAndSendInviteInputValidation();
            if (!isValid) { return; }

            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            _userChatRoomAssistantInstance.CreateChatRoomAndSendInvites(serverCommunicationInfo);

        }

        //BUTTONS End***************
        #endregion Event Handlers

        #region Callbacks

        private void ClientLogReportCallback(string report)
        {
            Action action = () =>
            {
                txtClientLog.Text += report;
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

            Action actionLblUsernameStatus = () =>
            {
                lblUsernameStatus.Text = (clientIsConnected) ? lblUsernameStatus.Text : string.Empty;
            };
            lblUsernameStatus.BeginInvoke(actionLblUsernameStatus);

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

            Action actionCheckedList = () =>
            {
                if (!clientIsConnected)
                {
                    checkedListServerUsers.Items.Clear();
                }
            };
            checkedListServerUsers.BeginInvoke(actionCheckedList);

            Action actionChkBoxAddGuest = () =>
            {
                chkBoxAddGuests.Enabled = (clientIsConnected) ? true : false;
            };
            chkBoxAddGuests.BeginInvoke(actionChkBoxAddGuest);

            Action actionBtnCreateChatRoom = () =>
            {
                btnCreateChatRoomSendInvites.Enabled = (clientIsConnected) ? true : false;
            };
            btnCreateChatRoomSendInvites.BeginInvoke(actionBtnCreateChatRoom);
        }

        private void UsernameActivationStatusCallback(MessageActionType messageActionType)
        {
            if (messageActionType == MessageActionType.RetryUsernameTaken)
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


        private void DisplayOtherActiveUsersCallback(List<ServerUser> otherActiveUsers)
        {

            _otherActiveUsers = otherActiveUsers;
            if (otherActiveUsers == null) return;
            string[] usernameOtherActiveUsers = otherActiveUsers.Select(a => a.Username).ToArray();
            Action actionCheckedList = () =>
            {
                string[] testValues = { "userJoe", "UserTom", "UserPete" };

                checkedListServerUsers.Items.Clear();
                checkedListServerUsers.Items.AddRange(testValues);
                checkedListServerUsers.Items.AddRange(usernameOtherActiveUsers);
                checkedListServerUsers.Enabled = false;
                checkedListServerUsers.Refresh();
            };

            checkedListServerUsers.BeginInvoke(actionCheckedList);

        }
        #endregion Callbacks


        #region Private Methods 

        private ServerCommunicationInfo CreateServerCommunicationInfo()
        {
            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);
            ClientConnectionReportDelegate connectionReportCallback = new ClientConnectionReportDelegate(ClientConnectionReportCallback);
            UsernameStatusReportDelegate usernameStatusReportCallback = new UsernameStatusReportDelegate(UsernameActivationStatusCallback);
            OtherServerUsersReportDelegate otherServerUsersReportCallback = new OtherServerUsersReportDelegate(DisplayOtherActiveUsersCallback);

            ServerCommunicationInfo serverCommunicationInfo = new ServerCommunicationInfo()
            {
                IPAddress = txtServerIPAddress.Text.Trim(),
                Port = Int32.Parse(txtPort.Text.Trim()),
                Username = txtUsername.Text.Trim(),
                ChatRoomName = txtChatRoomName.Text.Trim(),
                SelectedGuestUsers = GetAllSelectedServerUsers(),
                LogReportCallback = logReportCallback,
                ConnectionReportCallback = connectionReportCallback,
                UsernameStatusReportCallback = usernameStatusReportCallback,
                OtherServerUsersReportCallback = otherServerUsersReportCallback
            };
            return serverCommunicationInfo;
        }

        private bool RevolveClientConnectToServerInputValidation()
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
            var inputsReport = _inputValidator.ValidateClientConnectToServerInputs(clientInputs);
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

        private bool ResolveUserCreateChatRoomAndSendInviteInputValidation()
        {
            lblWarningChatRoomName.Text = string.Empty;
            lblWarningGuests.Text = string.Empty;

            ClientInputs clientInputs = new ClientInputs()
            {
                ChatRoomName = txtChatRoomName.Text.Trim(),
                GuestSelectorStatus = chkBoxAddGuests.Checked,
            };
            ClientInputsValidationReport report = _inputValidator.ValidateUserCreateChatRoomAndSendInvitesInputs(clientInputs);

            if (!report.InputsAreValid)
            {
                lblWarningChatRoomName.Text = report.ChatRoomNameReport;
                lblWarningChatRoomName.ForeColor = Color.Red;
                lblWarningChatRoomName.BackColor = lblWarningChatRoomName.BackColor;
                lblWarningChatRoomName.Refresh();

                lblWarningGuests.Text = report.GuestSelectorReport;
                lblWarningGuests.BackColor = lblWarningGuests.BackColor;
                lblWarningGuests.ForeColor = Color.Red;
                lblWarningGuests.Refresh();
            }
            return report.InputsAreValid;

        }

        private List<ServerUser> GetAllSelectedServerUsers()
        {
            List<ServerUser> selectedUsers = new List<ServerUser>();
            foreach (var item in listBoxAllGuests.Items)
            {
                string itemUsernameLower = item.ToString().ToLower();
                var selectedUser = _otherActiveUsers.Where(a => a.Username.ToLower() == itemUsernameLower).FirstOrDefault();
                if (selectedUser != null)
                {
                    selectedUsers.Add(selectedUser);
                }
            }
            return selectedUsers;
        }


        #endregion Private Methods


        #region Dynamic Controls

        private void ChatRoomUpdate_ThreadCallback(List<ChatRoom> allActiveChatRooms)
        {
            Thread threadChatRoomUpdateEvent = new Thread(() =>
            {
                if (allActiveChatRooms.Count > 0)
                {
                    ResolveChatRoomDynamicControl(allActiveChatRooms);
                }
            });
            threadChatRoomUpdateEvent.Name = "threadChatRoomUpdateEvent";
            threadChatRoomUpdateEvent.IsBackground = true;
            threadChatRoomUpdateEvent.Start();

        }

        private List<ChatRoom> GetAllChatRoomsTest()
        {
            List<ChatRoom> allChatRooms = new List<ChatRoom>();
            ChatRoom chatRoom1 = new ChatRoom()
            {
                ChatRoomIdentifierNameId = "abc-111",
                ChatRoomStatus = ChatRoomStatus.OpenActive,
                AllActiveUsersInChatRoom = new List<ServerUser> { new ServerUser() { Username = "abc" }, new ServerUser() { Username = "mno" }, new ServerUser() { Username = "Jonathan" }, new ServerUser() { Username = "Surex" } },
                AllInvitesSentToGuestUsers = new List<Invite> { new Invite() { GuestServerUser = new ServerUser() { Username = "TOM" }, InviteStatus = InviteStatus.Accepted }, }

            };
            ChatRoom chatRoom2 = new ChatRoom()
            {
                ChatRoomIdentifierNameId = "abc-112",
                ChatRoomStatus = ChatRoomStatus.OpenActive,
                AllActiveUsersInChatRoom = new List<ServerUser> { new ServerUser() { Username = "abc1" }, new ServerUser() { Username = "mno" } },
                AllInvitesSentToGuestUsers = new List<Invite> { new Invite() { GuestServerUser = new ServerUser() { Username = "Mark" }, InviteStatus = InviteStatus.SentPendingResponse } }

            };
            ChatRoom chatRoom3 = new ChatRoom()
            {
                ChatRoomIdentifierNameId = "abc-113",
                ChatRoomStatus = ChatRoomStatus.OpenActive,
                AllActiveUsersInChatRoom = new List<ServerUser> { new ServerUser() { Username = "abc2" }, new ServerUser() { Username = "mno" } },
                AllInvitesSentToGuestUsers = new List<Invite> { new Invite() { GuestServerUser = new ServerUser() { Username = "Pam" }, InviteStatus = InviteStatus.SentPendingResponse }, }

            };
            ChatRoom chatRoom4 = new ChatRoom()
            {
                ChatRoomIdentifierNameId = "abc-114",
                ChatRoomStatus = ChatRoomStatus.OpenActive,
                AllActiveUsersInChatRoom = new List<ServerUser> { new ServerUser() { Username = "abc3" }, new ServerUser() { Username = "mno" } },
                AllInvitesSentToGuestUsers = new List<Invite> { new Invite() { GuestServerUser = new ServerUser() { Username = "Jack" }, InviteStatus = InviteStatus.SentPendingResponse }, }

            };
            allChatRooms.Add(chatRoom1);
            //allChatRooms.Add(chatRoom2);
            //allChatRooms.Add(chatRoom3);
            //allChatRooms.Add(chatRoom4);
            return allChatRooms;
        }


        public void ResolveChatRoomDynamicControl(List<ChatRoom> chatRooms)
        {
            Action actionUpdate = () =>
            {
                if (_tlpCanvas.Controls.Count > 0)
                {
                    _tlpCanvas.Controls.Clear();
                }
                int canvasWidth = 430;
                _tlpCanvas.Dock = DockStyle.Fill;
                _tlpCanvas.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                _tlpCanvas.ColumnCount = 1;
                _tlpCanvas.RowCount = 1;
                _tlpCanvas.BackColor = Color.White;
                _tlpCanvas.SetRowSpan(_tlpCanvas, 3);

                _tlpCanvas.HorizontalScroll.Maximum = 0;
                _tlpCanvas.HorizontalScroll.Visible = false;
                _tlpCanvas.AutoScroll = false;
                _tlpCanvas.VerticalScroll.Visible = true;
                _tlpCanvas.AutoScroll = true;

                for (var a = 0; a < chatRooms.Count; a++)
                {
                    var activeUsersInChatRoom = chatRooms[a].AllActiveUsersInChatRoom.Count.ToString();
                    var chatRoomIdentifier = chatRooms[a].ChatRoomIdentifierNameId;
                    string[] allActiveUsers = chatRooms[a].AllActiveUsersInChatRoom.Select(a => a.Username).ToArray();
                    string conversationRecord = chatRooms[a].ConversationRecord;
                    string controlId = chatRoomIdentifier + "_" + a;

                    var tlpRow = new TableLayoutPanel();
                    tlpRow.Name = controlId;
                    tlpRow.Height = 300;
                    //tlpRow.Dock = DockStyle.Fill;
                    tlpRow.Width = canvasWidth;
                    tlpRow.BackColor = Color.LightGray;
                    tlpRow.ColumnCount = 3;
                    tlpRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
                    tlpRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
                    tlpRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));

                    tlpRow.RowCount = 5;
                    tlpRow.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                    tlpRow.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                    tlpRow.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                    tlpRow.RowStyles.Add(new RowStyle(SizeType.Absolute, 190F));
                    tlpRow.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

                    tlpRow.Controls.Add(new Label() { Text = "Identifier:", BorderStyle = BorderStyle.FixedSingle, Width = 80, TextAlign = ContentAlignment.MiddleRight }, 0, 0);
                    tlpRow.Controls.Add(new Label() { Text = "Users:", BorderStyle = BorderStyle.FixedSingle, Width = 80, TextAlign = ContentAlignment.MiddleRight }, 0, 1);
                    tlpRow.Controls.Add(new Label() { Text = activeUsersInChatRoom, BorderStyle = BorderStyle.FixedSingle, Width = 80, TextAlign = ContentAlignment.MiddleCenter }, 0, 2);
                    tlpRow.Controls.Add(new Label() { Text = chatRoomIdentifier, Enabled = false, Width = 250, BorderStyle = BorderStyle.FixedSingle }, 1, 0);
                    ListBox activeUsers = new ListBox() { Enabled = true, Width = 350, BackColor = SystemColors.Control, BorderStyle = BorderStyle.Fixed3D };
                    activeUsers.Items.AddRange(allActiveUsers);
                    tlpRow.SetRowSpan(activeUsers, 2);
                    tlpRow.Controls.Add(activeUsers, 1, 1);

                    Button buttonExitChatroom = new Button();
                    buttonExitChatroom.Name = controlId;
                    buttonExitChatroom.Text = "Exit Chat Room";
                    buttonExitChatroom.Dock = DockStyle.Fill;
                    buttonExitChatroom.Click += ButtonExitChatroom_Click;
                    buttonExitChatroom.Width = 50;
                    tlpRow.SetRowSpan(buttonExitChatroom, 3);
                    tlpRow.Controls.Add(buttonExitChatroom, 3, 0);


                    TextBox chatRoomConversation = new TextBox();
                    chatRoomConversation.Dock = DockStyle.Fill;
                    chatRoomConversation.Enabled = false;
                    chatRoomConversation.Multiline = true;
                    chatRoomConversation.Text = conversationRecord;
                    tlpRow.SetColumnSpan(chatRoomConversation, 3);
                    tlpRow.Controls.Add(chatRoomConversation, 0, 3);

                    TextBox message = new TextBox();
                    message.Dock = DockStyle.Fill;
                    message.Name = controlId;
                    tlpRow.SetColumnSpan(message, 2);
                    tlpRow.Controls.Add(message, 0, 4);

                    Button buttonSendMessage = new Button();
                    buttonSendMessage.Name = controlId;
                    buttonSendMessage.Text = "Send";
                    buttonSendMessage.Dock = DockStyle.Fill;
                    buttonSendMessage.Click += ButtonSendMessage_Click;

                    _tlpCanvas.SetColumnSpan(tlpRow, 3);
                    tlpRow.Controls.Add(buttonSendMessage, 2, 4);


                    _tlpCanvas.Controls.Add(tlpRow, 0, a);
                }

                tlpBase.Controls.Add(_tlpCanvas, 2, 0);
            };
            this.tlpBase.BeginInvoke(actionUpdate);
        }

        private void ButtonSendMessage_Click(object? sender, EventArgs e)
        {
            var buttonSendMessage = (Button)sender;
            var buttonSendMessageId = buttonSendMessage.Name;
            bool messageSent = false;
            foreach (var control in this._tlpCanvas.Controls)
            {
                if (control is TableLayoutPanel)
                {
                    TableLayoutPanel tlp = (TableLayoutPanel)control;
                    string tlpId = tlp.Name;
                    if (tlpId == buttonSendMessageId)
                    {
                        foreach (var itemControl in tlp.Controls)
                        {
                            if (itemControl is TextBox)
                            {
                                TextBox textBoxMessage = (TextBox)itemControl;
                                if (textBoxMessage.Name == buttonSendMessageId && !string.IsNullOrEmpty(textBoxMessage.Text))
                                {
                                    string[] chatRoomIdentifierArray = textBoxMessage.Name.Split('_');
                                    string chatRoomName = (chatRoomIdentifierArray.Length > 0) ? chatRoomIdentifierArray[0] : string.Empty;
                                    Guid chatRoomId = (chatRoomIdentifierArray.Length > 1) ? new Guid(chatRoomIdentifierArray[1]) : Guid.Empty;
                                    //Send message
                                    ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
                                    serverCommunicationInfo.ChatRoomName = chatRoomName;
                                    serverCommunicationInfo.ChatRoomId = chatRoomId;
                                    serverCommunicationInfo.MessageToChatRoom = textBoxMessage.Text;
                                    _user.SendMessageToChatRoom(serverCommunicationInfo);


                                    textBoxMessage.Text = string.Empty;
                                    messageSent = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (messageSent)
                {
                    break;
                }
            }
        }

        private void ButtonExitChatroom_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var info = button.Name;
            var value = "stop here";

        }
        #endregion Dynamic Controls
    }
}