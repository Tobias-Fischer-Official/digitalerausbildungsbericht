using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using System.Diagnostics;
using System.Net;
using Digitaler_Ausbildungsbericht.Net.Properties;
using System.Threading;
using Digitaler_Ausbildungsbericht.Net.Tools;


namespace Digitaler_Ausbildungsbericht.Net
{
    public partial class frmMain : Office2007RibbonForm
    {
        #region vars

        private Dictionary<string, string> _updateConfig = new Dictionary<string, string>();
        private Dictionary<string, string> _baseData = new Dictionary<string, string>();
        private Dictionary<string, string> _settings = new Dictionary<string, string>();
        private Dictionary<string, string> _blocks = new Dictionary<string, string>();

        private Report _actReport;

        private frmAbout About = new frmAbout();

        private string _hwid = string.Empty;
        private object ReportElement = null;

        internal ExtendedCalendar daypicker;

        #endregion

        #region ctor & frmClosed
        public frmMain()
        {
            InitializeComponent();
            FBLikeBrowser.DocumentText = Resources.FBLike;
            DonateBrowser.DocumentText = Resources.donate;
            try
            {
                News.Navigate(DataManager.baseWebAddress + DataManager.WebNewsPath + "/news.php");
            }
            catch (Exception)
            {
                News.Visible = false;
            }
            StartProgram();
        }

        

        void StartProgram()
        {
            try
            {
                Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
                AddDayPicker();
                LoadBlocks();

                lblLocalVersionShortCut.Text = "Deine Version: " + Application.ProductVersion;

                //Updater herunterladen
                if (File.Exists(DataManager.GetWorkingDirectory() + "\\Updater.exe"))
                    File.Delete(DataManager.GetWorkingDirectory() + "\\Updater.exe");

                if (MiscTools.CheckFilesystem())
                    InternalSetup.setupAssist(0, ref _settings);

                _settings = DataManager.LoadSettings();

                InternalSetup.setupAssist(int.Parse(_settings["lastUndoneSetupStep"]), ref _settings);

                foreach (RatingItem ri in icRatings.SubItems)
                    if (_settings.ContainsKey(ri.Name))
                        ri.RatingValue = int.Parse(_settings[ri.Name]);

                if (!_settings.ContainsKey("FileNames"))
                    _settings.Add("FileNames", "Bericht {AW} von {WS} - {WE}");
                DataManager.SaveSettings(_settings);

                cbxAutoUpdate.Checked = _settings["AutoUpdate"] == "1" ? true : false;

                loadBaseData();

                if (_settings["ReportType"] == "dayly" || _settings["ReportType"] == "dayly+sa")
                {
                    _actReport = DataManager.GetReportOfDate(DateTime.Now);
                    if (_actReport == null)
                        _actReport = new Report(null, DateTime.Now.ToShortDateString(), "", "", "", "", "", "", "0", null, null);

                    DaylyReport dr = new DaylyReport(daypicker);
                    btnSave.Click += new System.EventHandler(dr.SaveAsPDF);
                    btnSpellcheck.Click += new EventHandler(dr.SetSpellCheckEnabled);
                    dr.Dock = DockStyle.Fill;

                    panelReport.Controls.Add(dr);
                    dr.fillReportedDays();

                    ReportElement = dr;
                }
                else if (_settings["ReportType"] == "weekly")
                {
                    _actReport = DataManager.GetReportOfWeek(DateTimeTools.GetActAppWeek(DateTime.Parse(DataManager.LoadBaseData()["beginn"]), DateTime.Now));
                    if (_actReport == null)
                        _actReport = new Report(null, DateTimeTools.GetWeekOfYear(DateTime.Now).ToString(), DateTimeTools.GetWeekOfYear(DateTime.Now).ToString(), DateTime.Now.Year.ToString(), "", "", "", "");

                    WeeklyReport wr = new WeeklyReport(_actReport, daypicker, _baseData);
                    btnSave.Click += new System.EventHandler(wr.SaveAsPDF);
                    btnSpellcheck.Click += new EventHandler(wr.SetSpellCheckEnabled);
                    wr.Dock = DockStyle.Fill;

                    panelReport.Controls.Add(wr);
                    wr.fillReportedDays();

                    ReportElement = wr;
                }
                else
                    InternalSetup.setupAssist(2, ref _settings);

                fillGUI(daypicker.SelectedDate);

                addEvents();

                Thread start = new Thread(SendStats.SendStart);
                start.Start();
                Thread checkv = new Thread(checkVersion);
                checkv.Start();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                Environment.Exit(0);
            }
        }

