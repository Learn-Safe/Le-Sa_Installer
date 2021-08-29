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
using System.Diagnostics;

namespace Le_Sa_Installer
{
    public partial class formInstaller : Form
    {
        private Point lastPoint;
        private bool IsAdminAccount = false;
        private static readonly string mainDrive = Path.GetPathRoot(Environment.SystemDirectory);
        private static readonly string fileName = String.Concat(Process.GetCurrentProcess().ProcessName, ".exe");
        private static readonly string fileCurrentPath = Path.Combine(Environment.CurrentDirectory, fileName);

        private static readonly string currentDate = DateTime.Now.ToString("yyyyMMdd");

        private static string currentUserFolder = $@"{mainDrive}Users\{userName()}";
        private static string currentUserInstallationFolder = $@"{currentUserFolder}\AppData\Local\Programs\Le-Sa";
        private static string currentUserTempFolder = $@"{currentUserFolder}\AppData\Local\Temp";

        private static string allUsersInstallationFolder = $@"{mainDrive}Program Files (x86)\Le-Sa";

        private static readonly string ApplicationName = "Le-Sa";
        private static readonly string currentUserApplicationPath = $@"{currentUserInstallationFolder}\{ApplicationName}.exe";
        private static readonly string publisher = "Sathsara Bandara Jayasundara";
        private static readonly int estimatedSize = 25750;
        private static readonly string uninstallerRegKeys = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\";
        private static readonly string currentUserIconLock = currentUserInstallationFolder + @"\Le-Sa_256px_ico.ico";

        private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private static readonly string startMenue = $@"{currentUserFolder}\AppData\Roaming\Microsoft\Windows\Start Menu\Programs";
        private static string dataFileURL = "https://gist.githubusercontent.com/sathsarabandaraj/d3dd8501f5ca577e42d2f25aabe1659c/raw";
        List<string> DownAndVerDat = new List<string>();

        private static string compressedFileLink;
        private static string Version;
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
                        DownAndVerDat.Add(line);
                    }
                }
                        
            }

            compressedFileLink = DownAndVerDat[0];
            Version = DownAndVerDat[1];


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

        #region Current User username
        private static string userName()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            return (collection.Cast<ManagementBaseObject>().First()["UserName"].ToString().Split('\\')[1]);
        }
        #endregion

        private static string installationLocation;

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (lblWarning.Text == "Installation Finished")
            {
                Close();
            }
            btnInstall.Enabled = false;
            string[] uninstallStringKeyNames = { "Comments", "DisplayIcon", "DisplayName", "DisplayVersion", "InstallDate", "InstallLocation", "Publisher", "UninstallString" };
            string[] uninstallStringKeyValues = { $"Le-Sa {Version}", installationLocation, ApplicationName, Version, currentDate, installationLocation, publisher, $"\"{installationLocation}\\Le-Sa Installer.exe\" --uninstall"};

            #region Download Files
            if (!IsConnectedToInternet())
            {
                MessageBox.Show("You are not connected to Internet");
            }
            else
            {
                Thread downloadApplication = new Thread(new ThreadStart(startIstallation));
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
            }
            #endregion

            #region Create Shoutcuts
            if (cBoxDesktopShoutcut.Checked)
            {
                if (rBtnCurrentUser.Checked)
                {
                    CreateShortcut(ApplicationName, desktopPath, currentUserApplicationPath, currentUserIconLock);
                }

                //if (rBtnAllUsers.Checked)
                //{
                //    CreateShortcut(ApplicationName, desktopPath, allUsersInstallationFolder + @"\" + ApplicationName, allUsersInstallationFolder + @"\Le-Sa.exe");
                //}
            }
            if (rBtnCurrentUser.Checked)
            {
                CreateShortcut(ApplicationName, startMenue, currentUserApplicationPath, currentUserApplicationPath);
            }
            //if (rBtnAllUsers.Checked)
            //{
            //    CreateShortcut(ApplicationName, startMenue, allUsersInstallationFolder + @"\" + ApplicationName, allUsersInstallationFolder + @"\Le-Sa.exe"); 
            //}
            //    
            #endregion
        }

        #region Finish Installation 
        private void finishInstallation()
        {
            btnInstall.Text = "OK";
            btnInstall.Enabled = true;
            lblWarning.Text = "Installation Finished";
            lblWarning.Font = new Font(lblWarning.Font, FontStyle.Bold);
            lblWarning.Visible = true;
        }
        #endregion

        #region Shoutcut
        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation, string iconLocation)
        {
            string shortcutLocation = Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.WorkingDirectory = currentUserInstallationFolder;
            shortcut.Description = ApplicationName;
            shortcut.IconLocation = iconLocation;        
            shortcut.TargetPath = targetFileLocation;               
            shortcut.Save();                                   
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

        #region Download and Extract Application Files
        private void startIstallation()
        {
            try
            {
                Thread downloadThread = new Thread(() => {
                    WebClient client = new WebClient();
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    client.DownloadFileAsync(new Uri(compressedFileLink), currentUserTempFolder + $@"\{ApplicationName}.zip");
                });
                downloadThread.Start();
            }
            catch(Exception downloadError)
            {
                MessageBox.Show(downloadError.Message + "\nMaby this installer is expired.\nPlease retry download new version.","Error");
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytesToDown = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytesToDown * 100;
                lblStatus.Text = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive;
            });
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                lblStatus.Text = "Completed";

                if (Directory.Exists(currentUserInstallationFolder))
                {
                    Directory.Delete(currentUserInstallationFolder, true);
                }
                if (System.IO.File.Exists($@"{currentUserTempFolder}\{ApplicationName}.zip"))
                {
                    try
                    {
                        ZipFile.ExtractToDirectory($@"{currentUserTempFolder}\{ApplicationName}.zip", currentUserFolder+ @"\AppData\Local\Programs");
                        finishInstallation();
                    }
                    catch(Exception error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }
            });
        }
        #endregion

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
    }
}