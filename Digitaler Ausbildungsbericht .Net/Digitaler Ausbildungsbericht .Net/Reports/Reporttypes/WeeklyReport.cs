using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

namespace Digitaler_Ausbildungsbericht.Net
{
    internal partial class WeeklyReport : UserControl
    {
        internal WeeklyReport()
        {
            InitializeComponent();
        }

        private Report _actWeekReport;
        private ExtendedCalendar _daypicker;
        private Dictionary<string, string> _baseData;
        private bool _changed = false;
        public bool Changed
        {
            get { return _changed; }
            set { _changed = value; }
        }
        private bool _loading = false;
        private RichTextBox _ActiveRTB;
        public RichTextBox ActiveRTB
        {
            get { return _ActiveRTB; }
            set { _ActiveRTB = value; }
        }
        NHunspellExtender.NHunspellTextBoxExtender ext;

        internal WeeklyReport(Report actWeekReport, ExtendedCalendar DayPicker, Dictionary<string, string> BaseData)
        {
            InitializeComponent();
            _daypicker = DayPicker;
            _actWeekReport = actWeekReport;
            _daypicker.onDaySelected += new EventHandler<DaySelectedEventArgs>(_daypicker_DaySelected);
            _baseData = BaseData;
            _ActiveRTB = tbxCompanyWork;
            tabControl1.TabIndexChanged += new EventHandler(tabControl1_TabIndexChanged);
            tbxCompanyWork.KeyPress += new KeyPressEventHandler(tbx_KeyPress);
            tbxSchoolWork.KeyPress += new KeyPressEventHandler(tbx_KeyPress);
            tbxTalkWork.KeyPress += new KeyPressEventHandler(tbx_KeyPress);
            try
            {
                ext = new NHunspellExtender.NHunspellTextBoxExtender();
                ext.UpdateLanguageFiles("Deutsch", DataManager.GetWorkingDirectory() + "SpellCheck\\de_DE.aff", DataManager.GetWorkingDirectory() + "SpellCheck\\de_DE.dic", false, false);
                ext.EnableTextBoxBase(new TextBoxBase[] { tbxCompanyWork, tbxSchoolWork, tbxTalkWork });
                ext.SpellAsYouType = false;
            }
            catch { }
            _daypicker.SelectedDate = DateTime.Now;
            _actWeekReport = DataManager.GetReportOfWeek(DateTimeTools.GetActAppWeek(DateTime.Parse(DataManager.LoadBaseData()["beginn"]), DateTime.Now));
            if (_actWeekReport == null)
                _actWeekReport = new Report(null, DateTimeTools.GetActAppWeek(DateTime.Parse(_baseData["beginn"]), DateTime.Now).ToString(), DateTimeTools.GetWeekOfYear(DateTime.Now).ToString(), DateTime.Now.Year.ToString(), "", "", "", "0");
            fillReport();
        }

        private bool checkLeaveEditor()
        {
            string caption = "Sie haben den aktuellen Bericht geändert, aber die Änderungen noch nicht gespeichert!";
            string title = "Achtung!";
            if (this.Changed)
                return MessageBox.Show(caption, title, MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK;
            else
                return true;
        }

        internal void SetSpellCheckEnabled(object sender, EventArgs e)
        {
            if (!ext.SpellAsYouType)
                ext.SpellAsYouType = true;
            else
                ext.SpellAsYouType = false;
        }

        void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Name)
            {
                case "tpCompany":
                    _ActiveRTB = tbxCompanyWork;
                    break;
                case "tpTalk":
                    _ActiveRTB = tbxTalkWork;
                    break;
                case "tpSchool":
                    _ActiveRTB = tbxSchoolWork;
                    break;
            }
        }

