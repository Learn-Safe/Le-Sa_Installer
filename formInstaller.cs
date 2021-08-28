using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Management;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Net;
using System.ComponentModel;
using System.Collections.Generic;
using Le_Sa_Installer.Models.Registry;
using Microsoft.Win32;
using System.Diagnostics;
using System.DirectoryServices;

namespace Le_Sa_Installer
{
    public partial class formInstaller : Form
    {
        private bool IsAdminAccount = false;
        private static readonly string mainDrive = Path.GetPathRoot(Environment.SystemDirectory);
        private static readonly string currentDate = DateTime.Now.ToString("yyyyMMdd");

        private static string currentUserFolder = $@"{mainDrive}Users\{userName()}";
        private static string currentUserInstallationFolder = $@"{currentUserFolder}\AppData\Local\Programs\Le-Sa";
        private static string currentUserTempFolder = $@"{currentUserFolder}\AppData\Local\Temp";

        private static string allUsersInstallationFolder = $@"{mainDrive}Program Files (x86)\Le-Sa";
        private static string allUsersTempFolder = $@"{mainDrive}Windows\Temp";

        private static readonly RegistryKey AllUserBase = Registry.LocalMachine;
        private static readonly RegistryKey CurrentUserBase = Registry.CurrentUser;

        private static readonly string ApplicationName = "Le-Sa";
        private static readonly string Version = "1.0.0";
        private static readonly string publisher = "Sathsara Bandara Jayasundara";
        private static readonly int estimatedSize = 34;
         private static readonly string uninstallerRegKeys = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\";

        private const string compressedFileLink = "https://firebasestorage.googleapis.com/v0/b/le-sa-f718d.appspot.com/o/file.zip?alt=media&token=822ac61c-3068-4a5c-b12a-86566a768012";

        private static string[] uninstallStringKeyNames;
        private static string[] uninstallStringKeyValues;
        private static int registryKeyCount = 0;

        public formInstaller()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

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

        private void formInstaller_Load(object sender, EventArgs e)
        {
            string sPath = "WinNT://" + Environment.MachineName + ",computer";
            using (var computerEntry = new DirectoryEntry(sPath))
            {
                foreach (DirectoryEntry childEntry in computerEntry.Children)
                {
                    if (childEntry.SchemaClassName == "User" && childEntry.Name == userName())
                    {
                        object obGroups = childEntry.Invoke("Groups");
                        foreach (object localGroup in (System.Collections.IEnumerable)obGroups)
                        {
                            DirectoryEntry lGroupEntry = new DirectoryEntry(localGroup);
                            if (lGroupEntry.Name == "Administrators")
                            {
                                IsAdminAccount = true;
                                break;
                            }
                            lGroupEntry.Close();
                        }

                    }
                }
            }

            if (IsAdminAccount)
            {
                lblWarning.Text = "";
            }
            else
            {
                lblWarning.Text = $"This ({userName()}) is a Standard Windows User Account. Please use the Administrator Account to use this application smoothly.";
            }

            lblInstallationFolder.Text = currentUserInstallationFolder;
            rBtnCurrentUser.Text = $"Only for me ({userName()})";
            progress.Minimum = 0;
            progress.Maximum = 100;
        }

        #region TitleBar
        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.close_27px;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.close_12px;
        }

        private void btnMinimize_MouseHover(object sender, EventArgs e)
        {
            btnMinimize.BackgroundImage = Properties.Resources.minimize_27px;
        }

        private void btnMinimize_MouseLeave(object sender, EventArgs e)
        {
            btnMinimize.BackgroundImage = Properties.Resources.minimize_15px;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        private static string userName()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            return (collection.Cast<ManagementBaseObject>().First()["UserName"].ToString().Split('\\')[1]);
        }

        private static string installationLocation;

        private void btnInstall_Click(object sender, EventArgs e)
        {
            string[] uninstallStringKeyNames = { "Comments", "DisplayIcon", "DisplayName", "DisplayVersion", "InstallDate", "InstallLocation", "Publisher", "UninstallString" };
            string[] uninstallStringKeyValues = { $"Le-Sa {Version}", installationLocation, ApplicationName, Version, currentDate, installationLocation, publisher, $"\"{installationLocation}\\Le-Sa Installer.exe\" --uninstall"};

            if (!IsConnectedToInternet())
            {
                MessageBox.Show("You are not connected to Internet");
            }
            //else if (rBtnAllUsers.Checked)
            //{
            //    MessageBox.Show("Do you want to install application for all users.\nSome feature still not support to Standerd User Account");
            //}
            else if (rBtnCurrentUser.Checked)
            {
                uninstallStringKeyValues[1] = $@"{currentUserInstallationFolder}\Le-Sa Installer.exe";
                uninstallStringKeyValues[5] = currentUserInstallationFolder;
                uninstallStringKeyValues[7] = $"\"{currentUserInstallationFolder}\\Le-Sa Installer.exe\" --uninstall";
                foreach (string keyName in uninstallStringKeyNames)
                {
                    ReadWriteRegistry.WriteRegistry(Registry.LocalMachine, uninstallerRegKeys + ApplicationName, keyName, uninstallStringKeyValues[registryKeyCount], RegistryValueKind.String);
                    registryKeyCount++;
                }
                registryKeyCount = 0;
                ReadWriteRegistry.WriteRegistry(Registry.LocalMachine, uninstallerRegKeys + ApplicationName, "EstimatedSize", estimatedSize, RegistryValueKind.DWord);
            }
            Close();

        }

        #region Current User Installation
        private static void forCurrentUserInstallation()
        {

        }
        #endregion

        //#region All User Installation
        //private static void forAllUserInstallation()
        //{
        //    foreach (string keyName in uninstallStringKeyNames)
        //    {
        //        ReadWriteRegistry.WriteRegistry(Registry.LocalMachine, uninstallerKeys + ApplicationName, keyName, uninstallStringKeyValues[registryKeyCount] ,RegistryValueKind.String);
        //        registryKeyCount ++;
        //    }
        //    registryKeyCount = 0;
        //}
        //#endregion

        #region Internet Connectivity
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }
        #endregion

        #region Download Application Files
        private void startDownload()
        {
            try
            {
                Thread downloadThread = new Thread(() => {
                    WebClient client = new WebClient();
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    client.DownloadFileAsync(new Uri(compressedFileLink), currentUserTempFolder + @"\file.zip");
                });
                downloadThread.Start();
            }
            catch(Exception downloadError)
            {
                MessageBox.Show(downloadError.Message);
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                lblStatus.Text = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive;
                progress.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                lblStatus.Text = "Completed";
            });
        }
        #endregion
    }
}