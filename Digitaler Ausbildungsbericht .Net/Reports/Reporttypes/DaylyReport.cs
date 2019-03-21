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
    public partial class DaylyReport : UserControl
    {
        internal DaylyReport()
        {
            InitializeComponent();
        }

        private bool _changed = false;
        public bool Changed
        {
            get { return _changed; }
            set { _changed = value; }
        }

        private Report _actDayReport;
        private ExtendedCalendar _daypicker;
        private bool _loadingReport = false;

        private RichTextBox _ActiveRTB;
        public RichTextBox ActiveRTB
        {
            get { return _ActiveRTB; }
            set { _ActiveRTB = value; }
        }

        internal NHunspellExtender.NHunspellTextBoxExtender ext;

        internal DaylyReport(ExtendedCalendar DayPicker)
        {
            InitializeComponent();
            _ActiveRTB = tbxReport;
            _daypicker = DayPicker;
            _daypicker.onDaySelected += new EventHandler<DaySelectedEventArgs>(_daypicker_onDaySelected);
            LoadReport(DateTime.Now);
            try
            {
                ext = new NHunspellExtender.NHunspellTextBoxExtender();
                ext.UpdateLanguageFiles("Deutsch", DataManager.GetWorkingDirectory() + "SpellCheck\\de_DE.aff", DataManager.GetWorkingDirectory() + "SpellCheck\\de_DE.dic", false, false);
                ext.EnableTextBoxBase(new TextBoxBase[] { tbxReport });
                ext.SpellAsYouType = false;
            }
            catch { }
        }

        internal void SetSpellCheckEnabled(object sender, EventArgs e)
        {
            if (!ext.SpellAsYouType)
                ext.SpellAsYouType = true;
            else
                ext.SpellAsYouType = false;
        }

        internal void _daypicker_onDaySelected(object sender, DaySelectedEventArgs e)
        {
            if (checkLeaveEditor())
                LoadReport(e.SelectedDate);
        }

        internal void LoadReport(DateTime dt)
        {
            _loadingReport = true;
            gpEdit.Enabled = true;

            _actDayReport = DataManager.GetReportOfDate(dt);
            if (_actDayReport == null)
                _actDayReport = new Report(null, dt.ToShortDateString(), String.Empty, "0", "8", "Work", "0", "0", "0", null, null);

            if (_actDayReport.ReportText == String.Empty)
                tbxReport.ResetText();
            else
                try { tbxReport.Rtf = _actDayReport.ReportText; }
                catch { tbxReport.Text = _actDayReport.ReportText; }

            tbxWorkHours.Text = _actDayReport.WorkHours;
            tbxNotWork.Text = _actDayReport.AwayHours;

            tbxOtherPlace.Text = (_actDayReport.Division != "Work" && _actDayReport.Division != "School") ? _actDayReport.Division : String.Empty;
            tbxOtherPlace.Enabled = (_actDayReport.Division != "Work" && _actDayReport.Division != "School" && _actDayReport.Division != string.Empty) ? true : false;
            cbxOtherPlace.Checked = _actDayReport.Division != "School" && _actDayReport.Division != "Work" ? true : false;

            cbxSchool.Checked = _actDayReport.Division == "School" ? true : false;
            cbxCompany.Checked = _actDayReport.Division == "Work" ? true : false;

            cbxDelivered.Checked = _actDayReport.Delivered != "1" ? false : true;
            cbxHolidays.Checked = _actDayReport.Holyday != "1" ? false : true;
            cbxIll.Checked = _actDayReport.Ill != "1" ? false : true;


            tbxFirstCommentLine.Text = _actDayReport.Comments != null ? _actDayReport.Comments[0] : "";
            tbxSecondCommentLine.Text = _actDayReport.Comments != null ? _actDayReport.Comments[1] : "";

            nudHour1.Value = decimal.Parse(_actDayReport.SingleHours != null ? _actDayReport.SingleHours[0] : "0");
            nudHour2.Value = decimal.Parse(_actDayReport.SingleHours != null ? _actDayReport.SingleHours[1] : "0");
            nudHour3.Value = decimal.Parse(_actDayReport.SingleHours != null ? _actDayReport.SingleHours[2] : "0");
            nudHour4.Value = decimal.Parse(_actDayReport.SingleHours != null ? _actDayReport.SingleHours[3] : "0");
            nudHour5.Value = decimal.Parse(_actDayReport.SingleHours != null ? _actDayReport.SingleHours[4] : "0");
            nudHour6.Value = decimal.Parse(_actDayReport.SingleHours != null ? _actDayReport.SingleHours[5] : "0");
            nudHour7.Value = decimal.Parse(_actDayReport.SingleHours != null ? _actDayReport.SingleHours[6] : "0");


            _loadingReport = false;
            _changed = false;
        }

        internal void fillReportedDays()
        {
            ReportedDay[] rws = DataManager.GetAllReportedDays();
            foreach (ReportedDay rd in rws)
            {
                if (!_daypicker.MarkedDates.ContainsKey(rd.Date))
                    _daypicker.MarkedDates.Add(rd.Date, rd.Delivered ? Color.Blue : Color.Green);
                else
                    _daypicker.MarkedDates[rd.Date] = rd.Delivered ? Color.Blue : Color.Green;
            }
            _daypicker.ColorDates();
            _daypicker.SelectedDate = DateTime.Parse(_actDayReport.Date);
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

        internal string Save()
        {
            string file = string.Empty;
            if (SaveReport())
            {
                Report[] reports = DataManager.getWeeklyReports(DateTimeTools.GetWorkWeekStartAndEnd(_daypicker.SelectedDate)[0], (DataManager.LoadSettings()["ReportType"] == "dayly") ? false : true);

                Dictionary<string, string> _baseData = DataManager.LoadBaseData();

                if (reports[0] == null || reports[1] == null || reports[2] == null || reports[3] == null || reports[4] == null)
                    MessageBox.Show("Bitte erfassen Sie die Woche erst vollständig bevor Sie sie als PDF Speichern!", "Woche nicht vollständig!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    file = PDF.SaveDaylyReport
                       (
                           DateTimeTools.GetActAppWeek(DateTime.Parse(_baseData["beginn"]), DateTime.Parse(_actDayReport.Date)).ToString(),
                           DateTimeTools.GetWeekOfYear(DateTime.Parse(reports[1].Date)).ToString(),
                           DateTimeTools.GetActAppYear(DateTime.Parse(_baseData["beginn"]), DateTime.Parse(reports[1].Date)).ToString(),
                            _baseData["nachname"] + ", " + _baseData["vorname"], _baseData["beruf"],
                           _baseData["fachrichtung"],
                           reports,
                           _baseData["firma"],
                            DataManager.LoadSettings()["PDFDir"],
                           _baseData["ausbilder"],
                           _baseData["abteilung"]
                       );
                }
            }
            return file;
        }



        internal void SaveAsPDF(object sender, EventArgs e)
        {
            if (SaveReport())
            {
                string file = Save();
                if (file != string.Empty)
                {
                    if (MessageBox.Show("Bericht erfolgreich gespeichert, jetzt öffnen?", "Erfolg", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        try { Process.Start(file); }
                        catch { MessageBox.Show("Konnte Datei nicht erfolgreich öffnen, ist Adobe Reader installiert?", "Fehler beim Öffnen der Datei"); }
                }
                else
                    MessageBox.Show("Bericht nicht gespeichert.");
            }
        }

        private void resetFields()
        {
            tbxNotWork.Value = 0.00M;
            tbxWorkHours.Value = 8.00M;
            tbxReport.ResetText();
            tbxOtherPlace.ResetText();
            cbxDelivered.Checked = false;
            cbxCompany.Checked = true;
            cbxHolidays.Checked = false;
            cbxIll.Checked = false;
            cbxOtherPlace.Checked = false;
            gpReportText.Text = "Tätigkeitsbericht";
            gpTimes.Text = "Ausbildungszeiten";
            gpLocation.Text = "Ausbildungsort";
            tbxOtherPlace.Enabled = false;
            nudHour1.Value = 0;
            nudHour2.Value = 0;
            nudHour3.Value = 0;
            nudHour4.Value = 0;
            nudHour5.Value = 0;
            nudHour6.Value = 0;
            nudHour7.Value = 0;

        }

        internal void t_Tick(object sender, EventArgs e)
        {
            lblSaved.Visible = false;
            (sender as System.Windows.Forms.Timer).Stop();
        }

        private void tbxOtherPlace_TextChanged(object sender, EventArgs e)
        {
            if (tbxOtherPlace.Enabled && !_loadingReport)
            {
                gpLocation.Text = "Ausbildungsort*";
                _changed = true;
            }
        }

        private void tbxWorkHours_ValueChanged(object sender, EventArgs e)
        {
            if (!_loadingReport)
            {
                gpTimes.Text = "Ausbildungszeiten*";
                _changed = true;
            }
        }

        private void tbxNotWork_ValueChanged(object sender, EventArgs e)
        {
            if (!_loadingReport)
            {
                gpTimes.Text = "Ausbildungszeiten*";
                _changed = true;
            }
        }

        private void tbxReport_TextChanged(object sender, EventArgs e)
        {
            if (!_loadingReport)
            {
                gpReportText.Text = "Ausbildungsbericht*";
                _changed = true;
            }
            tbxReport.Rtf = tbxReport.Rtf.Replace("fs29", "fs20");
        }

        #region Buttons
        private void btnSaveReport_Click(object sender, EventArgs e) { SaveReport(); }

        private bool SaveReport()
        {
            if (_actDayReport == null)
            {
                _actDayReport = new Report(null, "", "", "", "", "", "", "", "", null, null);
                return false;
            }
            else if (tbxReport.Text == string.Empty)
            {
                MessageBox.Show("Bitte geben Sie einen Bericht ein bevor Sie ihn speichern!");
                return false;
            }
            _changed = false;

            gpReportText.Text = "Tätigkeitsbericht";
            gpTimes.Text = "Ausbildungszeiten";
            gpLocation.Text = "Ausbildungsort";

            _actDayReport.Date = _daypicker.SelectedDate.ToShortDateString();
            DateTime rpDate = DateTime.Parse(_actDayReport.Date);

            _actDayReport.AwayHours = tbxNotWork.Value != 0.00M ? tbxNotWork.Value.ToString() : "0";
            _actDayReport.WorkHours = tbxWorkHours.Value != 0.00M ? tbxWorkHours.Value.ToString() : "0";

            _actDayReport.ReportText = tbxReport.Rtf;

            if (cbxCompany.Checked)
                _actDayReport.Division = "Work";
            else if (cbxSchool.Checked)
                _actDayReport.Division = "School";
            else if (cbxOtherPlace.Checked)
                _actDayReport.Division = tbxOtherPlace.Text;
            else if (cbxHolidays.Checked)
            {
                _actDayReport.Holyday = "1";
                _actDayReport.Division = "---";
            }
            else if (cbxIll.Checked)
            {
                _actDayReport.Ill = "1";
                _actDayReport.Division = "---";
            }

            _actDayReport.Delivered = cbxDelivered.Checked ? "1" : "0";

            _actDayReport.SingleHours = new string[8];
            _actDayReport.SingleHours[0] = nudHour1.Value.ToString();
            _actDayReport.SingleHours[1] = nudHour2.Value.ToString();
            _actDayReport.SingleHours[2] = nudHour3.Value.ToString();
            _actDayReport.SingleHours[3] = nudHour4.Value.ToString();
            _actDayReport.SingleHours[4] = nudHour5.Value.ToString();
            _actDayReport.SingleHours[5] = nudHour6.Value.ToString();
            _actDayReport.SingleHours[6] = nudHour7.Value.ToString();

            _actDayReport.Comments = new string[2];
            _actDayReport.Comments[0] = tbxFirstCommentLine.Text;
            _actDayReport.Comments[1] = tbxSecondCommentLine.Text;

            DataManager.CreateDayReport(_actDayReport);

            fillReportedDays();

            lblSaved.Visible = true;
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 5000;
            t.Tick += new EventHandler(t_Tick);
            t.Start();

            return true;
        }

        private void btnClearReport_Click(object sender, EventArgs e)
        {
            resetFields();
            _changed = true;
        }

        #endregion

        #region checkboxoperations

        private void cbxOtherPlace_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loadingReport)
            {
                gpLocation.Text = "Ausbildungsort*";
                _changed = true;
            }
            if (cbxOtherPlace.Checked)
            {
                cbxCompany.Checked = false;
                cbxHolidays.Checked = false;
                cbxIll.Checked = false;
                cbxSchool.Checked = false;
                tbxWorkHours.Value = 8.00M;
                tbxNotWork.Value = 0.00M;
                tbxOtherPlace.Enabled = true;
            }
            else
            {
                tbxOtherPlace.Enabled = false;
                tbxOtherPlace.ResetText();
            }
        }

        private void cbxCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loadingReport)
            {
                gpLocation.Text = "Ausbildungsort*";
                _changed = true;
            }

            if (cbxCompany.Checked)
            {
                cbxHolidays.Checked = false;
                cbxIll.Checked = false;
                cbxSchool.Checked = false;
                cbxOtherPlace.Checked = false;
                tbxOtherPlace.ResetText();
                tbxWorkHours.Value = 8.00M;
                tbxNotWork.Value = 0.00M;
                tbxOtherPlace.Enabled = false;
            }
        }

        private void cbxSchool_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loadingReport)
            {
                gpLocation.Text = "Ausbildungsort*";
                _changed = true;
            }

            if (cbxSchool.Checked)
            {
                cbxCompany.Checked = false;
                cbxHolidays.Checked = false;
                cbxIll.Checked = false;
                cbxOtherPlace.Checked = false;
                tbxOtherPlace.ResetText();
                tbxWorkHours.Value = 8.00M;
                tbxNotWork.Value = 0.00M;
                tbxOtherPlace.Enabled = false;
            }
        }

        private void cbxIll_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loadingReport)
            {
                _changed = true;
                gpTimes.Text = "Ausbildungszeiten*";
            }

            if (cbxIll.Checked)
            {
                cbxCompany.Checked = false;
                cbxHolidays.Checked = false;
                cbxSchool.Checked = false;
                cbxOtherPlace.Checked = false;
                tbxOtherPlace.Enabled = false;
                tbxOtherPlace.ResetText();
                tbxReport.Text = "Krank";
                tbxWorkHours.Value = 0.00M;
                tbxNotWork.Value = 8.00M;
                gpLocation.Enabled = false;
            }
            else
            {
                gpLocation.Enabled = true;
                cbxCompany.Checked = true;
                tbxWorkHours.Value = 8.00M;
                tbxNotWork.Value = 0.00M;
            }
        }

        private void cbxHolidays_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loadingReport)
            {
                gpTimes.Text = "Ausbildungszeiten*";
                _changed = true;
            }

            if (cbxHolidays.Checked)
            {
                cbxCompany.Checked = false;
                cbxIll.Checked = false;
                cbxSchool.Checked = false;
                cbxOtherPlace.Checked = false;
                tbxOtherPlace.Enabled = false;
                tbxOtherPlace.ResetText();
                tbxReport.Text = "Urlaub";
                tbxWorkHours.Value = 0.00M;
                tbxNotWork.Value = 8.00M;
                gpLocation.Enabled = false;
            }
            else
            {
                gpLocation.Enabled = true;
                cbxCompany.Checked = true;
                tbxWorkHours.Value = 8.00M;
                tbxNotWork.Value = 0.00M;
            }
        }
        #endregion

        private void btnShowComments_Click(object sender, EventArgs e)
        {
            if (_actDayReport.IsMonday)
            {
                if ((sender as Button).Text.Contains("einblenden"))
                {
                    tbxFirstCommentLine.Visible = true;
                    tbxSecondCommentLine.Visible = true;
                    (sender as Button).Text = (sender as Button).Text.Replace("einblenden", "ausblenden");
                }
                else
                {
                    tbxFirstCommentLine.Visible = false;
                    tbxSecondCommentLine.Visible = false;
                    (sender as Button).Text = (sender as Button).Text.Replace("ausblenden", "einblenden");
                }
            }
            else
                MessageBox.Show("Bitte die Kommentare beim Bericht von Montag eintragen");
        }

        private void btnShowSingleHours_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text.Contains("einblenden"))
            {
                gpSingleHours.Visible = true;
                (sender as Button).Text = (sender as Button).Text.Replace("einblenden", "ausblenden");
            }
            else
            {
                gpSingleHours.Visible = false;
                (sender as Button).Text = (sender as Button).Text.Replace("ausblenden", "einblenden");
            }
        }

        private void nudHourValueChanged(object sender, EventArgs e)
        {
            if (nudHour1.Value + nudHour2.Value + nudHour3.Value + nudHour4.Value + nudHour5.Value + nudHour6.Value + nudHour7.Value > 0)
                tbxWorkHours.Value = nudHour1.Value + nudHour2.Value + nudHour3.Value + nudHour4.Value + nudHour5.Value + nudHour6.Value + nudHour7.Value;
            else
                tbxWorkHours.Value = 8.00M;
            gpSingleHours.Text = gpSingleHours.Text + "*";
        }
    }
}
