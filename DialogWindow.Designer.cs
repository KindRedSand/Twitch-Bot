namespace TwitchBot
{
    partial class DialogWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogWindow));
            this.moduleCheckBox = new System.Windows.Forms.CheckBox();
            this.moduleReloadButton = new System.Windows.Forms.Button();
            this.ModuleName = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.commandFeedbackTextBox = new System.Windows.Forms.TextBox();
            this.commandsCommandTextBox = new System.Windows.Forms.TextBox();
            this.commandsMinusButton = new System.Windows.Forms.Button();
            this.commandsReplaceButton = new System.Windows.Forms.Button();
            this.commandsPlusButton = new System.Windows.Forms.Button();
            this.CommandsListBox = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chatListBox = new System.Windows.Forms.ListBox();
            this.chattersListBox = new System.Windows.Forms.ListBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OATokenTextBox = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.UsernameTextBox = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.openConfigFolderButton = new Bunifu.Framework.UI.BunifuFlatButton();
            this.joinButton = new Bunifu.Framework.UI.BunifuFlatButton();
            this.channelTextBox = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.connectButton = new System.Windows.Forms.Button();
            this.connectionStateLabel = new System.Windows.Forms.Label();
            this.constConnectionStateLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // moduleCheckBox
            // 
            this.moduleCheckBox.Location = new System.Drawing.Point(0, 0);
            this.moduleCheckBox.Name = "moduleCheckBox";
            this.moduleCheckBox.Size = new System.Drawing.Size(104, 24);
            this.moduleCheckBox.TabIndex = 0;
            // 
            // moduleReloadButton
            // 
            this.moduleReloadButton.Location = new System.Drawing.Point(0, 0);
            this.moduleReloadButton.Name = "moduleReloadButton";
            this.moduleReloadButton.Size = new System.Drawing.Size(75, 23);
            this.moduleReloadButton.TabIndex = 0;
            // 
            // ModuleName
            // 
            this.ModuleName.Location = new System.Drawing.Point(0, 0);
            this.ModuleName.Name = "ModuleName";
            this.ModuleName.Size = new System.Drawing.Size(100, 23);
            this.ModuleName.TabIndex = 0;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.White;
            this.tabPage4.Controls.Add(this.commandFeedbackTextBox);
            this.tabPage4.Controls.Add(this.commandsCommandTextBox);
            this.tabPage4.Controls.Add(this.commandsMinusButton);
            this.tabPage4.Controls.Add(this.commandsReplaceButton);
            this.tabPage4.Controls.Add(this.commandsPlusButton);
            this.tabPage4.Controls.Add(this.CommandsListBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1000, 535);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "GCommands";
            // 
            // commandFeedbackTextBox
            // 
            this.commandFeedbackTextBox.Location = new System.Drawing.Point(506, 32);
            this.commandFeedbackTextBox.Multiline = true;
            this.commandFeedbackTextBox.Name = "commandFeedbackTextBox";
            this.commandFeedbackTextBox.Size = new System.Drawing.Size(488, 230);
            this.commandFeedbackTextBox.TabIndex = 5;
            // 
            // commandsCommandTextBox
            // 
            this.commandsCommandTextBox.Location = new System.Drawing.Point(506, 6);
            this.commandsCommandTextBox.Name = "commandsCommandTextBox";
            this.commandsCommandTextBox.Size = new System.Drawing.Size(488, 20);
            this.commandsCommandTextBox.TabIndex = 4;
            // 
            // commandsMinusButton
            // 
            this.commandsMinusButton.Location = new System.Drawing.Point(471, 191);
            this.commandsMinusButton.Name = "commandsMinusButton";
            this.commandsMinusButton.Size = new System.Drawing.Size(31, 23);
            this.commandsMinusButton.TabIndex = 3;
            this.commandsMinusButton.Text = "-";
            this.commandsMinusButton.UseVisualStyleBackColor = true;
            this.commandsMinusButton.Click += new System.EventHandler(this.commandsMinusButton_Click);
            // 
            // commandsReplaceButton
            // 
            this.commandsReplaceButton.Location = new System.Drawing.Point(471, 162);
            this.commandsReplaceButton.Name = "commandsReplaceButton";
            this.commandsReplaceButton.Size = new System.Drawing.Size(31, 23);
            this.commandsReplaceButton.TabIndex = 2;
            this.commandsReplaceButton.Text = "<=";
            this.commandsReplaceButton.UseVisualStyleBackColor = true;
            this.commandsReplaceButton.Click += new System.EventHandler(this.commandsReplaceButton_Click);
            // 
            // commandsPlusButton
            // 
            this.commandsPlusButton.Location = new System.Drawing.Point(471, 133);
            this.commandsPlusButton.Name = "commandsPlusButton";
            this.commandsPlusButton.Size = new System.Drawing.Size(31, 23);
            this.commandsPlusButton.TabIndex = 1;
            this.commandsPlusButton.Text = "+";
            this.commandsPlusButton.UseVisualStyleBackColor = true;
            this.commandsPlusButton.Click += new System.EventHandler(this.commandsPlusButton_Click);
            // 
            // CommandsListBox
            // 
            this.CommandsListBox.FormattingEnabled = true;
            this.CommandsListBox.Location = new System.Drawing.Point(8, 6);
            this.CommandsListBox.Name = "CommandsListBox";
            this.CommandsListBox.Size = new System.Drawing.Size(460, 511);
            this.CommandsListBox.TabIndex = 0;
            this.CommandsListBox.SelectedIndexChanged += new System.EventHandler(this.CommandsListBox_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chatListBox);
            this.tabPage3.Controls.Add(this.chattersListBox);
            this.tabPage3.Controls.Add(this.sendButton);
            this.tabPage3.Controls.Add(this.messageTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1000, 535);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Console";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // chatListBox
            // 
            this.chatListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatListBox.FormattingEnabled = true;
            this.chatListBox.HorizontalScrollbar = true;
            this.chatListBox.Location = new System.Drawing.Point(3, 43);
            this.chatListBox.Name = "chatListBox";
            this.chatListBox.Size = new System.Drawing.Size(822, 489);
            this.chatListBox.TabIndex = 10;
            this.chatListBox.SelectedIndexChanged += new System.EventHandler(this.chatListBox_SelectedIndexChanged);
            // 
            // chattersListBox
            // 
            this.chattersListBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.chattersListBox.FormattingEnabled = true;
            this.chattersListBox.Location = new System.Drawing.Point(825, 43);
            this.chattersListBox.Name = "chattersListBox";
            this.chattersListBox.Size = new System.Drawing.Size(172, 489);
            this.chattersListBox.TabIndex = 8;
            // 
            // sendButton
            // 
            this.sendButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.sendButton.Location = new System.Drawing.Point(3, 23);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(994, 20);
            this.sendButton.TabIndex = 7;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.AutoCompleteCustomSource.AddRange(new string[] {
            "!reload",
            "!vote start"});
            this.messageTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.messageTextBox.Location = new System.Drawing.Point(3, 3);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(994, 20);
            this.messageTextBox.TabIndex = 6;
            this.messageTextBox.TextChanged += new System.EventHandler(this.messageTextBox_TextChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.OATokenTextBox);
            this.tabPage1.Controls.Add(this.UsernameTextBox);
            this.tabPage1.Controls.Add(this.openConfigFolderButton);
            this.tabPage1.Controls.Add(this.joinButton);
            this.tabPage1.Controls.Add(this.channelTextBox);
            this.tabPage1.Controls.Add(this.connectButton);
            this.tabPage1.Controls.Add(this.connectionStateLabel);
            this.tabPage1.Controls.Add(this.constConnectionStateLabel);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1000, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main Settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(8, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "OAToken";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(8, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Username";
            // 
            // OATokenTextBox
            // 
            this.OATokenTextBox.BorderColorFocused = System.Drawing.Color.Blue;
            this.OATokenTextBox.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OATokenTextBox.BorderColorMouseHover = System.Drawing.Color.Blue;
            this.OATokenTextBox.BorderThickness = 3;
            this.OATokenTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.OATokenTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.OATokenTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OATokenTextBox.isPassword = true;
            this.OATokenTextBox.Location = new System.Drawing.Point(107, 102);
            this.OATokenTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.OATokenTextBox.Name = "OATokenTextBox";
            this.OATokenTextBox.Size = new System.Drawing.Size(344, 41);
            this.OATokenTextBox.TabIndex = 10;
            this.OATokenTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.BorderColorFocused = System.Drawing.Color.Blue;
            this.UsernameTextBox.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.UsernameTextBox.BorderColorMouseHover = System.Drawing.Color.Blue;
            this.UsernameTextBox.BorderThickness = 3;
            this.UsernameTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.UsernameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.UsernameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.UsernameTextBox.isPassword = false;
            this.UsernameTextBox.Location = new System.Drawing.Point(107, 53);
            this.UsernameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(344, 41);
            this.UsernameTextBox.TabIndex = 9;
            this.UsernameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // openConfigFolderButton
            // 
            this.openConfigFolderButton.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.openConfigFolderButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.openConfigFolderButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.openConfigFolderButton.BorderRadius = 0;
            this.openConfigFolderButton.ButtonText = "Open Configs Folder";
            this.openConfigFolderButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openConfigFolderButton.DisabledColor = System.Drawing.Color.Gray;
            this.openConfigFolderButton.Iconcolor = System.Drawing.Color.Transparent;
            this.openConfigFolderButton.Iconimage = ((System.Drawing.Image)(resources.GetObject("openConfigFolderButton.Iconimage")));
            this.openConfigFolderButton.Iconimage_right = null;
            this.openConfigFolderButton.Iconimage_right_Selected = null;
            this.openConfigFolderButton.Iconimage_Selected = null;
            this.openConfigFolderButton.IconMarginLeft = 0;
            this.openConfigFolderButton.IconMarginRight = 0;
            this.openConfigFolderButton.IconRightVisible = false;
            this.openConfigFolderButton.IconRightZoom = 0D;
            this.openConfigFolderButton.IconVisible = false;
            this.openConfigFolderButton.IconZoom = 90D;
            this.openConfigFolderButton.IsTab = false;
            this.openConfigFolderButton.Location = new System.Drawing.Point(753, 7);
            this.openConfigFolderButton.Name = "openConfigFolderButton";
            this.openConfigFolderButton.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.openConfigFolderButton.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.openConfigFolderButton.OnHoverTextColor = System.Drawing.Color.White;
            this.openConfigFolderButton.selected = false;
            this.openConfigFolderButton.Size = new System.Drawing.Size(239, 41);
            this.openConfigFolderButton.TabIndex = 8;
            this.openConfigFolderButton.Text = "Open Configs Folder";
            this.openConfigFolderButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openConfigFolderButton.Textcolor = System.Drawing.Color.White;
            this.openConfigFolderButton.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openConfigFolderButton.Click += new System.EventHandler(this.openConfigFolderButton_Click);
            // 
            // joinButton
            // 
            this.joinButton.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.joinButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.joinButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.joinButton.BorderRadius = 0;
            this.joinButton.ButtonText = "Join";
            this.joinButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.joinButton.DisabledColor = System.Drawing.Color.Gray;
            this.joinButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.joinButton.Iconcolor = System.Drawing.Color.Transparent;
            this.joinButton.Iconimage = global::TwitchBot.Properties.Resources.fa_connected;
            this.joinButton.Iconimage_right = null;
            this.joinButton.Iconimage_right_Selected = null;
            this.joinButton.Iconimage_Selected = null;
            this.joinButton.IconMarginLeft = 0;
            this.joinButton.IconMarginRight = 0;
            this.joinButton.IconRightVisible = false;
            this.joinButton.IconRightZoom = 0D;
            this.joinButton.IconVisible = false;
            this.joinButton.IconZoom = 90D;
            this.joinButton.IsTab = false;
            this.joinButton.Location = new System.Drawing.Point(457, 7);
            this.joinButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.joinButton.Name = "joinButton";
            this.joinButton.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.joinButton.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.joinButton.OnHoverTextColor = System.Drawing.Color.White;
            this.joinButton.selected = false;
            this.joinButton.Size = new System.Drawing.Size(42, 41);
            this.joinButton.TabIndex = 7;
            this.joinButton.Text = "Join";
            this.joinButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.joinButton.Textcolor = System.Drawing.Color.White;
            this.joinButton.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.joinButton.Click += new System.EventHandler(this.bunifuFlatButton1_Click);
            // 
            // channelTextBox
            // 
            this.channelTextBox.BorderColorFocused = System.Drawing.Color.Blue;
            this.channelTextBox.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.channelTextBox.BorderColorMouseHover = System.Drawing.Color.Blue;
            this.channelTextBox.BorderThickness = 3;
            this.channelTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.channelTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.channelTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.channelTextBox.isPassword = false;
            this.channelTextBox.Location = new System.Drawing.Point(107, 7);
            this.channelTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.channelTextBox.Name = "channelTextBox";
            this.channelTextBox.Size = new System.Drawing.Size(344, 41);
            this.channelTextBox.TabIndex = 6;
            this.channelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // connectButton
            // 
            this.connectButton.BackColor = System.Drawing.Color.NavajoWhite;
            this.connectButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.connectButton.Location = new System.Drawing.Point(183, 509);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 22);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = false;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // connectionStateLabel
            // 
            this.connectionStateLabel.AutoSize = true;
            this.connectionStateLabel.Location = new System.Drawing.Point(104, 514);
            this.connectionStateLabel.Name = "connectionStateLabel";
            this.connectionStateLabel.Size = new System.Drawing.Size(73, 13);
            this.connectionStateLabel.TabIndex = 4;
            this.connectionStateLabel.Text = "Disconnected";
            // 
            // constConnectionStateLabel
            // 
            this.constConnectionStateLabel.AutoSize = true;
            this.constConnectionStateLabel.Location = new System.Drawing.Point(8, 514);
            this.constConnectionStateLabel.Name = "constConnectionStateLabel";
            this.constConnectionStateLabel.Size = new System.Drawing.Size(90, 13);
            this.constConnectionStateLabel.TabIndex = 3;
            this.constConnectionStateLabel.Text = "Connection state:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(8, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Channel";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1008, 561);
            this.tabControl.TabIndex = 0;
            // 
            // DialogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.tabControl);
            this.Name = "DialogWindow";
            this.Text = "Twitch bot API. State: Disconnected";
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox moduleCheckBox;
        private System.Windows.Forms.Button moduleReloadButton;
        private System.Windows.Forms.Label ModuleName;
        public System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox commandFeedbackTextBox;
        private System.Windows.Forms.TextBox commandsCommandTextBox;
        private System.Windows.Forms.Button commandsMinusButton;
        private System.Windows.Forms.Button commandsReplaceButton;
        private System.Windows.Forms.Button commandsPlusButton;
        protected System.Windows.Forms.ListBox CommandsListBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox chatListBox;
        private System.Windows.Forms.ListBox chattersListBox;
        public System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.TabPage tabPage1;
        internal Bunifu.Framework.UI.BunifuFlatButton joinButton;
        private Bunifu.Framework.UI.BunifuMetroTextbox channelTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label connectionStateLabel;
        private System.Windows.Forms.Label constConnectionStateLabel;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TabControl tabControl;
        private Bunifu.Framework.UI.BunifuFlatButton openConfigFolderButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuMetroTextbox OATokenTextBox;
        private Bunifu.Framework.UI.BunifuMetroTextbox UsernameTextBox;

        public static System.Windows.Forms.Timer Timer => singleton.timer;
    }
}