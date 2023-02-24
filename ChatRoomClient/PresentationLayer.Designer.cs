namespace ChatRoomClient
{
    partial class PresentationLayer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tlpBase = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            txtUsername = new TextBox();
            lblUsername = new Label();
            lblWarningPort = new Label();
            txtPort = new TextBox();
            lblPort = new Label();
            lblWarningIPAddress = new Label();
            txtServerIPAddress = new TextBox();
            lblServerIPAddress = new Label();
            txtClientStatus = new TextBox();
            labelClientStatus = new Label();
            lblWarningUsername = new Label();
            btnConnect = new Button();
            btnDisconnect = new Button();
            txtClientLog = new TextBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            txtUsernameChatRoom = new TextBox();
            lblOtherActiveUsers = new Label();
            lblUsernameStatus = new Label();
            btnUsernameRetry = new Button();
            checkedListServerUsers = new CheckedListBox();
            lblCreateChatRoom = new Label();
            lblChatRoomName = new Label();
            txtChatRoomName = new TextBox();
            chkBoxAddGuests = new CheckBox();
            lblWarningChatRoomName = new Label();
            lblWarningGuests = new Label();
            listBoxAllGuests = new ListBox();
            btnCreateChatRoomSendInvites = new Button();
            tlpBase.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tlpBase
            // 
            tlpBase.ColumnCount = 3;
            tlpBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.93305F));
            tlpBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57.06695F));
            tlpBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 477F));
            tlpBase.Controls.Add(tableLayoutPanel2, 0, 0);
            tlpBase.Controls.Add(txtClientLog, 0, 1);
            tlpBase.Controls.Add(tableLayoutPanel3, 1, 0);
            tlpBase.Dock = DockStyle.Fill;
            tlpBase.Location = new Point(0, 0);
            tlpBase.Name = "tlpBase";
            tlpBase.RowCount = 4;
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Percent, 97.30458F));
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Percent, 2.695418F));
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 207F));
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 14F));
            tlpBase.Size = new Size(1268, 590);
            tlpBase.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.530806F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 91.46919F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 156F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            tableLayoutPanel2.Controls.Add(txtUsername, 1, 1);
            tableLayoutPanel2.Controls.Add(lblUsername, 1, 0);
            tableLayoutPanel2.Controls.Add(lblWarningPort, 1, 10);
            tableLayoutPanel2.Controls.Add(txtPort, 1, 9);
            tableLayoutPanel2.Controls.Add(lblPort, 1, 8);
            tableLayoutPanel2.Controls.Add(lblWarningIPAddress, 1, 7);
            tableLayoutPanel2.Controls.Add(txtServerIPAddress, 1, 6);
            tableLayoutPanel2.Controls.Add(lblServerIPAddress, 1, 5);
            tableLayoutPanel2.Controls.Add(txtClientStatus, 1, 4);
            tableLayoutPanel2.Controls.Add(labelClientStatus, 1, 3);
            tableLayoutPanel2.Controls.Add(lblWarningUsername, 1, 2);
            tableLayoutPanel2.Controls.Add(btnConnect, 1, 12);
            tableLayoutPanel2.Controls.Add(btnDisconnect, 2, 12);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 13;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 40.74074F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 59.25926F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(333, 353);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel2.SetColumnSpan(txtUsername, 2);
            txtUsername.Dock = DockStyle.Fill;
            txtUsername.Location = new Point(17, 27);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(302, 27);
            txtUsername.TabIndex = 1;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // lblUsername
            // 
            lblUsername.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(17, 4);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(78, 20);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username:";
            lblUsername.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblWarningPort
            // 
            lblWarningPort.AutoSize = true;
            lblWarningPort.BackColor = SystemColors.Control;
            tableLayoutPanel2.SetColumnSpan(lblWarningPort, 2);
            lblWarningPort.ForeColor = Color.Red;
            lblWarningPort.Location = new Point(17, 272);
            lblWarningPort.Name = "lblWarningPort";
            tableLayoutPanel2.SetRowSpan(lblWarningPort, 2);
            lblWarningPort.Size = new Size(88, 20);
            lblWarningPort.TabIndex = 12;
            lblWarningPort.Text = "warningPort";
            lblWarningPort.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPort
            // 
            txtPort.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel2.SetColumnSpan(txtPort, 2);
            txtPort.Dock = DockStyle.Fill;
            txtPort.Location = new Point(17, 242);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(302, 27);
            txtPort.TabIndex = 7;
            txtPort.TextAlign = HorizontalAlignment.Center;
            txtPort.TextChanged += txtPort_TextChanged;
            // 
            // lblPort
            // 
            lblPort.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPort.AutoSize = true;
            lblPort.Location = new Point(17, 219);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(38, 20);
            lblPort.TabIndex = 6;
            lblPort.Text = "Port:";
            lblPort.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblWarningIPAddress
            // 
            lblWarningIPAddress.AutoSize = true;
            lblWarningIPAddress.BackColor = SystemColors.Control;
            tableLayoutPanel2.SetColumnSpan(lblWarningIPAddress, 2);
            lblWarningIPAddress.ForeColor = Color.Red;
            lblWarningIPAddress.Location = new Point(17, 191);
            lblWarningIPAddress.Name = "lblWarningIPAddress";
            lblWarningIPAddress.Size = new Size(126, 20);
            lblWarningIPAddress.TabIndex = 11;
            lblWarningIPAddress.Text = "warningIPAddress";
            lblWarningIPAddress.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtServerIPAddress
            // 
            txtServerIPAddress.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel2.SetColumnSpan(txtServerIPAddress, 2);
            txtServerIPAddress.Dock = DockStyle.Fill;
            txtServerIPAddress.Location = new Point(17, 163);
            txtServerIPAddress.Name = "txtServerIPAddress";
            txtServerIPAddress.Size = new Size(302, 27);
            txtServerIPAddress.TabIndex = 5;
            txtServerIPAddress.TextAlign = HorizontalAlignment.Center;
            txtServerIPAddress.TextChanged += txtServerIPAddress_TextChanged;
            // 
            // lblServerIPAddress
            // 
            lblServerIPAddress.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblServerIPAddress.AutoSize = true;
            lblServerIPAddress.Location = new Point(17, 140);
            lblServerIPAddress.Name = "lblServerIPAddress";
            lblServerIPAddress.Size = new Size(126, 20);
            lblServerIPAddress.TabIndex = 4;
            lblServerIPAddress.Text = "Server IP Address:";
            lblServerIPAddress.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtClientStatus
            // 
            txtClientStatus.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel2.SetColumnSpan(txtClientStatus, 2);
            txtClientStatus.Dock = DockStyle.Fill;
            txtClientStatus.Location = new Point(17, 111);
            txtClientStatus.Name = "txtClientStatus";
            txtClientStatus.ReadOnly = true;
            txtClientStatus.Size = new Size(302, 27);
            txtClientStatus.TabIndex = 3;
            txtClientStatus.Text = "Disconnected";
            txtClientStatus.TextAlign = HorizontalAlignment.Center;
            // 
            // labelClientStatus
            // 
            labelClientStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelClientStatus.AutoSize = true;
            labelClientStatus.Location = new Point(17, 88);
            labelClientStatus.Name = "labelClientStatus";
            labelClientStatus.Size = new Size(94, 20);
            labelClientStatus.TabIndex = 2;
            labelClientStatus.Text = "Client Status:";
            labelClientStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblWarningUsername
            // 
            lblWarningUsername.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(lblWarningUsername, 2);
            lblWarningUsername.ForeColor = Color.Red;
            lblWarningUsername.Location = new Point(17, 60);
            lblWarningUsername.Name = "lblWarningUsername";
            lblWarningUsername.Size = new Size(128, 20);
            lblWarningUsername.TabIndex = 10;
            lblWarningUsername.Text = "warningUsername";
            lblWarningUsername.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(17, 321);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(134, 28);
            btnConnect.TabIndex = 8;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += BtnClientConnectToServer_ClickEvent;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(169, 321);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(149, 28);
            btnDisconnect.TabIndex = 9;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += BtnClientDisconnectFromServer_ClickEvent;
            // 
            // txtClientLog
            // 
            txtClientLog.BorderStyle = BorderStyle.FixedSingle;
            txtClientLog.Location = new Point(3, 362);
            txtClientLog.Multiline = true;
            txtClientLog.Name = "txtClientLog";
            txtClientLog.ReadOnly = true;
            tlpBase.SetRowSpan(txtClientLog, 2);
            txtClientLog.ScrollBars = ScrollBars.Vertical;
            txtClientLog.Size = new Size(328, 209);
            txtClientLog.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 77.65151F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.348484F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 270F));
            tableLayoutPanel3.Controls.Add(txtUsernameChatRoom, 0, 1);
            tableLayoutPanel3.Controls.Add(lblOtherActiveUsers, 0, 2);
            tableLayoutPanel3.Controls.Add(lblUsernameStatus, 0, 0);
            tableLayoutPanel3.Controls.Add(btnUsernameRetry, 2, 1);
            tableLayoutPanel3.Controls.Add(checkedListServerUsers, 0, 3);
            tableLayoutPanel3.Controls.Add(lblCreateChatRoom, 1, 2);
            tableLayoutPanel3.Controls.Add(lblChatRoomName, 1, 3);
            tableLayoutPanel3.Controls.Add(txtChatRoomName, 2, 3);
            tableLayoutPanel3.Controls.Add(chkBoxAddGuests, 1, 5);
            tableLayoutPanel3.Controls.Add(lblWarningChatRoomName, 1, 4);
            tableLayoutPanel3.Controls.Add(lblWarningGuests, 1, 6);
            tableLayoutPanel3.Controls.Add(listBoxAllGuests, 1, 7);
            tableLayoutPanel3.Controls.Add(btnCreateChatRoomSendInvites, 0, 8);
            tableLayoutPanel3.Location = new Point(342, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 9;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 38.88889F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 61.11111F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 116F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(445, 353);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // txtUsernameChatRoom
            // 
            txtUsernameChatRoom.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel3.SetColumnSpan(txtUsernameChatRoom, 2);
            txtUsernameChatRoom.Dock = DockStyle.Fill;
            txtUsernameChatRoom.Location = new Point(4, 26);
            txtUsernameChatRoom.Name = "txtUsernameChatRoom";
            txtUsernameChatRoom.ReadOnly = true;
            txtUsernameChatRoom.Size = new Size(245, 27);
            txtUsernameChatRoom.TabIndex = 0;
            // 
            // lblOtherActiveUsers
            // 
            lblOtherActiveUsers.AutoSize = true;
            lblOtherActiveUsers.Location = new Point(4, 58);
            lblOtherActiveUsers.Name = "lblOtherActiveUsers";
            lblOtherActiveUsers.Size = new Size(130, 20);
            lblOtherActiveUsers.TabIndex = 1;
            lblOtherActiveUsers.Text = "Other Active Users";
            // 
            // lblUsernameStatus
            // 
            lblUsernameStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblUsernameStatus.AutoSize = true;
            lblUsernameStatus.Location = new Point(4, 2);
            lblUsernameStatus.Name = "lblUsernameStatus";
            lblUsernameStatus.Size = new Size(113, 20);
            lblUsernameStatus.TabIndex = 1;
            lblUsernameStatus.Text = "usernameStatus";
            // 
            // btnUsernameRetry
            // 
            btnUsernameRetry.Dock = DockStyle.Left;
            btnUsernameRetry.Location = new Point(256, 26);
            btnUsernameRetry.Name = "btnUsernameRetry";
            btnUsernameRetry.Size = new Size(107, 28);
            btnUsernameRetry.TabIndex = 2;
            btnUsernameRetry.Text = "Retry";
            btnUsernameRetry.UseVisualStyleBackColor = true;
            btnUsernameRetry.Click += BtnUsernameRetry_ClickEvent;
            // 
            // checkedListServerUsers
            // 
            checkedListServerUsers.BackColor = SystemColors.Control;
            checkedListServerUsers.Dock = DockStyle.Fill;
            checkedListServerUsers.Enabled = false;
            checkedListServerUsers.FormattingEnabled = true;
            checkedListServerUsers.Location = new Point(4, 87);
            checkedListServerUsers.Name = "checkedListServerUsers";
            tableLayoutPanel3.SetRowSpan(checkedListServerUsers, 5);
            checkedListServerUsers.Size = new Size(188, 227);
            checkedListServerUsers.TabIndex = 3;
            checkedListServerUsers.ItemCheck += checkedListServerUsers_ItemCheck;
            // 
            // lblCreateChatRoom
            // 
            lblCreateChatRoom.AutoSize = true;
            tableLayoutPanel3.SetColumnSpan(lblCreateChatRoom, 2);
            lblCreateChatRoom.Location = new Point(199, 58);
            lblCreateChatRoom.Name = "lblCreateChatRoom";
            lblCreateChatRoom.Size = new Size(126, 20);
            lblCreateChatRoom.TabIndex = 4;
            lblCreateChatRoom.Text = "Create ChatRoom";
            // 
            // lblChatRoomName
            // 
            lblChatRoomName.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblChatRoomName.AutoSize = true;
            lblChatRoomName.Location = new Point(200, 84);
            lblChatRoomName.Name = "lblChatRoomName";
            lblChatRoomName.Size = new Size(49, 32);
            lblChatRoomName.TabIndex = 5;
            lblChatRoomName.Text = "Name";
            lblChatRoomName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtChatRoomName
            // 
            txtChatRoomName.BorderStyle = BorderStyle.FixedSingle;
            txtChatRoomName.Dock = DockStyle.Fill;
            txtChatRoomName.Location = new Point(256, 87);
            txtChatRoomName.Name = "txtChatRoomName";
            txtChatRoomName.Size = new Size(185, 27);
            txtChatRoomName.TabIndex = 6;
            txtChatRoomName.TextChanged += txtChatRoomName_TextChanged;
            // 
            // chkBoxAddGuests
            // 
            chkBoxAddGuests.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkBoxAddGuests.AutoSize = true;
            tableLayoutPanel3.SetColumnSpan(chkBoxAddGuests, 2);
            chkBoxAddGuests.Location = new Point(199, 150);
            chkBoxAddGuests.Name = "chkBoxAddGuests";
            chkBoxAddGuests.Size = new Size(198, 23);
            chkBoxAddGuests.TabIndex = 7;
            chkBoxAddGuests.Text = "Add Guests to ChatRoom";
            chkBoxAddGuests.UseVisualStyleBackColor = true;
            chkBoxAddGuests.CheckedChanged += chkBoxAddGuests_CheckedChanged;
            // 
            // lblWarningChatRoomName
            // 
            lblWarningChatRoomName.AutoSize = true;
            tableLayoutPanel3.SetColumnSpan(lblWarningChatRoomName, 2);
            lblWarningChatRoomName.Dock = DockStyle.Left;
            lblWarningChatRoomName.ForeColor = Color.Red;
            lblWarningChatRoomName.Location = new Point(199, 117);
            lblWarningChatRoomName.Name = "lblWarningChatRoomName";
            lblWarningChatRoomName.Size = new Size(132, 29);
            lblWarningChatRoomName.TabIndex = 10;
            lblWarningChatRoomName.Text = "warningChatRoom";
            // 
            // lblWarningGuests
            // 
            lblWarningGuests.AutoSize = true;
            tableLayoutPanel3.SetColumnSpan(lblWarningGuests, 2);
            lblWarningGuests.ForeColor = Color.Red;
            lblWarningGuests.Location = new Point(199, 177);
            lblWarningGuests.Name = "lblWarningGuests";
            lblWarningGuests.Size = new Size(107, 20);
            lblWarningGuests.TabIndex = 11;
            lblWarningGuests.Text = "WarningGuests";
            // 
            // listBoxAllGuests
            // 
            listBoxAllGuests.BackColor = SystemColors.Control;
            tableLayoutPanel3.SetColumnSpan(listBoxAllGuests, 2);
            listBoxAllGuests.Dock = DockStyle.Fill;
            listBoxAllGuests.FormattingEnabled = true;
            listBoxAllGuests.ItemHeight = 20;
            listBoxAllGuests.Location = new Point(199, 204);
            listBoxAllGuests.Name = "listBoxAllGuests";
            listBoxAllGuests.Size = new Size(242, 110);
            listBoxAllGuests.TabIndex = 8;
            // 
            // btnCreateChatRoomSendInvites
            // 
            tableLayoutPanel3.SetColumnSpan(btnCreateChatRoomSendInvites, 2);
            btnCreateChatRoomSendInvites.Location = new Point(4, 321);
            btnCreateChatRoomSendInvites.Name = "btnCreateChatRoomSendInvites";
            btnCreateChatRoomSendInvites.Size = new Size(245, 28);
            btnCreateChatRoomSendInvites.TabIndex = 9;
            btnCreateChatRoomSendInvites.Text = "Create ChatRoom And Send Invites";
            btnCreateChatRoomSendInvites.UseVisualStyleBackColor = true;
            btnCreateChatRoomSendInvites.Click += BtnCreateChatRoomAndSendInvite_ClickEvent;
            // 
            // PresentationLayer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1268, 590);
            Controls.Add(tlpBase);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PresentationLayer";
            Text = "ChatClient";
            Load += WinFormOnLoad_Event;
            tlpBase.ResumeLayout(false);
            tlpBase.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpBase;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label labelClientStatus;
        private TextBox txtClientStatus;
        private Label lblServerIPAddress;
        private TextBox txtServerIPAddress;
        private Label lblPort;
        private TextBox txtPort;
        private Button btnConnect;
        private Button btnDisconnect;
        private TextBox txtClientLog;
        private Label lblWarningUsername;
        private Label lblWarningIPAddress;
        private Label lblWarningPort;
        private TableLayoutPanel tableLayoutPanel3;
        private TextBox txtUsernameChatRoom;
        private Label lblOtherActiveUsers;
        private Label lblUsernameStatus;
        private Button btnUsernameRetry;
        private CheckedListBox checkedListServerUsers;
        private Label lblCreateChatRoom;
        private Label lblChatRoomName;
        private TextBox txtChatRoomName;
        private CheckBox chkBoxAddGuests;
        private ListBox listBoxAllGuests;
        private Button btnCreateChatRoomSendInvites;
        private Label lblWarningChatRoomName;
        private Label lblWarningGuests;
    }
}