using Microsoft.Win32;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Le_Sa_Installer
{
    public partial class formFinish : Form
    {
        private Point lastPoint;
        private static readonly string ApplicationName = "Le-Sa";

        private static readonly string fileName = String.Concat(Process.GetCurrentProcess().ProcessName, ".exe");
        private readonly string currentUserTempFolder = $@"{currentUserFolder}\AppData\Local\Temp";
        private readonly string startMenue = $@"{mainDrive}ProgramData\Microsoft\Windows\Start Menu\Programs";

        private static readonly string mainDrive = Path.GetPathRoot(Environment.SystemDirectory);
        private readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        private static string currentUserFolder = $@"{mainDrive}Users\{userName()}";
        private string currentUserInstallationFolder = $@"{currentUserFolder}\AppData\Local\Programs\Le-Sa";
        private string userDataFolder = $@"{currentUserFolder}\AppData\Local\Le-Sa";

        //      private static string allUsersInstallationFolder = $@"{mainDrive}Program Files (x86)\Le-Sa";

        private readonly string currentUserUninstallerRegKeys = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\";
        private readonly string LocalMachineUninstallerRegKeys = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\";
        private readonly string chromeRegKeys = @"SOFTWARE\Policies\Google\Chrome";
        private readonly string EdgeRegKeys = @"SOFTWARE\Policies\Microsoft\Edge";
        private readonly string FirefoxRegKeys = @"SOFTWARE\Policies\Mozilla\Firefox";

        public formFinish()
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

        #region Current User Username
        private static string userName()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            return (collection.Cast<ManagementBaseObject>().First()["UserName"].ToString().Split('\\')[1]);
        }
        #endregion

        //#region Check Le-Sa Status
        //private static bool IsLeSaRunning()
        //{
        //    if (Process.GetProcessesByName(ApplicationName + ".exe").Length > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //#endregion

        #region Self Destruction Installer
        private void SelfDestruct()
        {
            Process procDestruct = new Process();
            string strName = "destruct.bat";
            string strPath = Path.Combine(currentUserTempFolder, strName);
            string strExe = new FileInfo(Application.ExecutablePath).Name;

            StreamWriter swDestruct = new StreamWriter(strPath);

            swDestruct.WriteLine("attrib \"" + strExe + "\"" + " -a -s -r -h");
            swDestruct.WriteLine(":Repeat");
            swDestruct.WriteLine("del " + "\"" + strExe + "\"");
            swDestruct.WriteLine("if exist \"" + strExe + "\"" + " goto Repeat");
            swDestruct.WriteLine("del \"" + strName + "\"");
            swDestruct.Close();

            procDestruct.StartInfo.FileName = "destruct.bat";

            procDestruct.StartInfo.CreateNoWindow = true;
            procDestruct.StartInfo.UseShellExecute = false;

            try
            {
                procDestruct.Start();
                Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Close();
            }
            finally
            {
                Close();
            }
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {

            btnYes.Visible = false;
            lblLocationMsg.Text = "Uninstalling Le-Sa....";
            btnNo.Visible = false;

            if (Directory.Exists(currentUserInstallationFolder))
            {
                try
                {
                    Directory.Delete(currentUserInstallationFolder, true);
                }
                catch (Exception delError)
                {
                    MessageBox.Show(delError.Message, "Error");
                }
            }

            if (File.Exists(Path.Combine(desktopPath, "Le-Sa.lnk")))
            {
                try
                {
                    File.Delete(Path.Combine(desktopPath, "Le-Sa.lnk"));
                }
                catch
                {

                }
            }

            if (File.Exists(Path.Combine(startMenue, "Le-Sa.lnk")))
            {
                try
                {
                    File.Delete(Path.Combine(startMenue, "Le-Sa.lnk"));
                }
                catch
                {

                }
            }

            if (File.Exists(userDataFolder))
            {
                try
                {
                    File.Delete(userDataFolder);
                }
                catch
                {

                }
            }

            try
            {
                Registry.CurrentUser.DeleteSubKeyTree(currentUserUninstallerRegKeys + ApplicationName, false);
                Registry.LocalMachine.DeleteSubKeyTree(LocalMachineUninstallerRegKeys + ApplicationName, false);
                Registry.CurrentUser.DeleteSubKeyTree(chromeRegKeys, false);
                Registry.CurrentUser.DeleteSubKeyTree(EdgeRegKeys, false);
                Registry.CurrentUser.DeleteSubKeyTree(FirefoxRegKeys, false);
            }
            catch
            {

            }
            lblLocationMsg.Text = "Uninstallation Finished.";
            SelfDestruct();
        }

        #region Drag
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
        #endregion

        private void btnNo_Click(object sender, EventArgs e)
        {
            Close();
            SelfDestruct();
        }
    }
}
