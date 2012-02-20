using IdleMiner.Properties;

namespace IdleMiner
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minerSettingsGroup = new System.Windows.Forms.GroupBox();
            this.hideLaunchCheckbox = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageBasic = new System.Windows.Forms.TabPage();
            this.passwordInput = new System.Windows.Forms.TextBox();
            this.usernameInput = new System.Windows.Forms.TextBox();
            this.addressInput = new System.Windows.Forms.TextBox();
            this.workSizeComboBox = new System.Windows.Forms.ComboBox();
            this.workSizeSelectorLabel = new System.Windows.Forms.Label();
            this.passwordInputLabel = new System.Windows.Forms.Label();
            this.usernameInputLabel = new System.Windows.Forms.Label();
            this.poolAddressInputLabel = new System.Windows.Forms.Label();
            this.vectorCheckbox = new System.Windows.Forms.CheckBox();
            this.deviceSelection = new System.Windows.Forms.ComboBox();
            this.deviceSelectorLabel = new System.Windows.Forms.Label();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.argumentsInput = new System.Windows.Forms.TextBox();
            this.argumentsLabel = new System.Windows.Forms.Label();
            this.browseForProgramButton = new System.Windows.Forms.Button();
            this.programInput = new System.Windows.Forms.TextBox();
            this.launchThisLabel = new System.Windows.Forms.Label();
            this.activitySettingsGroup = new System.Windows.Forms.GroupBox();
            this.delayInput = new System.Windows.Forms.TextBox();
            this.beginMiningLabel = new System.Windows.Forms.Label();
            this.broughtToYouLabel = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.checkTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIconMenu.SuspendLayout();
            this.minerSettingsGroup.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageBasic.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            this.activitySettingsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyIconMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = global::IdleMiner.Properties.Resources.Application_Title;
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.NotifyIconBalloonTipClicked);
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIconMouseDoubleClick);
            // 
            // notifyIconMenu
            // 
            this.notifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.notifyIconMenu.Name = "notifyIconMenu";
            this.notifyIconMenu.Size = new System.Drawing.Size(93, 26);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = global::IdleMiner.Properties.Resources.Exit_Label;
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // minerSettingsGroup
            // 
            this.minerSettingsGroup.Controls.Add(this.hideLaunchCheckbox);
            this.minerSettingsGroup.Controls.Add(this.tabControl);
            this.minerSettingsGroup.Location = new System.Drawing.Point(2, 2);
            this.minerSettingsGroup.Name = "minerSettingsGroup";
            this.minerSettingsGroup.Size = new System.Drawing.Size(281, 205);
            this.minerSettingsGroup.TabIndex = 1;
            this.minerSettingsGroup.TabStop = false;
            this.minerSettingsGroup.Text = "Miner Settings";
            // 
            // hideLaunchCheckbox
            // 
            this.hideLaunchCheckbox.AutoSize = true;
            this.hideLaunchCheckbox.Checked = true;
            this.hideLaunchCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hideLaunchCheckbox.Location = new System.Drawing.Point(145, 16);
            this.hideLaunchCheckbox.Name = "hideLaunchCheckbox";
            this.hideLaunchCheckbox.Size = new System.Drawing.Size(128, 17);
            this.hideLaunchCheckbox.TabIndex = 1;
            this.hideLaunchCheckbox.Text = "Hide launch window?";
            this.hideLaunchCheckbox.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageBasic);
            this.tabControl.Controls.Add(this.tabPageAdvanced);
            this.tabControl.Location = new System.Drawing.Point(6, 19);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(271, 180);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageBasic
            // 
            this.tabPageBasic.Controls.Add(this.passwordInput);
            this.tabPageBasic.Controls.Add(this.usernameInput);
            this.tabPageBasic.Controls.Add(this.addressInput);
            this.tabPageBasic.Controls.Add(this.workSizeComboBox);
            this.tabPageBasic.Controls.Add(this.workSizeSelectorLabel);
            this.tabPageBasic.Controls.Add(this.passwordInputLabel);
            this.tabPageBasic.Controls.Add(this.usernameInputLabel);
            this.tabPageBasic.Controls.Add(this.poolAddressInputLabel);
            this.tabPageBasic.Controls.Add(this.vectorCheckbox);
            this.tabPageBasic.Controls.Add(this.deviceSelection);
            this.tabPageBasic.Controls.Add(this.deviceSelectorLabel);
            this.tabPageBasic.Location = new System.Drawing.Point(4, 22);
            this.tabPageBasic.Name = "tabPageBasic";
            this.tabPageBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBasic.Size = new System.Drawing.Size(263, 154);
            this.tabPageBasic.TabIndex = 0;
            this.tabPageBasic.Text = global::IdleMiner.Properties.Resources.Basic;
            this.tabPageBasic.UseVisualStyleBackColor = true;
            // 
            // passwordInput
            // 
            this.passwordInput.Location = new System.Drawing.Point(92, 100);
            this.passwordInput.Name = "passwordInput";
            this.passwordInput.PasswordChar = '*';
            this.passwordInput.Size = new System.Drawing.Size(165, 20);
            this.passwordInput.TabIndex = 10;
            this.passwordInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // usernameInput
            // 
            this.usernameInput.Location = new System.Drawing.Point(92, 74);
            this.usernameInput.Name = "usernameInput";
            this.usernameInput.Size = new System.Drawing.Size(165, 20);
            this.usernameInput.TabIndex = 9;
            this.usernameInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // addressInput
            // 
            this.addressInput.Location = new System.Drawing.Point(92, 46);
            this.addressInput.Name = "addressInput";
            this.addressInput.Size = new System.Drawing.Size(165, 20);
            this.addressInput.TabIndex = 8;
            this.addressInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // workSizeComboBox
            // 
            this.workSizeComboBox.FormattingEnabled = true;
            this.workSizeComboBox.Items.AddRange(new object[] {
            "32",
            "64",
            "128",
            "256",
            "512"});
            this.workSizeComboBox.Location = new System.Drawing.Point(191, 128);
            this.workSizeComboBox.Name = "workSizeComboBox";
            this.workSizeComboBox.Size = new System.Drawing.Size(66, 21);
            this.workSizeComboBox.TabIndex = 7;
            // 
            // workSizeSelectorLabel
            // 
            this.workSizeSelectorLabel.AutoSize = true;
            this.workSizeSelectorLabel.Location = new System.Drawing.Point(127, 132);
            this.workSizeSelectorLabel.Name = "workSizeSelectorLabel";
            this.workSizeSelectorLabel.Size = new System.Drawing.Size(59, 13);
            this.workSizeSelectorLabel.TabIndex = 6;
            this.workSizeSelectorLabel.Text = "Work Size:";
            // 
            // passwordInputLabel
            // 
            this.passwordInputLabel.AutoSize = true;
            this.passwordInputLabel.Location = new System.Drawing.Point(6, 105);
            this.passwordInputLabel.Name = "passwordInputLabel";
            this.passwordInputLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordInputLabel.TabIndex = 5;
            this.passwordInputLabel.Text = "Password:";
            // 
            // usernameInputLabel
            // 
            this.usernameInputLabel.AutoSize = true;
            this.usernameInputLabel.Location = new System.Drawing.Point(6, 77);
            this.usernameInputLabel.Name = "usernameInputLabel";
            this.usernameInputLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameInputLabel.TabIndex = 4;
            this.usernameInputLabel.Text = "Username:";
            // 
            // poolAddressInputLabel
            // 
            this.poolAddressInputLabel.AutoSize = true;
            this.poolAddressInputLabel.Location = new System.Drawing.Point(6, 49);
            this.poolAddressInputLabel.Name = "poolAddressInputLabel";
            this.poolAddressInputLabel.Size = new System.Drawing.Size(72, 13);
            this.poolAddressInputLabel.TabIndex = 3;
            this.poolAddressInputLabel.Text = "Pool Address:";
            // 
            // vectorCheckbox
            // 
            this.vectorCheckbox.AutoSize = true;
            this.vectorCheckbox.Checked = true;
            this.vectorCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vectorCheckbox.Location = new System.Drawing.Point(9, 132);
            this.vectorCheckbox.Name = "vectorCheckbox";
            this.vectorCheckbox.Size = new System.Drawing.Size(89, 17);
            this.vectorCheckbox.TabIndex = 2;
            this.vectorCheckbox.Text = global::IdleMiner.Properties.Resources.Use_Vectors_Label;
            this.vectorCheckbox.UseVisualStyleBackColor = true;
            // 
            // deviceSelection
            // 
            this.deviceSelection.FormattingEnabled = true;
            this.deviceSelection.Location = new System.Drawing.Point(92, 14);
            this.deviceSelection.Name = "deviceSelection";
            this.deviceSelection.Size = new System.Drawing.Size(165, 21);
            this.deviceSelection.TabIndex = 1;
            this.deviceSelection.SelectedValueChanged += new System.EventHandler(this.DeviceSelectionSelectedValueChanged);
            // 
            // deviceSelectorLabel
            // 
            this.deviceSelectorLabel.AutoSize = true;
            this.deviceSelectorLabel.Location = new System.Drawing.Point(6, 17);
            this.deviceSelectorLabel.Name = "deviceSelectorLabel";
            this.deviceSelectorLabel.Size = new System.Drawing.Size(86, 13);
            this.deviceSelectorLabel.TabIndex = 0;
            this.deviceSelectorLabel.Text = "OpenCL Device:";
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.argumentsInput);
            this.tabPageAdvanced.Controls.Add(this.argumentsLabel);
            this.tabPageAdvanced.Controls.Add(this.browseForProgramButton);
            this.tabPageAdvanced.Controls.Add(this.programInput);
            this.tabPageAdvanced.Controls.Add(this.launchThisLabel);
            this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdvanced.Size = new System.Drawing.Size(263, 154);
            this.tabPageAdvanced.TabIndex = 1;
            this.tabPageAdvanced.Text = global::IdleMiner.Properties.Resources.Advanced;
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // argumentsInput
            // 
            this.argumentsInput.Location = new System.Drawing.Point(6, 79);
            this.argumentsInput.Name = "argumentsInput";
            this.argumentsInput.Size = new System.Drawing.Size(251, 20);
            this.argumentsInput.TabIndex = 4;
            // 
            // argumentsLabel
            // 
            this.argumentsLabel.AutoSize = true;
            this.argumentsLabel.Location = new System.Drawing.Point(3, 63);
            this.argumentsLabel.Name = "argumentsLabel";
            this.argumentsLabel.Size = new System.Drawing.Size(113, 13);
            this.argumentsLabel.TabIndex = 3;
            this.argumentsLabel.Text = "With these arguments:";
            // 
            // browseForProgramButton
            // 
            this.browseForProgramButton.Location = new System.Drawing.Point(233, 30);
            this.browseForProgramButton.Name = "browseForProgramButton";
            this.browseForProgramButton.Size = new System.Drawing.Size(24, 20);
            this.browseForProgramButton.TabIndex = 2;
            this.browseForProgramButton.Text = "...";
            this.browseForProgramButton.UseVisualStyleBackColor = true;
            this.browseForProgramButton.Click += new System.EventHandler(this.BrowseForProgramButtonClick);
            // 
            // programInput
            // 
            this.programInput.Location = new System.Drawing.Point(6, 30);
            this.programInput.Name = "programInput";
            this.programInput.Size = new System.Drawing.Size(227, 20);
            this.programInput.TabIndex = 1;
            // 
            // launchThisLabel
            // 
            this.launchThisLabel.AutoSize = true;
            this.launchThisLabel.Location = new System.Drawing.Point(3, 14);
            this.launchThisLabel.Name = "launchThisLabel";
            this.launchThisLabel.Size = new System.Drawing.Size(106, 13);
            this.launchThisLabel.TabIndex = 0;
            this.launchThisLabel.Text = "Launch this program:";
            // 
            // activitySettingsGroup
            // 
            this.activitySettingsGroup.Controls.Add(this.delayInput);
            this.activitySettingsGroup.Controls.Add(this.beginMiningLabel);
            this.activitySettingsGroup.Location = new System.Drawing.Point(2, 213);
            this.activitySettingsGroup.Name = "activitySettingsGroup";
            this.activitySettingsGroup.Size = new System.Drawing.Size(281, 60);
            this.activitySettingsGroup.TabIndex = 2;
            this.activitySettingsGroup.TabStop = false;
            this.activitySettingsGroup.Text = "Activity Settings";
            // 
            // delayInput
            // 
            this.delayInput.Location = new System.Drawing.Point(6, 32);
            this.delayInput.Name = "delayInput";
            this.delayInput.Size = new System.Drawing.Size(269, 20);
            this.delayInput.TabIndex = 1;
            // 
            // beginMiningLabel
            // 
            this.beginMiningLabel.AutoSize = true;
            this.beginMiningLabel.Location = new System.Drawing.Point(10, 16);
            this.beginMiningLabel.Name = "beginMiningLabel";
            this.beginMiningLabel.Size = new System.Drawing.Size(227, 13);
            this.beginMiningLabel.TabIndex = 0;
            this.beginMiningLabel.Text = "Begin mining when computer has been idle for:";
            // 
            // broughtToYouLabel
            // 
            this.broughtToYouLabel.AutoSize = true;
            this.broughtToYouLabel.Location = new System.Drawing.Point(5, 276);
            this.broughtToYouLabel.Name = "broughtToYouLabel";
            this.broughtToYouLabel.Size = new System.Drawing.Size(157, 13);
            this.broughtToYouLabel.TabIndex = 3;
            this.broughtToYouLabel.Text = "Brought to you by JonnyFunFun";
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.Location = new System.Drawing.Point(118, 289);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(165, 13);
            this.linkLabel.TabIndex = 4;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "http://idleminer.jonnyfunfun.com/";
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelLinkClicked);
            // 
            // checkTimer
            // 
            this.checkTimer.Enabled = true;
            this.checkTimer.Interval = 5000;
            this.checkTimer.Tick += new System.EventHandler(this.CheckTimerTick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 309);
            this.Controls.Add(this.minerSettingsGroup);
            this.Controls.Add(this.activitySettingsGroup);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.broughtToYouLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "IdleMiner";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Shown += new System.EventHandler(MainFormShown    );
            this.notifyIconMenu.ResumeLayout(false);
            this.minerSettingsGroup.ResumeLayout(false);
            this.minerSettingsGroup.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageBasic.ResumeLayout(false);
            this.tabPageBasic.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.tabPageAdvanced.PerformLayout();
            this.activitySettingsGroup.ResumeLayout(false);
            this.activitySettingsGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyIconMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox minerSettingsGroup;
        private System.Windows.Forms.GroupBox activitySettingsGroup;
        private System.Windows.Forms.Label beginMiningLabel;
        private System.Windows.Forms.TextBox delayInput;
        private System.Windows.Forms.Label broughtToYouLabel;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.Timer checkTimer;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageBasic;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.Label deviceSelectorLabel;
        private System.Windows.Forms.ComboBox deviceSelection;
        private System.Windows.Forms.CheckBox vectorCheckbox;
        private System.Windows.Forms.Label passwordInputLabel;
        private System.Windows.Forms.Label usernameInputLabel;
        private System.Windows.Forms.Label poolAddressInputLabel;
        private System.Windows.Forms.Label workSizeSelectorLabel;
        private System.Windows.Forms.ComboBox workSizeComboBox;
        private System.Windows.Forms.TextBox addressInput;
        private System.Windows.Forms.TextBox usernameInput;
        private System.Windows.Forms.TextBox passwordInput;
        private System.Windows.Forms.CheckBox hideLaunchCheckbox;
        private System.Windows.Forms.Label launchThisLabel;
        private System.Windows.Forms.TextBox programInput;
        private System.Windows.Forms.Button browseForProgramButton;
        private System.Windows.Forms.Label argumentsLabel;
        private System.Windows.Forms.TextBox argumentsInput;
    }
}

