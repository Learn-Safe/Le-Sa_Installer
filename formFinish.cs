using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Le_Sa_Installer
{
    public partial class formFinish : Form
    {
        public formFinish()
        {
            InitializeComponent();
        }

        private void formFinish_Load(object sender, EventArgs e)
        {
            SelfDestruct();
        }

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
            catch (Exception)
            {
                Close();
            }
            finally
            {
                Close();
            }
        }
    }
}
