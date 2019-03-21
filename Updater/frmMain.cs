using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace Updater
{
    internal partial class frmMain : Form
    {
        internal frmMain()
        {
            InitializeComponent();
            closeDA();
        }

        public void closeDA()
        {
            List<Process> processes = new List<Process>();
            processes.AddRange(Process.GetProcesses());
            processes.ForEach(process => {
                if (process.ProcessName.ToLower().Contains("ausbildungsbericht"))
                    process.Kill();
            });
        }        

        public frmMain(string[] args)
        {
            InitializeComponent();
            closeDA();
            baseConfig = new Dictionary<string, string>();
            foreach (string arg in args)
                baseConfig.Add(arg.Split('=')[0], arg.Split('=')[1]);
            baseURL = baseConfig["baseURL"];
            path2ConfigFile = baseConfig["path2ConfigFile"];           
        }

        UpdateConfig UpdateConfig;
        string baseURL = "http://ausbildungsbericht.net/";
        string path2ConfigFile = "update/config.xml";
        List<UpdateFile> filesToDownload;
        int filesCompleted = 0;
        bool completed = false;
        private Dictionary<string, string> baseConfig;

        private void CheckOnlineVersion()
        {
            lblActTask.Text = "Verbinde zu Updateserver...";
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                wc.DownloadStringAsync(new Uri(baseURL + path2ConfigFile));
            }
            catch { 
                //Keine Verbindung zum Internet oder Website down
            }
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            lblActTask.Text = "Prüfe auf neue Version...";
            UpdateConfig = UpdateConfig.Deserialize(e.Result);
            DownloadFiles();
        }

        private void DownloadFiles()
        {
            lblActTask.Text = "Lade Files für neue Version...";
            filesToDownload = UpdateConfig.Files;
            DoGuiChanges(filesToDownload);
            foreach (UpdateFile file in filesToDownload)
            {
                string filePath = string.Empty;
                if (file.Folder == "User")
                    filePath = GetPath() + file.FileName + ".tmp";
                else if (file.Folder == "App")
                    filePath = GetAppPath() + file.FileName + ".tmp";
                else
                    return;
                if (!File.Exists(filePath.Substring(0, filePath.Length - 4)) || file.Overwrite)
                {
                    WebClient wc = new WebClient();
                    string uri = baseURL + "update/files/" + UpdateConfig.Version + "/" + file.FileName;
                    wc.DownloadFileAsync(new Uri(uri), filePath, file.FileName);
                    wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloader_DownloadProgressChanged);
                    wc.DownloadFileCompleted += new AsyncCompletedEventHandler(downloader_DownloadFileCompleted);
                }
            }
        }

        void downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            string name = (e.UserState as string);
            (panel1.Controls["pb" + name] as ProgressBar).Value = e.ProgressPercentage;
            (panel1.Controls["stat" + name] as Label).Text = "[Datei:" + e.UserState.ToString() + "]" + e.BytesReceived.ToString() + " Bytes von " + e.TotalBytesToReceive.ToString() + " Bytes heruntergeladen.";
        }

        public static string GetPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Digitaler Ausbildungsbericht.Net\\";
        }

        public static string GetAppPath()
        {
            if (Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) != string.Empty)
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Digital-Reports\\Digitaler Ausbildungsbericht.Net\\";
            else
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Digital-Reports\\Digitaler Ausbildungsbericht.Net\\";
        }

        private void DoGuiChanges(List<UpdateFile> files)
        {
            foreach (UpdateFile file in files)
            {
                ProgressBar progressBar = new ProgressBar();
                progressBar.Step = 1;
                progressBar.Dock = DockStyle.Top;
                progressBar.Height = 5;
                progressBar.Name = "pb" + file.FileName;
                Label status = new Label();
                status.Dock = DockStyle.Top;
                status.Name = "stat" + file.FileName;
                panel1.Controls.Add(status);
                panel1.Controls.Add(progressBar);
            }
        }

        void downloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            filesCompleted++;
            if (filesCompleted == filesToDownload.Count)
            {             
                lblActTask.Text = "Überschreibe alte Dateien...";
                try
                {
                    foreach (UpdateFile file in filesToDownload)
                    {
                        string filePath = string.Empty;
                        if (file.Folder == "User")
                            filePath = GetPath() + file.FileName + ".tmp";
                        else if (file.Folder == "App")
                            filePath = GetAppPath() + file.FileName + ".tmp";
                        if (File.Exists(filePath))
                        {
                            if (filePath.Contains(".exe"))
                            {                              
                                if (File.Exists(GetAppPath() + "Digitaler Ausbildungsbericht.Net.exe"))
                                    File.Delete(GetAppPath() + "Digitaler Ausbildungsbericht.Net.exe");
                                File.Copy(filePath, GetAppPath() + "Digitaler Ausbildungsbericht.Net.exe");
                            }
                            else
                            {
                                if (File.Exists(filePath.Replace(".tmp", string.Empty)))
                                    File.Delete(filePath.Replace(".tmp", string.Empty));
                                File.Copy(filePath, filePath.Replace(".tmp", string.Empty));
                            }
                            File.Delete(filePath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + Environment.NewLine + "Fehler beim Kopieren der neuen Dateien,\r\nFür weitere Infos bitte unsere Website besuchen (http://www.ausbildungsbericht.net)", "Kritischer Updatefehler");
                }
                lblActTask.Text = "Fertig";
                completed = true;
                Process.Start(GetAppPath() + "Digitaler Ausbildungsbericht.Net.exe");
                Environment.Exit(0);
            }
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            if (!completed)
            {
                MessageBox.Show("Update abgebrochen!");
                Environment.Exit(0);
            }
            else if (completed)
            {
                Process.Start("Digitaler Ausbildungsbericht.Net.exe");
                Environment.Exit(0);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckOnlineVersion();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.ShowInTaskbar = false;
            this.notifyIcon1.Visible = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
            this.notifyIcon1.Visible = false;
        }
    }
}
