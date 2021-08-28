using Le_Sa_Installer.Models.AdminCheck;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Le_Sa_Installer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Contains("--uninstall") || args.Contains("-u"))
            {
                Application.Run(new formAskForUninstall());
            }
            else if (args.Contains("--finish") || args.Contains("-f"))
            {
                Application.Run(new formFinish());
            }
            else
            {
                if (AdminCheck.IsAdmin())
                {
                    Application.Run(new formInstaller());
                }
                else
                {
                    AdminCheck.RestartUnderAdmin();
                }
            }
        }
    }
}