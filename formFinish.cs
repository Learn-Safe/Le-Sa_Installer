using Le_Sa_Installer.Models.AdminCheck;
using Microsoft.Win32;
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
    public partial class formFinish : Form
    {
        private static readonly string ApplicationName = "Le-Sa";

        private static readonly string tempFolder = Path.GetTempPath();
        private static readonly string fileName = String.Concat(Process.GetCurrentProcess().ProcessName, ".exe");
        private static readonly string fileCurrentPath = Path.Combine(Environment.CurrentDirectory, fileName);
        private static readonly string startMenue = $@"{mainDrive}ProgramData\Microsoft\Windows\Start Menu\Programs";

        private static readonly string mainDrive = Path.GetPathRoot(Environment.SystemDirectory);
        private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        private static string currentUserFolder = $@"{mainDrive}Users\{userName()}";
        private static string currentUserInstallationFolder = $@"{currentUserFolder}\AppData\Local\Programs\Le-Sa";

        private static string allUsersInstallationFolder = $@"{mainDrive}Program Files (x86)\Le-Sa";

        private static readonly string currentUserUninstallerRegKeys = @"SOFTWARE\";
        private static readonly string LocalMachineUninstallerRegKeys = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\";

        public formFinish()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            progressUninstalling.Maximum = 0;
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

        private void formFinish_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(allUsersInstallationFolder))
            {
                try
                {
                    Directory.Delete(allUsersInstallationFolder, true);
                }
                catch (IOException)
                {
                    MessageBox.Show("You need to close Le-Sa before Uninstalling.\n Close Le-Sa and retry again.", "Le-Sa is already running", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                }
            }

            if (Directory.Exists(currentUserInstallationFolder))
            {
                try
                {
                    Directory.Delete(allUsersInstallationFolder, true);
                }
                catch (IOException)
                {
                    MessageBox.Show("You need to close Le-Sa before Uninstalling.\n Close Le-Sa and retry again.", "Le-Sa is already running", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                }
            }

            if (File.Exists(Path.Combine(desktopPath, "Le-Sa.lnk")))
            {
                File.Delete(Path.Combine(desktopPath, "Le-Sa.lnk"));
            }

            if (File.Exists(Path.Combine(startMenue, "Le-Sa.lnk")))
            {
                File.Delete(Path.Combine(startMenue, "Le-Sa.lnk"));
            }

            try
            {
                Registry.CurrentUser.DeleteSubKeyTree(currentUserUninstallerRegKeys + ApplicationName, false);
                Registry.LocalMachine.DeleteSubKeyTree(LocalMachineUninstallerRegKeys + ApplicationName, false);
            }
            catch
            {
                // Do nothing
            }
        }

        #region Check Le-Sa Status
        private static bool IsLeSaRunning()
        {
            if (Process.GetProcessesByName(ApplicationName + ".exe").Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Self Destruction Installer
        private void SelfDestruct()
        {
            Process procDestruct = new Process();
            string strName = "destruct.bat";
            string strPath = Path.Combine(Directory
               .GetCurrentDirectory(), strName);
            string strExe = new FileInfo(Application.ExecutablePath)
               .Name;

            StreamWriter swDestruct = new StreamWriter(strPath);

            swDestruct.WriteLine("attrib \"" + strExe + "\"" +
             " -a -s -r -h");
            swDestruct.WriteLine(":Repeat");
            swDestruct.WriteLine("del " + "\"" + strExe + "\"");
            swDestruct.WriteLine("if exist \"" + strExe + "\"" +
               " goto Repeat");
            swDestruct.WriteLine("del \"" + strName + "\"");
            swDestruct.Close();

            procDestruct.StartInfo.FileName = "destruct.bat";

            procDestruct.StartInfo.CreateNoWindow = true;
            procDestruct.StartInfo.UseShellExecute = false;

            try
            {
                procDestruct.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                Close();
            }
        }
        #endregion

    }
}