        private void AddDayPicker()
        {
            daypicker = new ExtendedCalendar();
            daypicker.Width = groupPanel1.Width - 77;
            daypicker.Left = (groupPanel1.Width - daypicker.Width) / 2;
            daypicker.SelectedDate = DateTime.Now;
            groupPanel1.Controls.Add(daypicker);
            groupPanel1.Height = groupPanel1.Height + daypicker.Height - 35;
        }

        void addEvents()
        {
            FormClosing += new FormClosingEventHandler(frmMain_FormClosing);
            FormClosed += new System.Windows.Forms.FormClosedEventHandler(frmMain_FormClosed);
            tsmiExit.Click += new EventHandler(tsmiExit_Click);
            tsmiMaximize.Click += new EventHandler(tsmiMaximize_Click);
            tsmiNewActDayReport.Click += new EventHandler(tsmiNewActDayReport_Click);
            daypicker.onDaySelected += new EventHandler<DaySelectedEventArgs>(daypicker_daychanged);
            riGUI.RatingChanged += new EventHandler(RatingChanged);
            riStability.RatingChanged += new EventHandler(RatingChanged);
            riSupport.RatingChanged += new EventHandler(RatingChanged);
            riUsability.RatingChanged += new EventHandler(RatingChanged);
            riFunctions.RatingChanged += new EventHandler(RatingChanged);
        }

        private void LoadBlocks()
        {
            btnTxtBlocks.SubItems.Clear();

            ButtonItem bi = new ButtonItem("btnEditTxtBlocks", "Textbausteine bearbeiten");
            bi.Click += new EventHandler(btnTxtBlocks_Click);
            bi.Image = Res.Properties.Resources.editTxtBlocks;
            bi.ImageFixedSize = new Size(32, 32);
            btnTxtBlocks.SubItems.Add(bi);

            if (File.Exists(DataManager.GetWorkingDirectory() + "blocks.tbs"))
            {
                _blocks = DataManager.LoadTxtBlocks();
                foreach (KeyValuePair<string, string> kvp in _blocks)
                {
                    ButtonItem b = new ButtonItem(kvp.Key, kvp.Key + " einfügen");
                    b.Click += new EventHandler(b_Click);
                    b.Image = Res.Properties.Resources.textbaustein;
                    b.ImageFixedSize = new System.Drawing.Size(16, 16);
                    btnTxtBlocks.SubItems.Add(b, btnTxtBlocks.SubItems.Count);
                }
            }
            btnTxtBlocks.AutoExpandOnClick = true;
        }

        void b_Click(object sender, EventArgs e)
        {
            ButtonItem b = (sender as ButtonItem);
            string input = _blocks[b.Name];
            if (ReportElement.GetType() == typeof(WeeklyReport))
            {
                WeeklyReport wr = (ReportElement as WeeklyReport);
                wr.ActiveRTB.SelectedRtf = input;
            }
            else if (ReportElement.GetType() == typeof(DaylyReport))
            {
                DaylyReport dr = (ReportElement as DaylyReport);
                dr.ActiveRTB.SelectedRtf = input;
            }
        }

