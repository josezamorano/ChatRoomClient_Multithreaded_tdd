using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services.Models;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Extensions;
using ChatRoomClient.Utils.Interfaces;
using System.Data;



namespace ChatRoomClient
{

    public delegate void ClientLogReportDelegate(string statusReport);

    public delegate void ClientConnectionReportDelegate(bool isConnected);

    public delegate void UsernameStatusReportDelegate(MessageActionType messageActionType);

    public delegate void OtherActiveServerUsersUpdateDelegate(List<ServerUser> otherActiveUsers);

    public delegate void ChatRoomUpdateDelegate(List<ChatRoom> allActiveChatrooms);

    public delegate void InviteUpdateDelegate(List<ControlInvite> allPendingInvites);
    public partial class PresentationLayer : Form
    {
        private string _clientConnected;
        private string _clientDisconnected;

        private List<ServerUser> _otherActiveUsers;
        private TableLayoutPanel _tlpChatRoomCanvas;
        private TableLayoutPanel _tlpInviteCanvas;


        private IClientManager _clientManager;
        private IUserChatRoomAssistant _userChatRoomAssistantInstance;
        private IUser _user;
        private IInputValidator _inputValidator;
        public PresentationLayer(IClientManager clientManager, IInputValidator inputValidator, IUserChatRoomAssistant userChatRoomAssistant, IUser user)
        {
            InitializeComponent();
            _otherActiveUsers = new List<ServerUser>();
            _tlpChatRoomCanvas = new TableLayoutPanel();
            _tlpInviteCanvas = new TableLayoutPanel();
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

            OtherActiveServerUsersUpdateDelegate otherActiveServerUsersUpdateCallback = new OtherActiveServerUsersUpdateDelegate(DisplayOtherActiveUsersCallback);
            _userChatRoomAssistantInstance.SetOtherActiveServerUsersUpdate(otherActiveServerUsersUpdateCallback);

            ChatRoomUpdateDelegate chatRoomUpdateCallback = new ChatRoomUpdateDelegate(ChatRoomUpdate_ThreadCallback);
            _userChatRoomAssistantInstance.SetChatRoomUpdateCallback(chatRoomUpdateCallback);

            //TESTING ONLY
            //var chatRooms = GetAllChatRoomsTest();
            //ChatRoomUpdate_ThreadCallback(chatRooms);

            CreateInviteCanvasDynamicControl();

            InviteUpdateDelegate inviteUpdateCallback = new InviteUpdateDelegate(InviteDisplay_ThreadCallback);
            _userChatRoomAssistantInstance.SetInviteUpdateCallback(inviteUpdateCallback);

            //TESTING ONLY
            //var allInvites = GetAllControlInvitesTEST();
            //InviteDisplay_ThreadCallback(allInvites);


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
            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            _clientManager.DisconnectFromServer(serverCommunicationInfo);
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
                //string[] testValues = { "userJoe", "UserTom", "UserPete" };

                checkedListServerUsers.Items.Clear();
                //checkedListServerUsers.Items.AddRange(testValues);
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

            ServerCommunicationInfo serverCommunicationInfo = new ServerCommunicationInfo()
            {
                IPAddress = txtServerIPAddress.Text.Trim(),
                Port = Int32.Parse(txtPort.Text.Trim()),
                Username = txtUsername.Text.Trim(),
                ChatRoomName = txtChatRoomName.Text.Trim(),
                SelectedGuestUsers = GetAllSelectedServerUsers(),
                LogReportCallback = logReportCallback,
                ConnectionReportCallback = connectionReportCallback,
                UsernameStatusReportCallback = usernameStatusReportCallback
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
                if (allActiveChatRooms.Count >= 0)
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
                AllInvitesSentToGuestUsers = new List<Invite> { new Invite() { GuestServerUser = new ServerUser() { Username = "Mark" }, InviteStatus = InviteStatus.SentAndPendingResponse } }

            };
            ChatRoom chatRoom3 = new ChatRoom()
            {
                ChatRoomIdentifierNameId = "abc-113",
                ChatRoomStatus = ChatRoomStatus.OpenActive,
                AllActiveUsersInChatRoom = new List<ServerUser> { new ServerUser() { Username = "abc2" }, new ServerUser() { Username = "mno" } },
                AllInvitesSentToGuestUsers = new List<Invite> { new Invite() { GuestServerUser = new ServerUser() { Username = "Pam" }, InviteStatus = InviteStatus.SentAndPendingResponse }, }

            };
            ChatRoom chatRoom4 = new ChatRoom()
            {
                ChatRoomIdentifierNameId = "abc-114",
                ChatRoomStatus = ChatRoomStatus.OpenActive,
                AllActiveUsersInChatRoom = new List<ServerUser> { new ServerUser() { Username = "abc3" }, new ServerUser() { Username = "mno" } },
                AllInvitesSentToGuestUsers = new List<Invite> { new Invite() { GuestServerUser = new ServerUser() { Username = "Jack" }, InviteStatus = InviteStatus.SentAndPendingResponse }, }

            };
            allChatRooms.Add(chatRoom1);
            //allChatRooms.Add(chatRoom2);
            //allChatRooms.Add(chatRoom3);
            //allChatRooms.Add(chatRoom4);
            return allChatRooms;
        }


