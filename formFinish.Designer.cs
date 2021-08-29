
namespace Le_Sa_Installer
{
    partial class formFinish
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formFinish));
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.lblLocationMsg = new System.Windows.Forms.Label();
            this.pBoxLogo = new System.Windows.Forms.PictureBox();
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.lblUninstaller = new System.Windows.Forms.Label();
            this.progressUninstalling = new System.Windows.Forms.ProgressBar();
            this.pnlBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxLogo)).BeginInit();
            this.pnlTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBackground
            // 
            this.pnlBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.pnlBackground.Controls.Add(this.progressUninstalling);
            this.pnlBackground.Controls.Add(this.lblLocationMsg);
            this.pnlBackground.Controls.Add(this.pBoxLogo);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBackground.Location = new System.Drawing.Point(0, 28);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(397, 92);
            this.pnlBackground.TabIndex = 7;
            // 
            // lblLocationMsg
            // 
            this.lblLocationMsg.AutoSize = true;
            this.lblLocationMsg.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocationMsg.ForeColor = System.Drawing.Color.White;
            this.lblLocationMsg.Location = new System.Drawing.Point(50, 3);
            this.lblLocationMsg.Name = "lblLocationMsg";
            this.lblLocationMsg.Size = new System.Drawing.Size(149, 21);
            this.lblLocationMsg.TabIndex = 2;
            this.lblLocationMsg.Text = "Le-Sa is uninstalling\r\n";
            // 
            // pBoxLogo
            // 
            this.pBoxLogo.Image = global::Le_Sa_Installer.Properties.Resources.Le_Sa_1001px_png;
            this.pBoxLogo.Location = new System.Drawing.Point(-43, 1);
            this.pBoxLogo.Name = "pBoxLogo";
            this.pBoxLogo.Size = new System.Drawing.Size(87, 87);
            this.pBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBoxLogo.TabIndex = 1;
            this.pBoxLogo.TabStop = false;
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(36)))));
            this.pnlTitleBar.Controls.Add(this.lblUninstaller);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(397, 28);
            this.pnlTitleBar.TabIndex = 6;
            // 
            // lblUninstaller
            // 
            this.lblUninstaller.AutoSize = true;
            this.lblUninstaller.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUninstaller.ForeColor = System.Drawing.Color.White;
            this.lblUninstaller.Location = new System.Drawing.Point(164, 6);
            this.lblUninstaller.Name = "lblUninstaller";
            this.lblUninstaller.Size = new System.Drawing.Size(68, 16);
            this.lblUninstaller.TabIndex = 5;
            this.lblUninstaller.Text = "Uninstallig";
            // 
            // progressUninstalling
            // 
            this.progressUninstalling.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressUninstalling.Location = new System.Drawing.Point(0, 87);
            this.progressUninstalling.Name = "progressUninstalling";
            this.progressUninstalling.Size = new System.Drawing.Size(397, 5);
            this.progressUninstalling.TabIndex = 3;
            // 
            // formFinish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 120);
            this.Controls.Add(this.pnlBackground);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formFinish";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Uninstalling Le-Sa";
            this.Load += new System.EventHandler(this.formFinish_Load);
            this.pnlBackground.ResumeLayout(false);
            this.pnlBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxLogo)).EndInit();
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlTitleBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Label lblLocationMsg;
        private System.Windows.Forms.PictureBox pBoxLogo;
        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.ProgressBar progressUninstalling;
        private System.Windows.Forms.Label lblUninstaller;
    }
}