        //In den SendStatsBlock setzen, damit wirklich nur erfasst wird, wenn auch gesendet
        private void RatingChanged(object sender, EventArgs e)
        {
            RatingItem ri = (sender as RatingItem);
            if (_settings.ContainsKey(ri.Name))
                _settings[ri.Name] = ri.Rating.ToString();
            else
                _settings.Add(ri.Name, ri.Rating.ToString());
            DataManager.SaveSettings(_settings);
            switch (ri.Name)
            {
                case "riGUI":
                    SendStats.SendRating("gui", ri.Rating);
                    break;
                case "riStability":
                    SendStats.SendRating("stability", ri.Rating);
                    break;
                case "riSupport":
                    SendStats.SendRating("support", ri.Rating);
                    break;
                case "riUsability":
                    SendStats.SendRating("usability", ri.Rating);
                    break;
                case "riFunctions":
                    SendStats.SendRating("functions", ri.Rating);
                    break;
            }
        }


        void frmMain_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            trayIcon.Visible = false;            
        }
        #endregion

    

        private void loadBaseData()
        {
            _baseData = DataManager.LoadBaseData();
            if (_baseData.Count != 0)
            {
                lblActWeek.Text = DateTimeTools.GetWeekOfYear(DateTime.Now).ToString();
                lblWorkWeek.Text = DateTimeTools.GetActAppWeek(DateTime.Parse(_baseData["beginn"]), DateTime.Now).ToString();
                lblWorkYear.Text = DateTimeTools.GetActAppYear(DateTime.Parse(_baseData["beginn"]), DateTime.Now).ToString();
                lblCompanyName.Text = _baseData["firma"];
                lblInstructor.Text = _baseData["ausbilder"];
                lblNameOfWork.Text = _baseData["beruf"];
                lblWorkSpecialName.Text = _baseData["fachrichtung"];
                lblAppStart.Text = _baseData["beginn"];
                lblAppEnd.Text = _baseData["ende"];
            }
        }

        private void fillGUI(DateTime date)
        {
            if (date.Ticks >= DateTimeTools.GetWorkWeekStartAndEnd(DateTime.Parse(_baseData["beginn"]))[0].Ticks)
            {
                if (_baseData.Count != 0)
                {
                    this.Text = "Digitaler Ausbildungsbericht - " + date.ToShortDateString();

                    lblActWeek.Text = DateTimeTools.GetWeekOfYear(date).ToString();

                    string ausbwoche = string.Empty;
                    DateTime beginnDate = DateTime.Parse(DataManager.LoadBaseData()["beginn"]);
                    DateTime[] dta = DateTimeTools.GetWorkWeekStartAndEnd(date);
                    if (dta[0].Month < beginnDate.Month)
                        ausbwoche = "1";
                    else
                        ausbwoche = DateTimeTools.GetActAppWeek(DateTime.Parse(_baseData["beginn"]), date).ToString();

                    lblWorkWeek.Text = ausbwoche;

                    int year = DateTimeTools.GetActAppYear(DateTime.Parse(_baseData["beginn"]), date);
                    if (year == 0)
                        year = 1;
                    lblWorkYear.Text = year.ToString();

                    panelReport.Enabled = ((_settings["ReportType"] == "dayly" && date.DayOfWeek == DayOfWeek.Saturday) || date.DayOfWeek == DayOfWeek.Sunday) ? false : true;

                    lblWeekStart.Text = DateTimeTools.GetWorkWeekStartAndEnd(date)[0].ToShortDateString();
                    lblWeekEnd.Text = DateTimeTools.GetWorkWeekStartAndEnd(date)[1].ToShortDateString();
                }
                else
                    InternalSetup.setupAssist(1, ref _settings);
            }
            else
                panelReport.Enabled = false;
        }
        #region Einstellungen laden

