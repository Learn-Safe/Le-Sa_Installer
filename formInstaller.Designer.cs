
namespace Le_Sa_Installer
{
    partial class formInstaller
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
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.pnlSeparator = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.cBoxDesktopShoutcut = new System.Windows.Forms.CheckBox();
            this.rBtnCurrentUser = new System.Windows.Forms.RadioButton();
            this.rBtnAllUsers = new System.Windows.Forms.RadioButton();
            this.lblInstallationFolder = new System.Windows.Forms.Label();
            this.lblLocationMsg = new System.Windows.Forms.Label();
            this.btnInstall = new System.Windows.Forms.Button();
            this.pBoxLogo = new System.Windows.Forms.PictureBox();
            this.pnlTitleBar.SuspendLayout();
            this.pnlBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(36)))));
            this.pnlTitleBar.Controls.Add(this.lblTitle);
            this.pnlTitleBar.Controls.Add(this.btnMinimize);
            this.pnlTitleBar.Controls.Add(this.pnlSeparator);
            this.pnlTitleBar.Controls.Add(this.btnClose);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(706, 28);
            this.pnlTitleBar.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(327, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(53, 16);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Installer";
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackgroundImage = global::Le_Sa_Installer.Properties.Resources.minimize_15px;
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(36)))));
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Location = new System.Drawing.Point(645, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(28, 28);
            this.btnMinimize.TabIndex = 3;
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            this.btnMinimize.MouseLeave += new System.EventHandler(this.btnMinimize_MouseLeave);
            this.btnMinimize.MouseHover += new System.EventHandler(this.btnMinimize_MouseHover);
            // 
            // pnlSeparator
            // 
            this.pnlSeparator.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSeparator.Location = new System.Drawing.Point(673, 0);
            this.pnlSeparator.Name = "pnlSeparator";
            this.pnlSeparator.Size = new System.Drawing.Size(5, 28);
            this.pnlSeparator.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::Le_Sa_Installer.Properties.Resources.close_12px;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(36)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(678, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(28, 28);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            this.btnClose.MouseHover += new System.EventHandler(this.btnClose_MouseHover);
            // 
            // pnlBackground
            // 
            this.pnlBackground.Controls.Add(this.lblStatus);
            this.pnlBackground.Controls.Add(this.lblWarning);
            this.pnlBackground.Controls.Add(this.progress);
            this.pnlBackground.Controls.Add(this.cBoxDesktopShoutcut);
            this.pnlBackground.Controls.Add(this.rBtnCurrentUser);
            this.pnlBackground.Controls.Add(this.rBtnAllUsers);
            this.pnlBackground.Controls.Add(this.lblInstallationFolder);
            this.pnlBackground.Controls.Add(this.lblLocationMsg);
            this.pnlBackground.Controls.Add(this.btnInstall);
            this.pnlBackground.Controls.Add(this.pBoxLogo);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBackground.Location = new System.Drawing.Point(0, 28);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(706, 219);
            this.pnlBackground.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(32, 193);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 16);
            this.lblStatus.TabIndex = 9;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.ForeColor = System.Drawing.Color.White;
            this.lblWarning.Location = new System.Drawing.Point(106, 136);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(477, 54);
            this.lblWarning.TabIndex = 8;
            this.lblWarning.Text = "warningMsg";
            // 
            // progress
            // 
            this.progress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progress.Location = new System.Drawing.Point(0, 214);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(706, 5);
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progress.TabIndex = 7;
            // 
            // cBoxDesktopShoutcut
            // 
            this.cBoxDesktopShoutcut.AutoSize = true;
            this.cBoxDesktopShoutcut.Checked = true;
            this.cBoxDesktopShoutcut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxDesktopShoutcut.FlatAppearance.BorderSize = 0;
            this.cBoxDesktopShoutcut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBoxDesktopShoutcut.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBoxDesktopShoutcut.ForeColor = System.Drawing.Color.White;
            this.cBoxDesktopShoutcut.Location = new System.Drawing.Point(110, 89);
            this.cBoxDesktopShoutcut.Name = "cBoxDesktopShoutcut";
            this.cBoxDesktopShoutcut.Size = new System.Drawing.Size(175, 20);
            this.cBoxDesktopShoutcut.TabIndex = 5;
            this.cBoxDesktopShoutcut.Text = "Create a desktop shortcut";
            this.cBoxDesktopShoutcut.UseVisualStyleBackColor = true;
            // 
            // rBtnCurrentUser
            // 
            this.rBtnCurrentUser.AutoSize = true;
            this.rBtnCurrentUser.Checked = true;
            this.rBtnCurrentUser.FlatAppearance.BorderSize = 0;
            this.rBtnCurrentUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rBtnCurrentUser.ForeColor = System.Drawing.Color.White;
            this.rBtnCurrentUser.Location = new System.Drawing.Point(471, 90);
            this.rBtnCurrentUser.Name = "rBtnCurrentUser";
            this.rBtnCurrentUser.Size = new System.Drawing.Size(79, 17);
            this.rBtnCurrentUser.TabIndex = 4;
            this.rBtnCurrentUser.TabStop = true;
            this.rBtnCurrentUser.Text = "currentUser";
            this.rBtnCurrentUser.UseVisualStyleBackColor = true;
            // 
            // rBtnAllUsers
            // 
            this.rBtnAllUsers.AutoSize = true;
            this.rBtnAllUsers.Enabled = false;
            this.rBtnAllUsers.FlatAppearance.BorderSize = 0;
            this.rBtnAllUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rBtnAllUsers.ForeColor = System.Drawing.Color.White;
            this.rBtnAllUsers.Location = new System.Drawing.Point(471, 112);
            this.rBtnAllUsers.Name = "rBtnAllUsers";
            this.rBtnAllUsers.Size = new System.Drawing.Size(65, 17);
            this.rBtnAllUsers.TabIndex = 2;
            this.rBtnAllUsers.Text = "All Users";
            this.rBtnAllUsers.UseVisualStyleBackColor = true;
            this.rBtnAllUsers.Visible = false;
            // 
            // lblInstallationFolder
            // 
            this.lblInstallationFolder.AutoSize = true;
            this.lblInstallationFolder.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold);
            this.lblInstallationFolder.ForeColor = System.Drawing.Color.White;
            this.lblInstallationFolder.Location = new System.Drawing.Point(119, 60);
            this.lblInstallationFolder.Name = "lblInstallationFolder";
            this.lblInstallationFolder.Size = new System.Drawing.Size(138, 18);
            this.lblInstallationFolder.TabIndex = 3;
            this.lblInstallationFolder.Text = "installationLocation";
            // 
            // lblLocationMsg
            // 
            this.lblLocationMsg.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocationMsg.ForeColor = System.Drawing.Color.White;
            this.lblLocationMsg.Location = new System.Drawing.Point(106, 10);
            this.lblLocationMsg.Name = "lblLocationMsg";
            this.lblLocationMsg.Size = new System.Drawing.Size(588, 51);
            this.lblLocationMsg.TabIndex = 2;
            this.lblLocationMsg.Text = "This setup will install the application into the following location. You can\'t ch" +
    "ange installation location.";
            // 
            // btnInstall
            // 
            this.btnInstall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(46)))), ((int)(((byte)(76)))));
            this.btnInstall.FlatAppearance.BorderSize = 0;
            this.btnInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstall.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInstall.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnInstall.Location = new System.Drawing.Point(589, 177);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(110, 30);
            this.btnInstall.TabIndex = 0;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = false;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // pBoxLogo
            // 
            this.pBoxLogo.Image = global::Le_Sa_Installer.Properties.Resources.Le_Sa_1001px_png;
            this.pBoxLogo.Location = new System.Drawing.Point(-99, 8);
            this.pBoxLogo.Name = "pBoxLogo";
            this.pBoxLogo.Size = new System.Drawing.Size(199, 199);
            this.pBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBoxLogo.TabIndex = 1;
            this.pBoxLogo.TabStop = false;
            // 
            // formInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(706, 247);
            this.Controls.Add(this.pnlBackground);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formInstaller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formInstaller";
            this.Load += new System.EventHandler(this.formInstaller_Load);
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlTitleBar.PerformLayout();
            this.pnlBackground.ResumeLayout(false);
            this.pnlBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Panel pnlSeparator;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.PictureBox pBoxLogo;
        private System.Windows.Forms.Label lblInstallationFolder;
        private System.Windows.Forms.Label lblLocationMsg;
        private System.Windows.Forms.RadioButton rBtnAllUsers;
        private System.Windows.Forms.RadioButton rBtnCurrentUser;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.CheckBox cBoxDesktopShoutcut;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label lblStatus;
    }
}