        internal void tbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as RichTextBox).Lines.Length >= 11 && e.KeyChar == '\r')
                e.Handled = true;
            else
                _changed = true;
        }

        internal void _daypicker_DaySelected(object sender, DaySelectedEventArgs e)
        {
            //Prüfen ob der aktuelle Text geändert wurde
            if (checkLeaveEditor())
            {
                gpEdit.Enabled = true;
                if (e.SelectedDate < DateTime.Parse(DataManager.LoadBaseData()["ende"]) && int.Parse(_actWeekReport.YearWeekNumber) != DateTimeTools.GetWeekOfYear(e.SelectedDate))
                {
                    _actWeekReport = DataManager.GetReportOfWeek(DateTimeTools.GetActAppWeek(DateTime.Parse(DataManager.LoadBaseData()["beginn"]), e.SelectedDate));
                    if (_actWeekReport == null)
                        _actWeekReport = new Report(null, DateTimeTools.GetActAppWeek(DateTime.Parse(_baseData["beginn"]), e.SelectedDate).ToString(), DateTimeTools.GetWeekOfYear(e.SelectedDate).ToString(), e.SelectedDate.Year.ToString(), "", "", "", "0");
                    fillReport();
                    cbxDelivered.Checked = _actWeekReport.Delivered == "1" ? true : false;
                }
                _changed = false;
            }
        }

        internal void fillReport()
        {
            _loading = true;
            try { tbxCompanyWork.Rtf = _actWeekReport.CompanyText; }
            catch { tbxCompanyWork.Text = _actWeekReport.CompanyText; }

            try { tbxSchoolWork.Rtf = _actWeekReport.SchoolText; }
            catch { tbxSchoolWork.Text = _actWeekReport.SchoolText; }

            try { tbxTalkWork.Rtf = _actWeekReport.InstructionsText; }
            catch { tbxTalkWork.Text = _actWeekReport.InstructionsText; }
            _loading = false;
        }

        internal void SaveAsPDF(object sender, EventArgs e)
        {
            if (SaveReport())
            {
                string file = PDF.SaveWeeklyReport(_actWeekReport.AppWeekNumber, _actWeekReport.YearWeekNumber, _actWeekReport.Year, _baseData["nachname"] + ", " + _baseData["vorname"], _baseData["beruf"], _baseData["fachrichtung"], _actWeekReport, _baseData["firma"], _baseData["abteilung"], _baseData["ausbilder"], DataManager.LoadSettings()["PDFDir"]);
                if (file != string.Empty && MessageBox.Show("Bericht erfolgreich gespeichert, jetzt öffnen?", "Erfolg", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try { Process.Start(file); }
                    catch { MessageBox.Show("Konnte Datei nicht erfolgreich öffnen, ist Adobe Reader installiert?", "Fehler beim Öffnen der Datei"); }
                }
            }
        }

        private void btnSaveReport_Click(object sender, EventArgs e)
        {
            SaveReport();
        }

        private bool SaveReport()
        {
            if ((tbxTalkWork.Text != string.Empty && tbxSchoolWork.Text != string.Empty && tbxCompanyWork.Text != string.Empty) || MessageBox.Show("Du hast mindestens einen Teil des Berichtes nicht ausgefüllt, willst du wirklich den Bericht speichern?", "Sicher?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DataManager.CreateWeekReport(_actWeekReport);
                lblSaved.Visible = true;
                System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
                t.Interval = 5000;
                t.Tick += new EventHandler(t_Tick);
                t.Start();
                fillReportedDays();
                _changed = false;
                resetHeaders();
                return true;
            }
            else
                return false;
        }

        internal void fillReportedDays()
        {
            ReportedWeek[] rws = DataManager.getAllReportedWeeks();
            foreach (ReportedWeek rw in rws)
            {
                DateTime[] WeekDaysReported = DateTimeTools.getWeekAsDays(rw.Week, rw.Year);
                foreach (DateTime day in WeekDaysReported)
                {
                    if (!_daypicker.MarkedDates.ContainsKey(day))
                        _daypicker.MarkedDates.Add(day, rw.Delivered ? Color.Blue : Color.Green);
                    else
                        _daypicker.MarkedDates[day] = rw.Delivered ? Color.Blue : Color.Green;
                }
            }
            _daypicker.ColorDates();
        }

        private void btnClearReport_Click(object sender, EventArgs e)
        {
            resetFields();
        }

        private void resetFields()
        {
            cbxDelivered.Checked = false;
            tbxCompanyWork.ResetText();
            tbxSchoolWork.ResetText();
            tbxTalkWork.ResetText();
            resetHeaders();
        }

        void resetHeaders()
        {
            tpSchool.Text = "Unterricht in der Berufsschule";
            tpTalk.Text = "Unterweisungen/Lehrgespräche";
            tpCompany.Text = "Betriebliche Tätigkeiten";
        }

        private void cbxDelivered_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loading)            
                _changed = true;            
            _actWeekReport.Delivered = cbxDelivered.Checked ? "1" : "0";
        }

        private void tbxSchoolWork_TextChanged(object sender, EventArgs e)
        {
            if (!_loading)
            {
                _changed = true;
                if (!(tpSchool.Text == "Unterricht in der Berufsschule*"))
                    tpSchool.Text = "Unterricht in der Berufsschule*";
            }
            _actWeekReport.SchoolText = tbxSchoolWork.Rtf;
            tbxSchoolWork.Rtf = tbxSchoolWork.Rtf.Replace("fs29", "fs20");
        }

        private void tbxTalkWork_TextChanged(object sender, EventArgs e)
        {
            if (!_loading)
            {
                if (!(tpTalk.Text == "Unterweisungen/Lehrgespräche*"))
                    tpTalk.Text = "Unterweisungen/Lehrgespräche*";
                _changed = true;
            }
            _actWeekReport.InstructionsText = tbxTalkWork.Rtf;
            tbxTalkWork.Rtf = tbxTalkWork.Rtf.Replace("fs29", "fs20");
        }

        private void tbxCompanyWork_TextChanged(object sender, EventArgs e)
        {
            if (!_loading)
            {
                if (!(tpCompany.Text == "Betriebliche Tätigkeiten*"))
                    tpCompany.Text = "Betriebliche Tätigkeiten*";
                _changed = true;
            }
            _actWeekReport.CompanyText = tbxCompanyWork.Rtf;
            tbxCompanyWork.Rtf = tbxCompanyWork.Rtf.Replace("fs29", "fs20");
        }

        internal void t_Tick(object sender, EventArgs e)
        {
            lblSaved.Visible = false;
            (sender as System.Windows.Forms.Timer).Stop();
        }
    }
}
