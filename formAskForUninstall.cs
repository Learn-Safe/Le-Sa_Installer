using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Le_Sa_Installer
{
    public partial class formAskForUninstall : Form
    {
        private static readonly string tempFolder = Path.GetTempPath();
        private static readonly string fileName = String.Concat(Process.GetCurrentProcess().ProcessName, ".exe");
        private static readonly string fileCurrentPath = Path.Combine(Environment.CurrentDirectory, fileName);

        public formAskForUninstall()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }

        #region Round Corners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        #endregion

        #region Title Bar
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.close_27px;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.close_12px;
        }
        #endregion

        private void btnNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Combine(tempFolder, fileName)))
            {
                File.Copy(fileCurrentPath, Path.Combine(tempFolder, fileName));
            }
            FinishUninstallation();
            Close();
        }

        private void FinishUninstallation()
        {
            ProcessStartInfo finish = new ProcessStartInfo
            {
                UseShellExecute = true,
                Verb = "runas",
                WorkingDirectory = tempFolder,
                FileName = fileName,
                Arguments = "--finish",
            };
            Process.Start(finish);
            Close();
        }
    }
}
