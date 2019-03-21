using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Net;
using System.Diagnostics;
using System.IO;
using Digitaler_Ausbildungsbericht.Net.Properties;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace Digitaler_Ausbildungsbericht.Net
{
    public partial class frmUpdateMessage : Office2007Form
    {
        //Import the function from user32.dll and define the shield constant (Added in Part 3)
        [DllImport("user32")]
        static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);
        const uint BCM_SETSHIELD = 0x0000160C;

        private string _dlString;

        public frmUpdateMessage()
        {
            InitializeComponent();
            _dlString = DataManager.baseWebAddress + "/update/cl.rtf";
            progressBar1.Style = ProgressBarStyle.Marquee;

            Thread thr = new Thread(LoadChangeLog);
            thr.Start();
            GetAndDisplayRights();
        }

        private void SetUACShields()
        {
            btnDoUpdate.FlatStyle = FlatStyle.System;
            SendMessage(btnDoUpdate.Handle, BCM_SETSHIELD, 0, 1);
        }

        private void GetAndDisplayRights()
        {
            WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!hasAdministrativeRight)
                SetUACShields();
        }

        private void LoadChangeLog()
        {
            Thread.Sleep(1000);
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            wc.DownloadStringAsync(new Uri(_dlString));
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
                this.Invoke((MethodInvoker)delegate
                {
                    rtbChangelog.Rtf = e.Result;
                    panel3.Visible = false;
                });
            else
                MessageBox.Show(e.Error.ToString());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDoUpdate_Click(object sender, EventArgs e)
        {
            DoUpdate();
        }

        private void DoUpdate()
        {
            if (!File.Exists(DataManager.GetWorkingDirectory() + "Updater.exe"))
                File.WriteAllBytes(DataManager.GetWorkingDirectory() + "Updater.exe", Resources.Updater);
            Process updaterProcess = new Process();

            updaterProcess.StartInfo.UseShellExecute = true;
            updaterProcess.StartInfo.WorkingDirectory = DataManager.GetWorkingDirectory();
            updaterProcess.StartInfo.FileName = DataManager.GetWorkingDirectory() + "Updater.exe";
            updaterProcess.StartInfo.Arguments = "baseURL=" + DataManager.baseWebAddress +
                " "
                + "path2ConfigFile=" + DataManager.WebUpdatePath;
            updaterProcess.StartInfo.Verb = "runas";

            updaterProcess.Start();
            
            Environment.Exit(0);
            Environment.Exit(0);
        }
    }
}