        public void ResolveChatRoomDynamicControl(List<ChatRoom> chatRooms)
        {
            Action actionChatRoomUpdate = () =>
            {
                if (_tlpChatRoomCanvas.Controls.Count >= 0)
                {
                    _tlpChatRoomCanvas.Controls.Clear();
                }
                int canvasWidth = 430;
                _tlpChatRoomCanvas.Dock = DockStyle.Fill;
                _tlpChatRoomCanvas.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                _tlpChatRoomCanvas.ColumnCount = 1;
                _tlpChatRoomCanvas.RowCount = 1;
                _tlpChatRoomCanvas.BackColor = Color.White;
                _tlpChatRoomCanvas.SetRowSpan(_tlpChatRoomCanvas, 3);

                _tlpChatRoomCanvas.HorizontalScroll.Maximum = 0;
                _tlpChatRoomCanvas.HorizontalScroll.Visible = false;
                _tlpChatRoomCanvas.AutoScroll = false;
                _tlpChatRoomCanvas.VerticalScroll.Visible = true;
                _tlpChatRoomCanvas.AutoScroll = true;

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

                    _tlpChatRoomCanvas.SetColumnSpan(tlpRow, 3);
                    tlpRow.Controls.Add(buttonSendMessage, 2, 4);


                    _tlpChatRoomCanvas.Controls.Add(tlpRow, 0, a);
                }

                tlpBase.Controls.Add(_tlpChatRoomCanvas, 2, 0);
            };
            this.tlpBase.BeginInvoke(actionChatRoomUpdate);
        }

        private void ButtonSendMessage_Click(object? sender, EventArgs e)
        {
            var buttonSendMessage = (Button)sender;

            ServerCommunicationInfo serverCommunicationInfo = GetChatRoomInfoFromControls(buttonSendMessage);
            if (!string.IsNullOrEmpty(serverCommunicationInfo.MessageToChatRoom))
            {
                _user.SendMessageToChatRoom(serverCommunicationInfo);
            }

        }

        private void ButtonExitChatroom_Click(object sender, EventArgs e)
        {
            //PENDING
            var buttonExitChatRoom = (Button)sender;
            var info = buttonExitChatRoom.Name;
            ServerCommunicationInfo serverCommunicationInfo = GetChatRoomInfoFromControls(buttonExitChatRoom);
            _user.ExitChatRoom(serverCommunicationInfo);
        }