        private void saveSettings()
        {
            DataManager.SaveSettings(_settings);
        }
        #endregion
        #region Version prüfen
        private void checkVersion()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                wc.DownloadStringAsync(new Uri(DataManager.baseWebAddress + "/update/config.xml"));
            }
            catch (Exception) { }
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (e.Error == null || e.Error.Message != "Der Remotename konnte nicht aufgelöst werden: 'ausbildungsbericht.net'")
                {
                    try
                    {
                        UpdateConfig config = UpdateConfig.Deserialize(e.Result);

                        lblNewVersionShortcut.Text = "Neuste Version: " + config.Version;
                        Version OnlineVersion = Version.Parse(config.Version);
                        Version LocalVersion = Version.Parse(Application.ProductVersion);
                        if (OnlineVersion == LocalVersion)
                        {
                            lblNewVersionShortcut.ForeColor = Color.LightGreen;
                            lblLocalVersionShortCut.ForeColor = Color.LightGreen;
                        }
                        else
                        {
                            lblNewVersionShortcut.ForeColor = Color.Red;
                        }
                        if (OnlineVersion > LocalVersion)
                            btnUpdate.Enabled = true;
                        if (_settings["AutoUpdate"] == "1" && OnlineVersion > LocalVersion)
                        {
                            frmUpdateMessage um = new frmUpdateMessage();
                            um.ShowDialog();
                        }
                    }
                    catch
                    {
                        lblNewVersionShortcut.Text = "Neuste Version: N/A";
                    }
                }
                else
                {
                    lblNewVersionShortcut.Text = "Neuste Version: N/A";
                }
            });
        }
        #endregion
        #region minimieren und maximieren

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.trayIcon.Visible = true;
        }

        private void notifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Show();
            this.trayIcon.Visible = false;
        }

        #endregion
        #region Menüleiste 'Tools&Settings'
        private void btnAbout_Click(object sender, EventArgs e)
        {
            About.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmUpdateMessage um = new frmUpdateMessage();
            um.ShowDialog();
        }

        #endregion
        #region Hauptmenü

        private void btnShowPDFFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(_settings["PDFDir"]))
                Process.Start("explorer.exe", _settings["PDFDir"]);
        }

        private void btnDeleteDatabase_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Hierdurch werden ALLE Berichte in der Datenbank gelöscht!", "Sicher?", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {

            }
            else if (dr == DialogResult.Cancel)
                MessageBox.Show("Aktion abgebrochen");
        }

        private void btnCloseProgramm_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion
        #region TrayIcon
        private void tsmiNewActDayReport_Click(object sender, EventArgs e)
        {
            this.Show();
            this.trayIcon.Visible = false;
            daypicker.SelectedDate = DateTime.Now;
        }

        private void tsmiMaximize_Click(object sender, EventArgs e)
        {
            this.Show();
            this.trayIcon.Visible = false;
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.trayIcon.Visible = false;
            Environment.Exit(0);
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            About.ShowDialog();
        }
        #endregion
        #region Kalender
        private void daypicker_daychanged(object sender, DaySelectedEventArgs e)
        {
            fillGUI(e.SelectedDate);
        }
        #endregion

        private void btnSaveAsPDF_Click(object sender, EventArgs e)
        {
            TestSaveDir();
        }

        internal bool TestSaveDir()
        {
            try
            {
                if (!Directory.Exists(_settings["PDFDir"]))
                {
                    MessageBox.Show(_settings["PDFDir"]);
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.ShowNewFolderButton = true;
                    fbd.Description = "Der Ordner für die PDFs konnte nicht gefunden werden, wählen Sie bitte einen gültigen Ordner!";
                    if (fbd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        _settings["PDFDir"] = fbd.SelectedPath;
                    DataManager.SaveSettings(_settings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        private void btnWebsite_Click(object sender, EventArgs e)
        {
            Process.Start("http://ausbildungsbericht.net");
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            DataManager.ExtractAndOpenManual();
        }

        private void btnAbout_Click_1(object sender, EventArgs e)
        {
            frmAbout About = new frmAbout();
            About.ShowDialog();
        }

        void cbxAutoUpdate_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            _settings["AutoUpdate"] = (sender as CheckBoxItem).Checked ? "1" : "0";
            saveSettings();
        }

        void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!checkLeaveEditor())
                e.Cancel = true;
        }

        private bool checkLeaveEditor()
        {
            string caption = "Sie haben den aktuellen Bericht geändert, aber die Änderungen noch nicht gespeichert!";
            string title = "Programm wirklich beenden?";

            if (ReportElement.GetType() == typeof(WeeklyReport))
                if ((ReportElement as WeeklyReport).Changed)
                    return MessageBox.Show(caption, title, MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK;
                else
                    return true;
            else if (ReportElement.GetType() == typeof(DaylyReport))
                if ((ReportElement as DaylyReport).Changed)
                    return MessageBox.Show(caption, title, MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK;
                else
                    return true;
            return true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (checkLeaveEditor())
                Environment.Exit(0);
        }

        private void btnShowPDFFolder_Click_1(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", _settings["PDFDir"]);
        }

        private void btnTxtBlocks_Click(object sender, EventArgs e)
        {
            frmEditTxtBlocks edit = new frmEditTxtBlocks();
            if (edit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                LoadBlocks();
        }

        private void btnSpellcheck_Click(object sender, EventArgs e)
        {
            if (btnSpellcheck.Text.Contains("[Ein]"))
                btnSpellcheck.Text = btnSpellcheck.Text.Replace("[Ein]", "[Aus]");
            else if (btnSpellcheck.Text.Contains("[Aus]"))
                btnSpellcheck.Text = btnSpellcheck.Text.Replace("[Aus]", "[Ein]");
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            frmFeedback f = new frmFeedback();
            f.ShowDialog();
        }

        private void btnSetBaseData_Click(object sender, EventArgs e)
        {
            Office2007Form editBaseData = new Office2007Form();
            editBaseData.EnableGlass = false;
            editBaseData.Icon = Resources.iconLarge;
            editBaseData.Text = "Stammdaten bearbeiten";
            editBaseData.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            editBaseData.ControlBox = false;
            editBaseData.AutoSize = true;
            editBaseData.Controls.Add(new EditorBaseData());

            if (editBaseData.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                loadBaseData();

            this.BringToFront();
        }

        private void btnSetSettings_Click(object sender, EventArgs e)
        {
            Office2007Form editBaseData = new Office2007Form();
            editBaseData.EnableGlass = false;
            editBaseData.Icon = Resources.iconLarge;
            editBaseData.Text = "Einstellungen bearbeiten";
            editBaseData.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            editBaseData.ControlBox = false;
            editBaseData.AutoSize = true;
            editBaseData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            EditorSettings edit = new EditorSettings();
            edit.cbxSelectReporttype.Enabled = false;
            editBaseData.Controls.Add(edit);

            if (editBaseData.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                _settings = DataManager.LoadSettings();
            this.BringToFront();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Achtung, dieser Vorgang löscht ALLE(!) Berichte und Ihre gespeicherten Daten,\r\nund kann nicht rückgängig gemacht werden,\r\nsind Sie sicher?", "Wirklich Alles löschen?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Digitaler Ausbildungsbericht.Net";
                foreach (string directory in Directory.GetDirectories(dir))
                {
                    foreach (string file in Directory.GetFiles(directory))
                    {
                        FileInfo fi = new FileInfo(file);
                        fi.Delete();
                    }
                    Directory.Delete(directory);
                }
                
                Process.Start(DataManager.GetAppInstallFolder() + "Digitaler Ausbildungsbericht.Net.exe");
                Environment.Exit(0);
            }
        }

        private void News_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (!News.DocumentText.Contains("Die Navigation zu der Webseite wurde abgebrochen."))
            {
                News.Visible = true;
                FBLikeBrowser.Visible = true;
            }
        }

        private void btnImportExport_Click(object sender, EventArgs e)
        {
            Form imexp = new frmExport();
            imexp.ShowDialog();
        }

        private void DonateBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            DonateBrowser.Visible = true;
        }

        private void btnCloudSynch_Click(object sender, EventArgs e)
        {
            frmCloudLogin cloudLogin = new frmCloudLogin();
            cloudLogin.Show();
        }
    }
}