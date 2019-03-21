using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Threading;

namespace Digitaler_Ausbildungsbericht.Net
{
    public partial class frmExport : Office2007Form
    {
        public enum ExportType
        {
            CSV,
            MySQL,
            XML,
            Text,
            none
        }

        private string _exPath;
        private Dictionary<string, Report> _allReports = new Dictionary<string, Report>();
        private string _repType;

        private string[] ifn;

        public frmExport()
        {
            InitializeComponent();
            this._repType = DataManager.LoadSettings()["ReportType"];
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (tcImExport.SelectedTab.Name == "tabExp")
                Export();
            else
                Import(ifn);
        }

        private void Import(string[] FileNames)
        {
            List<Report> imported = new List<Report>();
            if (ifn == null || ifn.Length <= 0)
                MessageBox.Show("Keine Dateien ausgewählt");
            foreach (string filename in FileNames)
            {
                string end = filename.Split('.')[filename.Split('.').Length - 1];
                switch (end)
                {
                    case "sql":
                        MessageBox.Show("sql");
                        break;
                    case "txt":
                        MessageBox.Show("txt");
                        break;
                    case "xml":
                        XmlSerializer XmlSe = new XmlSerializer(typeof(Report[]));
                        XmlReader reader = XmlReader.Create(filename);
                        Report[] reports = (Report[])XmlSe.Deserialize(reader);
                        MessageBox.Show(reports[0].ReportText);
                        break;
                    case "csv":
                        string[] allReports = File.ReadAllLines(filename);
                        foreach (string newreport in allReports)
                        {
                            string[] paramS = newreport.Split('\a');
                            Report report = new Report();
                            report.AppWeekNumber = paramS[0];
                            report.AwayHours = paramS[1];
                            report.CompanyText = paramS[2];
                            report.Date = paramS[3];
                            report.Delivered = paramS[4];
                            report.Division = paramS[5];
                            report.Holyday = paramS[6];
                            report.Id = paramS[7];
                            report.Ill = paramS[8];
                            report.InstructionsText = paramS[9];
                            report.ReportText = paramS[10];
                            report.SchoolText = paramS[11];
                            report.WorkHours = paramS[12];
                            report.Year = paramS[13];
                            report.YearWeekNumber = paramS[14];

                        }
                        MessageBox.Show("csv");
                        break;
                }
            }
        }

        private void Export()
        {
            if (lbxDates.SelectedItems.Count == 0)
            {
                MessageBox.Show("Bitte mindestens einen Bericht auswählen");
                return;
            }
            tbxLog.AppendText("Starte Backup\r\n");
            List<string> lines = new List<string>();
            string path = _exPath + "\\Ausbildungsberichte-Backups - " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString().Replace(":", "-");
            tbxLog.AppendText("Backuptyp: XML\r\n");
            List<Report> reps = new List<Report>();
            foreach (string reportid in lbxDates.SelectedItems)
                reps.Add(_allReports[reportid]);
            path += ".xml";
            XmlWriter writer = XmlWriter.Create(path);
            Report[] repsA = reps.ToArray();
            new XmlSerializer(typeof(Report[])).Serialize(writer, repsA);
            writer.Flush();
            writer.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.Description = "Wählen Sie den Ordner, in welchem die Backup-Datei gespeichert werden soll.";
            if (fbd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                _exPath = fbd.SelectedPath;
            tbxExportPath.Text = _exPath;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (lbxDates.Items.Count > 0)
            {
                lbxDates.SelectedItems.Clear();

                dtpStartDate.Text = "";
                dtpEndDate.Text = "";

                dtpStartDate.Text = lbxDates.Items[0].ToString();
                dtpEndDate.Text = lbxDates.Items[lbxDates.Items.Count - 1].ToString();
            }
        }

        private void btnSelectSchool_Click(object sender, EventArgs e)
        {
            lbxDates.SelectedItems.Clear();
            foreach (KeyValuePair<string, Report> kvp in this._allReports)
                if (kvp.Value.Division == "School")
                    lbxDates.SelectedItems.Add(kvp.Key);
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            lbxDates.SelectedItems.Clear();
        }

        private void frmExport_Load(object sender, EventArgs e)
        {
            loadAllReportFileNames();
        }

        private void loadAllReportFileNames()
        {
            if (this._repType.StartsWith("dayly"))
                foreach (string file in Directory.GetFiles(DataManager.MakePath(DataManager.ReportsPath)))
                {
                    XmlSerializer XmlSe = new XmlSerializer(typeof(Report));
                    XmlReader reader = XmlReader.Create(file);
                    Report r = (Report)XmlSe.Deserialize(reader);
                    _allReports.Add(r.Date, r);
                    lbxDates.Items.Add(r.Date);
                    reader.Close();
                }
            else
                foreach (string file in Directory.GetFiles(DataManager.MakePath(DataManager.ReportsPath)))
                {
                    XmlSerializer XmlSe = new XmlSerializer(typeof(Report));
                    XmlReader reader = XmlReader.Create(file);
                    Report r = (Report)XmlSe.Deserialize(reader);
                    _allReports.Add(r.YearWeekNumber, r);
                    lbxDates.Items.Add(r.YearWeekNumber);
                    reader.Close();
                }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            selectReports();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            selectReports();
        }

        private void selectReports()
        {
            lbxDates.SelectedItems.Clear();
            if (this._repType.StartsWith("dayly"))
            {
                int i = 0;
                List<int> add = new List<int>();
                foreach (string item in lbxDates.Items)
                {
                    if (
                        DateTime.Parse(item) >= DateTime.Parse(dtpStartDate.Value.ToShortDateString())
                        &&
                        DateTime.Parse(item) <= DateTime.Parse(dtpEndDate.Value.ToShortDateString())
                        )
                        add.Add(i);
                    i++;
                }
                foreach (int x in add)
                    lbxDates.SelectedItems.Add(lbxDates.Items[x]);
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Dateien|*.xml";
            ofd.Title = "Wähle Datei/en zum Importieren";
            ofd.Multiselect = true;
            ofd.ShowDialog();
            ifn = ofd.FileNames;
        }
    }
}