        private ServerCommunicationInfo GetChatRoomInfoFromControls(Button buttonControl)
        {
            var buttonId = buttonControl.Name;
            foreach (var control in this._tlpChatRoomCanvas.Controls)
            {
                if (control is TableLayoutPanel)
                {
                    TableLayoutPanel tlp = (TableLayoutPanel)control;
                    string tlpId = tlp.Name;
                    if (tlpId == buttonId)
                    {
                        foreach (var itemControl in tlp.Controls)
                        {
                            if (itemControl is TextBox)
                            {
                                TextBox textBoxMessage = (TextBox)itemControl;
                                if (textBoxMessage.Name == buttonId)
                                {
                                    string[] chatRoomIdentifierArray = textBoxMessage.Name.Split('_');
                                    string chatRoomName = (chatRoomIdentifierArray.Length > 0) ? chatRoomIdentifierArray[0] : string.Empty;
                                    Guid chatRoomId = (chatRoomIdentifierArray.Length > 1) ? new Guid(chatRoomIdentifierArray[1]) : Guid.Empty;
                                    ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
                                    serverCommunicationInfo.ChatRoomName = chatRoomName;
                                    serverCommunicationInfo.ChatRoomId = chatRoomId;
                                    serverCommunicationInfo.MessageToChatRoom = textBoxMessage.Text;
                                    return serverCommunicationInfo;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        private void InviteDisplay_ThreadCallback(List<ControlInvite> allPendingInvites)
        {
            Thread threadInviteDisplayEvent = new Thread(() =>
            {
                if (allPendingInvites.Count > 0)
                {
                    List<ControlInvite> allInvitesPendingResolution = allPendingInvites.Where(a => a.ControlActionType != ControlActionType.Read).ToList();
                    foreach (ControlInvite pendingInvite in allInvitesPendingResolution)
                    {
                        ResolveInviteDynamicControl(pendingInvite);
                    }

                    List<ControlInvite> allInvitesForDeletion = allPendingInvites.Where(a => a.ControlActionType == ControlActionType.Delete).ToList();

                    allPendingInvites.RemoveAllExtension(allInvitesForDeletion);

                }
            });

            threadInviteDisplayEvent.Name = "threadInviteDisplayEvent";
            threadInviteDisplayEvent.IsBackground = true;
            threadInviteDisplayEvent.Start();

        }
        private List<ControlInvite> GetAllControlInvitesTEST()
        {
            List<ControlInvite> allControlInvites = new List<ControlInvite>();

            ControlInvite invite1 = new ControlInvite()
            {
                ControlActionType = ControlActionType.Create,
                InviteObject = new Invite()
                {
                    InviteId = Guid.NewGuid(),
                    ChatRoomCreator = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "USER Joe"
                    },
                    ChatRoomId = Guid.NewGuid(),
                    ChatRoomName = "Test 1",
                    GuestServerUser = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "Main User"
                    },
                    InviteStatus = InviteStatus.SentAndPendingResponse

                }
            };

            ControlInvite invite2 = new ControlInvite()
            {
                ControlActionType = ControlActionType.Create,
                InviteObject = new Invite()
                {
                    InviteId = Guid.NewGuid(),
                    ChatRoomCreator = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "USER Joe"
                    },
                    ChatRoomId = Guid.NewGuid(),
                    ChatRoomName = "Test 1",
                    GuestServerUser = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "Main User"
                    },
                    InviteStatus = InviteStatus.SentAndPendingResponse

                }
            };

            ControlInvite invite3 = new ControlInvite()
            {
                ControlActionType = ControlActionType.Create,
                InviteObject = new Invite()
                {
                    InviteId = Guid.NewGuid(),
                    ChatRoomCreator = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "USER Joe"
                    },
                    ChatRoomId = Guid.NewGuid(),
                    ChatRoomName = "Test 1",
                    GuestServerUser = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "Main User"
                    },
                    InviteStatus = InviteStatus.SentAndPendingResponse

                }
            };

            ControlInvite invite4 = new ControlInvite()
            {
                ControlActionType = ControlActionType.Create,
                InviteObject = new Invite()
                {
                    InviteId = Guid.NewGuid(),
                    ChatRoomCreator = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "USER Joe"
                    },
                    ChatRoomId = Guid.NewGuid(),
                    ChatRoomName = "Test 1",
                    GuestServerUser = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "Main User"
                    },
                    InviteStatus = InviteStatus.SentAndPendingResponse

                }
            };
            allControlInvites.Add(invite1);
            allControlInvites.Add(invite2);
            allControlInvites.Add(invite3);
            allControlInvites.Add(invite4);

