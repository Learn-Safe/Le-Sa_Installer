using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Management;
using System.Linq;
using System.IO;
using System.Threading;
using System.Net;
using System.ComponentModel;
using Le_Sa_Installer.Models.Registry;
using Microsoft.Win32;
using System.DirectoryServices;
using IWshRuntimeLibrary;
using System.IO.Compression;
using System.Text;
using System.Collections.Generic;

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

        private static readonly string ApplicationName = "Le-Sa";
        private static readonly string publisher = "Sathsara Bandara Jayasundara";
        private static readonly int estimatedSize = 34;
        private static readonly string uninstallerRegKeys = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\";

        private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private static readonly string startMenue = $@"{mainDrive}ProgramData\Microsoft\Windows\Start Menu\Programs";
        private static string dataFileURL = "https://gist.githubusercontent.com/sathsarabandaraj/d3dd8501f5ca577e42d2f25aabe1659c/raw/c54510ab01119a56b486e49093e89a183320fa7e/lesaversion&downlink";
        List<string> DownAndVerDat = new List<string>();

        private static string compressedFileLink;
        private static string Version;
        private static string[] uninstallStringKeyNames;
        private static string[] uninstallStringKeyValues;
        private static int registryKeyCount = 0;

        public formInstaller()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }

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

            if (System.IO.File.Exists(currentUserTempFolder + @"\lesadata.txt"))
            {
                System.IO.File.Delete(currentUserTempFolder + @"\lesadata.txt");
            }
            try
            {
                WebClient dataFile = new WebClient();
                dataFile.DownloadFile(dataFileURL, currentUserTempFolder + @"\lesadata.txt");
            }
            catch(Exception downErr)
            {
                MessageBox.Show(downErr.Message, "Error");
            }

            if (System.IO.File.Exists(currentUserTempFolder + @"\lesadata.txt"))
            {
                const Int32 BufferSize = 128;
                using (var fileStream = System.IO.File.OpenRead(currentUserTempFolder + @"\lesadata.txt"))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        MessageBox.Show(line);
                        DownAndVerDat.Add(line);
                    }
                }
                        
            }

            compressedFileLink = DownAndVerDat[0];
            Version = DownAndVerDat[1];

            MessageBox.Show(compressedFileLink);
            MessageBox.Show(Version);

            if (System.IO.File.Exists(currentUserTempFolder + @"\lesadata.txt"))
            {
                System.IO.File.Delete(currentUserTempFolder + @"\lesadata.txt");
            }

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

            #region Download Files
            if (!IsConnectedToInternet())
            {
                MessageBox.Show("You are not connected to Internet");
            }
            else
            {
                Thread downloadApplication = new Thread(new ThreadStart(startDownload));
                downloadApplication.Start();
            }
            #endregion


            //else if (rBtnAllUsers.Checked)
            //{
            //    MessageBox.Show("Do you want to install application for all users.\nSome feature still not support to Standerd User Account");
            //}
            #region Create Registry Keys
            if (rBtnCurrentUser.Checked)
            {
                uninstallStringKeyValues[1] = $@"{currentUserInstallationFolder}\Le-Sa Installer.exe";
                uninstallStringKeyValues[5] = currentUserInstallationFolder;
                uninstallStringKeyValues[7] = $"\"{currentUserInstallationFolder}\\Le-Sa Installer.exe\" --uninstall";
                foreach (string keyName in uninstallStringKeyNames)
                {
                    ReadWriteRegistry.WriteRegistry(Registry.CurrentUser, uninstallerRegKeys + ApplicationName, keyName, uninstallStringKeyValues[registryKeyCount], RegistryValueKind.String);
                    registryKeyCount++;
                }
                registryKeyCount = 0;
                ReadWriteRegistry.WriteRegistry(Registry.CurrentUser, uninstallerRegKeys + ApplicationName, "EstimatedSize", estimatedSize, RegistryValueKind.DWord);
                progress.Value += 25;
            }
            #endregion

            #region Create Shoutcuts
            if (cBoxDesktopShoutcut.Checked)
            {
                if (rBtnCurrentUser.Checked)
                {
                    CreateShortcut(ApplicationName, desktopPath, currentUserInstallationFolder + @"\" + ApplicationName, currentUserInstallationFolder + @"\Le-Sa_256px_ico");
                    CreateShortcut(ApplicationName, startMenue, currentUserInstallationFolder + @"\" + ApplicationName, currentUserInstallationFolder + @"\Le-Sa_256px_ico");
                    progress.Value += 10;
                }

                if (rBtnAllUsers.Checked)
                {
                    CreateShortcut(ApplicationName, desktopPath, allUsersInstallationFolder + @"\" + ApplicationName, allUsersInstallationFolder + @"\Le-Sa_256px_ico");
                    CreateShortcut(ApplicationName, startMenue, allUsersInstallationFolder + @"\" + ApplicationName, allUsersInstallationFolder + @"\Le-Sa_256px_ico");
                    progress.Value += 10;
                }
            }
            #endregion
        }

        #region Shoutcut Creator
        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation, string iconLocation)
        {
            string shortcutLocation = Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = ApplicationName;
            shortcut.IconLocation = iconLocation;        
            shortcut.TargetPath = targetFileLocation;               
            shortcut.Save();                                   
        }
        #endregion

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
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    client.DownloadFileAsync(new Uri(compressedFileLink), currentUserTempFolder + $@"\{ApplicationName}.zip");
                });
                downloadThread.Start();
            }
            catch(Exception downloadError)
            {
                MessageBox.Show(downloadError.Message);
            }
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                lblStatus.Text = "Completed";
                progress.Value += 40;
                if (!Directory.Exists(currentUserInstallationFolder))
                {
                    try
                    {
                        Directory.CreateDirectory(currentUserInstallationFolder);
                    }
                    catch
                    {
                    }
                }
                if (System.IO.File.Exists($@"{currentUserTempFolder}\{ApplicationName}.zip"))
                {
                    try
                    {
                        ZipFile.ExtractToDirectory($@"{currentUserTempFolder}\{ApplicationName}.zip", currentUserInstallationFolder);
                    }
                    catch
                    {

                    }
                }
            });
        }
        #endregion
    }
}