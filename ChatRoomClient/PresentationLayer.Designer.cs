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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblWarningPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblWarningIPAddress = new System.Windows.Forms.Label();
            this.txtServerIPAddress = new System.Windows.Forms.TextBox();
            this.lblServerIPAddress = new System.Windows.Forms.Label();
            this.txtClientStatus = new System.Windows.Forms.TextBox();
            this.labelClientStatus = new System.Windows.Forms.Label();
            this.lblWarningUsername = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtUsernameChatRoom = new System.Windows.Forms.TextBox();
            this.lblOtherActiveUsers = new System.Windows.Forms.Label();
            this.lblUsernameStatus = new System.Windows.Forms.Label();
            this.btnUsernameRetry = new System.Windows.Forms.Button();
            this.checkedListServerUsers = new System.Windows.Forms.CheckedListBox();
            this.lblCreateChatRoom = new System.Windows.Forms.Label();
            this.lblChatRoomName = new System.Windows.Forms.Label();
            this.txtChatRoomName = new System.Windows.Forms.TextBox();
            this.chkBoxAddGuests = new System.Windows.Forms.CheckBox();
            this.lblWarningChatRoomName = new System.Windows.Forms.Label();
            this.lblWarningGuests = new System.Windows.Forms.Label();
            this.listBoxAllGuests = new System.Windows.Forms.ListBox();
            this.btnCreateChatRoomSendInvites = new System.Windows.Forms.Button();
            this.txtClientLog = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.89211F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.10789F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtClientLog, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.19588F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.80412F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1242, 590);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.530806F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.46919F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.Controls.Add(this.txtUsername, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblUsername, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblWarningPort, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this.txtPort, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.lblPort, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.lblWarningIPAddress, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.txtServerIPAddress, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.lblServerIPAddress, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.txtClientStatus, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.labelClientStatus, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblWarningUsername, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnConnect, 1, 12);
            this.tableLayoutPanel2.Controls.Add(this.btnDisconnect, 2, 12);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 13;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.7347F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.2653F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(328, 349);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.txtUsername, 2);
            this.txtUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUsername.Location = new System.Drawing.Point(16, 24);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(298, 27);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(16, 1);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(78, 20);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username:";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWarningPort
            // 
            this.lblWarningPort.AutoSize = true;
            this.lblWarningPort.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel2.SetColumnSpan(this.lblWarningPort, 2);
            this.lblWarningPort.ForeColor = System.Drawing.Color.Red;
            this.lblWarningPort.Location = new System.Drawing.Point(16, 268);
            this.lblWarningPort.Name = "lblWarningPort";
            this.tableLayoutPanel2.SetRowSpan(this.lblWarningPort, 2);
            this.lblWarningPort.Size = new System.Drawing.Size(88, 20);
            this.lblWarningPort.TabIndex = 12;
            this.lblWarningPort.Text = "warningPort";
            this.lblWarningPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPort
            // 
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.txtPort, 2);
            this.txtPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPort.Location = new System.Drawing.Point(16, 238);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(298, 27);
            this.txtPort.TabIndex = 7;
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // lblPort
            // 
            this.lblPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(16, 215);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(38, 20);
            this.lblPort.TabIndex = 6;
            this.lblPort.Text = "Port:";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWarningIPAddress
            // 
            this.lblWarningIPAddress.AutoSize = true;
            this.lblWarningIPAddress.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel2.SetColumnSpan(this.lblWarningIPAddress, 2);
            this.lblWarningIPAddress.ForeColor = System.Drawing.Color.Red;
            this.lblWarningIPAddress.Location = new System.Drawing.Point(16, 187);
            this.lblWarningIPAddress.Name = "lblWarningIPAddress";
            this.lblWarningIPAddress.Size = new System.Drawing.Size(126, 20);
            this.lblWarningIPAddress.TabIndex = 11;
            this.lblWarningIPAddress.Text = "warningIPAddress";
            this.lblWarningIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtServerIPAddress
            // 
            this.txtServerIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.txtServerIPAddress, 2);
            this.txtServerIPAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServerIPAddress.Location = new System.Drawing.Point(16, 159);
            this.txtServerIPAddress.Name = "txtServerIPAddress";
            this.txtServerIPAddress.Size = new System.Drawing.Size(298, 27);
            this.txtServerIPAddress.TabIndex = 5;
            this.txtServerIPAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtServerIPAddress.TextChanged += new System.EventHandler(this.txtServerIPAddress_TextChanged);
            // 
            // lblServerIPAddress
            // 
            this.lblServerIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblServerIPAddress.AutoSize = true;
            this.lblServerIPAddress.Location = new System.Drawing.Point(16, 136);
            this.lblServerIPAddress.Name = "lblServerIPAddress";
            this.lblServerIPAddress.Size = new System.Drawing.Size(126, 20);
            this.lblServerIPAddress.TabIndex = 4;
            this.lblServerIPAddress.Text = "Server IP Address:";
            this.lblServerIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClientStatus
            // 
            this.txtClientStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.txtClientStatus, 2);
            this.txtClientStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClientStatus.Location = new System.Drawing.Point(16, 107);
            this.txtClientStatus.Name = "txtClientStatus";
            this.txtClientStatus.ReadOnly = true;
            this.txtClientStatus.Size = new System.Drawing.Size(298, 27);
            this.txtClientStatus.TabIndex = 3;
            this.txtClientStatus.Text = "Disconnected";
            this.txtClientStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelClientStatus
            // 
            this.labelClientStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelClientStatus.AutoSize = true;
            this.labelClientStatus.Location = new System.Drawing.Point(16, 84);
            this.labelClientStatus.Name = "labelClientStatus";
            this.labelClientStatus.Size = new System.Drawing.Size(94, 20);
            this.labelClientStatus.TabIndex = 2;
            this.labelClientStatus.Text = "Client Status:";
            this.labelClientStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWarningUsername
            // 
            this.lblWarningUsername.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.lblWarningUsername, 2);
            this.lblWarningUsername.ForeColor = System.Drawing.Color.Red;
            this.lblWarningUsername.Location = new System.Drawing.Point(16, 57);
            this.lblWarningUsername.Name = "lblWarningUsername";
            this.lblWarningUsername.Size = new System.Drawing.Size(128, 20);
            this.lblWarningUsername.TabIndex = 10;
            this.lblWarningUsername.Text = "warningUsername";
            this.lblWarningUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(16, 317);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(134, 29);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnClientConnectToServer_ClickEvent);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(164, 317);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(149, 29);
            this.btnDisconnect.TabIndex = 9;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.BtnClientDisconnectFromServer_ClickEvent);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.37255F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.62745F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 379F));
            this.tableLayoutPanel3.Controls.Add(this.txtUsernameChatRoom, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblOtherActiveUsers, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblUsernameStatus, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnUsernameRetry, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkedListServerUsers, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.lblCreateChatRoom, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblChatRoomName, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.txtChatRoomName, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.chkBoxAddGuests, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.lblWarningChatRoomName, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.lblWarningGuests, 1, 6);
            this.tableLayoutPanel3.Controls.Add(this.listBoxAllGuests, 1, 7);
            this.tableLayoutPanel3.Controls.Add(this.btnCreateChatRoomSendInvites, 0, 8);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(337, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 9;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.2549F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.7451F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(902, 349);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // txtUsernameChatRoom
            // 
            this.txtUsernameChatRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel3.SetColumnSpan(this.txtUsernameChatRoom, 2);
            this.txtUsernameChatRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUsernameChatRoom.Location = new System.Drawing.Point(4, 25);
            this.txtUsernameChatRoom.Name = "txtUsernameChatRoom";
            this.txtUsernameChatRoom.ReadOnly = true;
            this.txtUsernameChatRoom.Size = new System.Drawing.Size(299, 27);
            this.txtUsernameChatRoom.TabIndex = 0;
            // 
            // lblOtherActiveUsers
            // 
            this.lblOtherActiveUsers.AutoSize = true;
            this.lblOtherActiveUsers.Location = new System.Drawing.Point(4, 57);
            this.lblOtherActiveUsers.Name = "lblOtherActiveUsers";
            this.lblOtherActiveUsers.Size = new System.Drawing.Size(130, 20);
            this.lblOtherActiveUsers.TabIndex = 1;
            this.lblOtherActiveUsers.Text = "Other Active Users";
            // 
            // lblUsernameStatus
            // 
            this.lblUsernameStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUsernameStatus.AutoSize = true;
            this.lblUsernameStatus.Location = new System.Drawing.Point(4, 1);
            this.lblUsernameStatus.Name = "lblUsernameStatus";
            this.lblUsernameStatus.Size = new System.Drawing.Size(113, 20);
            this.lblUsernameStatus.TabIndex = 1;
            this.lblUsernameStatus.Text = "usernameStatus";
            // 
            // btnUsernameRetry
            // 
            this.btnUsernameRetry.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnUsernameRetry.Location = new System.Drawing.Point(310, 25);
            this.btnUsernameRetry.Name = "btnUsernameRetry";
            this.btnUsernameRetry.Size = new System.Drawing.Size(107, 28);
            this.btnUsernameRetry.TabIndex = 2;
            this.btnUsernameRetry.Text = "Retry";
            this.btnUsernameRetry.UseVisualStyleBackColor = true;
            this.btnUsernameRetry.Click += new System.EventHandler(this.BtnUsernameRetry_Click);
            // 
            // checkedListServerUsers
            // 
            this.checkedListServerUsers.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListServerUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListServerUsers.Enabled = false;
            this.checkedListServerUsers.FormattingEnabled = true;
            this.checkedListServerUsers.Location = new System.Drawing.Point(4, 83);
            this.checkedListServerUsers.Name = "checkedListServerUsers";
            this.tableLayoutPanel3.SetRowSpan(this.checkedListServerUsers, 5);
            this.checkedListServerUsers.Size = new System.Drawing.Size(242, 227);
            this.checkedListServerUsers.TabIndex = 3;
            this.checkedListServerUsers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListServerUsers_ItemCheck);
            // 
            // lblCreateChatRoom
            // 
            this.lblCreateChatRoom.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.lblCreateChatRoom, 2);
            this.lblCreateChatRoom.Location = new System.Drawing.Point(253, 57);
            this.lblCreateChatRoom.Name = "lblCreateChatRoom";
            this.lblCreateChatRoom.Size = new System.Drawing.Size(126, 20);
            this.lblCreateChatRoom.TabIndex = 4;
            this.lblCreateChatRoom.Text = "Create ChatRoom";
            // 
            // lblChatRoomName
            // 
            this.lblChatRoomName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChatRoomName.AutoSize = true;
            this.lblChatRoomName.Location = new System.Drawing.Point(254, 80);
            this.lblChatRoomName.Name = "lblChatRoomName";
            this.lblChatRoomName.Size = new System.Drawing.Size(49, 32);
            this.lblChatRoomName.TabIndex = 5;
            this.lblChatRoomName.Text = "Name";
            this.lblChatRoomName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtChatRoomName
            // 
            this.txtChatRoomName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChatRoomName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChatRoomName.Location = new System.Drawing.Point(310, 83);
            this.txtChatRoomName.Name = "txtChatRoomName";
            this.txtChatRoomName.Size = new System.Drawing.Size(207, 27);
            this.txtChatRoomName.TabIndex = 6;
            this.txtChatRoomName.TextChanged += new System.EventHandler(this.txtChatRoomName_TextChanged);
            // 
            // chkBoxAddGuests
            // 
            this.chkBoxAddGuests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkBoxAddGuests.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.chkBoxAddGuests, 2);
            this.chkBoxAddGuests.Location = new System.Drawing.Point(253, 146);
            this.chkBoxAddGuests.Name = "chkBoxAddGuests";
            this.chkBoxAddGuests.Size = new System.Drawing.Size(198, 23);
            this.chkBoxAddGuests.TabIndex = 7;
            this.chkBoxAddGuests.Text = "Add Guests to ChatRoom";
            this.chkBoxAddGuests.UseVisualStyleBackColor = true;
            this.chkBoxAddGuests.CheckedChanged += new System.EventHandler(this.chkBoxAddGuests_CheckedChanged);
            // 
            // lblWarningChatRoomName
            // 
            this.lblWarningChatRoomName.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.lblWarningChatRoomName, 2);
            this.lblWarningChatRoomName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblWarningChatRoomName.ForeColor = System.Drawing.Color.Red;
            this.lblWarningChatRoomName.Location = new System.Drawing.Point(253, 113);
            this.lblWarningChatRoomName.Name = "lblWarningChatRoomName";
            this.lblWarningChatRoomName.Size = new System.Drawing.Size(132, 29);
            this.lblWarningChatRoomName.TabIndex = 10;
            this.lblWarningChatRoomName.Text = "warningChatRoom";
            // 
            // lblWarningGuests
            // 
            this.lblWarningGuests.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.lblWarningGuests, 2);
            this.lblWarningGuests.ForeColor = System.Drawing.Color.Red;
            this.lblWarningGuests.Location = new System.Drawing.Point(253, 173);
            this.lblWarningGuests.Name = "lblWarningGuests";
            this.lblWarningGuests.Size = new System.Drawing.Size(107, 20);
            this.lblWarningGuests.TabIndex = 11;
            this.lblWarningGuests.Text = "WarningGuests";
            // 
            // listBoxAllGuests
            // 
            this.listBoxAllGuests.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel3.SetColumnSpan(this.listBoxAllGuests, 2);
            this.listBoxAllGuests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAllGuests.FormattingEnabled = true;
            this.listBoxAllGuests.ItemHeight = 20;
            this.listBoxAllGuests.Location = new System.Drawing.Point(253, 200);
            this.listBoxAllGuests.Name = "listBoxAllGuests";
            this.listBoxAllGuests.Size = new System.Drawing.Size(264, 110);
            this.listBoxAllGuests.TabIndex = 8;
            // 
            // btnCreateChatRoomSendInvites
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.btnCreateChatRoomSendInvites, 2);
            this.btnCreateChatRoomSendInvites.Location = new System.Drawing.Point(4, 317);
            this.btnCreateChatRoomSendInvites.Name = "btnCreateChatRoomSendInvites";
            this.btnCreateChatRoomSendInvites.Size = new System.Drawing.Size(264, 28);
            this.btnCreateChatRoomSendInvites.TabIndex = 9;
            this.btnCreateChatRoomSendInvites.Text = "Create ChatRoom And Send Invites";
            this.btnCreateChatRoomSendInvites.UseVisualStyleBackColor = true;
            // 
            // txtClientLog
            // 
            this.txtClientLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClientLog.Location = new System.Drawing.Point(3, 358);
            this.txtClientLog.Multiline = true;
            this.txtClientLog.Name = "txtClientLog";
            this.txtClientLog.ReadOnly = true;
            this.tableLayoutPanel1.SetRowSpan(this.txtClientLog, 2);
            this.txtClientLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtClientLog.Size = new System.Drawing.Size(328, 208);
            this.txtClientLog.TabIndex = 1;
            // 
            // PresentationLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 590);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PresentationLayer";
            this.Text = "ChatClient";
            this.Load += new System.EventHandler(this.WinFormOnLoad_Event);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
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