            return allControlInvites;
        }

        private void CreateInviteCanvasDynamicControl()
        {
            _tlpInviteCanvas.Dock = DockStyle.Fill;
            _tlpInviteCanvas.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            _tlpInviteCanvas.ColumnCount = 1;
            _tlpInviteCanvas.RowCount = 1;
            _tlpInviteCanvas.BackColor = Color.White;
            _tlpInviteCanvas.SetRowSpan(_tlpChatRoomCanvas, 3);
            _tlpInviteCanvas.HorizontalScroll.Maximum = 0;
            _tlpInviteCanvas.HorizontalScroll.Visible = false;
            _tlpInviteCanvas.AutoScroll = false;
            _tlpInviteCanvas.VerticalScroll.Visible = false;
            _tlpInviteCanvas.AutoScroll = true;
            tlpBase.Controls.Add(_tlpInviteCanvas, 1, 2);
        }
        private void ResolveInviteDynamicControl(ControlInvite controlInvite)
        {
            Action actionInviteDisplay = () =>
            {
                switch (controlInvite.ControlActionType)
                {
                    case ControlActionType.Create:

                        string controlInviteId = controlInvite.InviteObject.InviteId.ToString();
                        string chatRoomId = controlInvite.InviteObject.ChatRoomId.ToString();
                        string chatRoomIdentifier = controlInvite.InviteObject.ChatRoomName + "_" + chatRoomId;
                        string chatRoomCreator = controlInvite.InviteObject.ChatRoomCreator.Username;

                        TableLayoutPanel tlpNewRow = new TableLayoutPanel();
                        tlpNewRow.Name = controlInviteId;
                        tlpNewRow.Height = 50;
                        tlpNewRow.Width = (_tlpInviteCanvas.Width - 30);//415
                        tlpNewRow.BackColor = Color.LightGray;
                        tlpNewRow.ColumnCount = 4;
                        tlpNewRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
                        tlpNewRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 205F));
                        tlpNewRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
                        tlpNewRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));

                        tlpNewRow.RowCount = 3;
                        tlpNewRow.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                        tlpNewRow.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

                        tlpNewRow.Controls.Add(new Label() { Text = "Room:", Name = chatRoomId, BorderStyle = BorderStyle.FixedSingle, Width = 74, TextAlign = ContentAlignment.MiddleRight }, 0, 0);
                        tlpNewRow.Controls.Add(new Label() { Text = chatRoomIdentifier, Name = chatRoomId, BorderStyle = BorderStyle.FixedSingle, Width = 205, TextAlign = ContentAlignment.MiddleLeft }, 1, 0);
                        tlpNewRow.Controls.Add(new Label() { Text = "Creator:", BorderStyle = BorderStyle.FixedSingle, Width = 74, TextAlign = ContentAlignment.MiddleRight }, 0, 1);
                        tlpNewRow.Controls.Add(new Label() { Text = chatRoomCreator, BorderStyle = BorderStyle.FixedSingle, Width = 205, TextAlign = ContentAlignment.MiddleLeft }, 1, 1);

                        Button buttonAcceptInvite = new Button();
                        buttonAcceptInvite.Name = controlInviteId;
                        buttonAcceptInvite.Text = "Accept";
                        buttonAcceptInvite.Dock = DockStyle.Fill;
                        buttonAcceptInvite.Click += ButtonAcceptInvite_ClickEvent;
                        buttonAcceptInvite.Width = 50;
                        tlpNewRow.SetRowSpan(buttonAcceptInvite, 2);
                        tlpNewRow.Controls.Add(buttonAcceptInvite, 2, 0);

                        Button buttonDeclineInvite = new Button();
                        buttonDeclineInvite.Name = controlInviteId;
                        buttonDeclineInvite.Text = "Reject";
                        buttonDeclineInvite.Dock = DockStyle.Fill;
                        buttonDeclineInvite.Click += ButtonRejectInvite_ClickEvent;
                        buttonDeclineInvite.Width = 50;
                        tlpNewRow.SetRowSpan(buttonDeclineInvite, 2);
                        tlpNewRow.Controls.Add(buttonDeclineInvite, 2, 0);


                        int lastRowIndex = _tlpInviteCanvas.Controls.Count;

                        _tlpInviteCanvas.Controls.Add(tlpNewRow, 0, lastRowIndex);

                        controlInvite.ControlActionType = ControlActionType.Read;

                        break;
                    case ControlActionType.Update:

                        break;
                    case ControlActionType.Delete:
                        string controlInviteIdForDeletion = controlInvite.InviteObject.InviteId.ToString();
                        TableLayoutPanel tlpControlForRemoval = null;

                        foreach (var control in this._tlpInviteCanvas.Controls)
                        {
                            if (control is TableLayoutPanel)
                            {
                                TableLayoutPanel tlpControl = (TableLayoutPanel)control;
                                string tlpControlId = tlpControl.Name;
                                if (tlpControlId == controlInviteIdForDeletion)
                                {
                                    tlpControlForRemoval = tlpControl;
                                    break;
                                }
                            }
                        }
                        if (tlpControlForRemoval != null)
                        {
                            this._tlpInviteCanvas.Controls.Remove(tlpControlForRemoval);
                        }

                        break;
                }
            };
            this.tlpBase.BeginInvoke(actionInviteDisplay);
        }
        private void ButtonAcceptInvite_ClickEvent(object sender, EventArgs e)
        {
            Button btnAcceptInvite = (Button)sender;
            ServerCommunicationInfo serverCommunicationInfo = GetInviteInfoFromControls(btnAcceptInvite);
            if (serverCommunicationInfo == null) { return; }
            _user.AcceptInvite(serverCommunicationInfo);
        }

        private void ButtonRejectInvite_ClickEvent(object sender, EventArgs e)
        {
            Button btnRejectInvite = (Button)sender;
            ServerCommunicationInfo serverCommunicationInfo = GetInviteInfoFromControls(btnRejectInvite);
            if (serverCommunicationInfo == null) { return; }
            _user.RejectInvite(serverCommunicationInfo);
        }


        private ServerCommunicationInfo GetInviteInfoFromControls(Button buttonClicked)
        {
            string btnClickedId = buttonClicked.Name;
            foreach (var control in this._tlpInviteCanvas.Controls)
            {
                if (control is TableLayoutPanel)
                {
                    TableLayoutPanel tlp = (TableLayoutPanel)control;
                    string tlpId = tlp.Name;
                    if (tlpId == btnClickedId)
                    {
                        foreach (var itemControl in tlp.Controls)
                        {
                            if (itemControl is Label)
                            {
                                Label labelChatRoom = (Label)itemControl;
                                if (labelChatRoom.Text == "Room:")
                                {
                                    string chatRoomId = labelChatRoom.Name;
                                    ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
                                    serverCommunicationInfo.InviteId = new Guid(btnClickedId);
                                    serverCommunicationInfo.ChatRoomId = new Guid(chatRoomId);
                                    serverCommunicationInfo.ChatRoomName = string.Empty;
                                    return serverCommunicationInfo;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        #endregion Dynamic Controls
    }
}