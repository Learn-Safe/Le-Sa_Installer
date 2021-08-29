using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Le_Sa_Installer
{
    public partial class formAskForUninstall : Form
    {
        private Point lastPoint;
        private static readonly string mainDrive = Path.GetPathRoot(Environment.SystemDirectory);
        private static string currentUserFolder = $@"{mainDrive}Users\{userName()}";
        private static string currentUserTempFolder = $@"{currentUserFolder}\AppData\Local\Temp";
        private static readonly string fileName = String.Concat(Process.GetCurrentProcess().ProcessName, ".exe");
        private static string currentUserInstallationFolder = $@"{currentUserFolder}\AppData\Local\Programs\Le-Sa";

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

        private static string userName()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            return (collection.Cast<ManagementBaseObject>().First()["UserName"].ToString().Split('\\')[1]);
        }


        private void btnNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (File.Exists($@"{currentUserInstallationFolder}\{fileName}"))
            {
                if (File.Exists(Path.Combine(currentUserTempFolder, fileName)))
                {
                    File.Delete(Path.Combine(currentUserTempFolder, fileName));
                }
                File.Copy($@"{currentUserInstallationFolder}\{fileName}", Path.Combine(currentUserTempFolder, fileName));
                FinishUninstallation();
                Close();
            }
            else
            {
                MessageBox.Show("Uninstaller Missing", "Error",MessageBoxButtons.OK, MessageBoxIcon.Question);
                Close();
            }
        }

        private void FinishUninstallation()
        {
            ProcessStartInfo finish = new ProcessStartInfo
            {
                Verb = "runas",
                WorkingDirectory = currentUserTempFolder,
                FileName = fileName,
                Arguments = "--finish",
            };
            try
            {
                Process.Start(finish);
            }
            catch
            {
                Close();
            }
        }

        private void pnlTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void pnlTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
    }
}
