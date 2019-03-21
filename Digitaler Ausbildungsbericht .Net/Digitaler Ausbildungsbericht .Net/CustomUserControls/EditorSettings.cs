using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Digitaler_Ausbildungsbericht.Net
{
    internal partial class EditorSettings : UserControl
    {
        private Dictionary<string, string> _settings = new Dictionary<string, string>();
        FolderBrowserDialog fbd;

        internal EditorSettings()
        {
            InitializeComponent();
            cbxSelectReporttype.SelectedItem = 0;

            this.AutoSize = true;

            _settings = DataManager.LoadSettings();
            if (!_settings.ContainsKey("FileNames"))
                _settings.Add("FileNames", tbxFilenames.Text);
            else
                tbxFilenames.Text = _settings["FileNames"];
            if (!_settings.ContainsKey("PDFDir"))
                _settings.Add("PDFDir", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Ausbildungsberichte");
            if (!_settings.ContainsKey("AutoUpdate"))
                _settings.Add("AutoUpdate", "1");
            if (!_settings.ContainsKey("ReportType"))
                _settings.Add("ReportType", "");
            else
                switch (_settings["ReportType"])
                {
                    case "dayly":
                        cbxSelectReporttype.SelectedIndex = 0;
                        break;
                    case "dayly+sa":
                        cbxSelectReporttype.SelectedIndex = 1;
                        break;
                    case "weekly":
                        cbxSelectReporttype.SelectedIndex = 2;
                        break;
                }
            tbxPDFFolder.Text = _settings["PDFDir"];
            FillPreview();
        }


        private void btnChangePDFDir_Click(object sender, EventArgs e)
        {
            fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.Description = "Bitte geben Sie ein gültiges Verzeichnis zum Speichern der PDF-Dateien an";
            fbd.ShowDialog();
            tbxPDFFolder.Text = fbd.SelectedPath;
            _settings["PDFDir"] = tbxPDFFolder.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _settings["AutoUpdate"] = cbxAutoupdate.Checked ? "1" : "0";
            _settings["FileNames"] = tbxFilenames.Text;
            if (!_settings["FileNames"].Contains("{WS}") && !_settings["FileNames"].Contains("{WE}") && !_settings["FileNames"].Contains("{KW}") && !_settings["FileNames"].Contains("{AW}"))
            {
                MessageBox.Show("Bitte gib mindestens eine Dynamische Variable {WS},{WE},{KW} oder {AW} in dem Dateinamen an", "Ungültiger Dateiname");
                return;
            }

            if (!Directory.Exists(tbxPDFFolder.Text))
            {
                if (MessageBox.Show("Das angebene Verzeichnis existiert nicht, soll das Programm versuchen es anzulegen?", "Achtung", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {
                        Directory.CreateDirectory(_settings["PDFDir"]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\r\n\r\nBitte geben Sie ein gültiges Verzeichnis zum Speichern der PDF-Dateien an!", "Konnte Verzeichnis nicht anlegen!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Bitte geben Sie ein gültiges Verzeichnis zum Speichern der PDF-Dateien an!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (cbxSelectReporttype.Text == string.Empty)
            {
                MessageBox.Show("Bitte wählen Sie eine Berichtsform!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataManager.SaveSettings(_settings);
            this.ParentForm.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void cbxSelectReporttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            _settings["ReportType"] = cbxSelectReporttype.SelectedIndex == 0 ? "dayly" : cbxSelectReporttype.SelectedIndex == 1 ? "dayly+sa" : cbxSelectReporttype.SelectedIndex == 2 ? "weekly" : String.Empty;
        }


        private void cbxAutoupdate_CheckedChanged(object sender, EventArgs e)
        {
            _settings["AutoUpdate"] = cbxAutoupdate.Checked ? "1" : "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,
                "Wähle hier das gewünschte Format, wie die gespeicherten Berichte benannt werden sollen" + Environment.NewLine +
                "{KW} = Kalenderwoche" + Environment.NewLine +
                "{AW} = Ausbildungswoche" + Environment.NewLine +
                "{WS} = Woche Startdatum" + Environment.NewLine +
                "{WE} = Woche Enddatum" + Environment.NewLine +
                "{VN} = Dein Vorname" + Environment.NewLine +
                "{NN} = Dein Nachnachname" + Environment.NewLine +
                "Beispiel: Ausbildunsgbericht {NN} der Woche {KW} von {WS} bis {WE}" + Environment.NewLine +
                "So siehts aus: Ausbildungsbericht NachName der woche XX von XX.XX.XXXX bis XX.XX.XXXX.pdf"
                , "Dateinamen erstellen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tbxFilenames_TextChanged(object sender, EventArgs e)
        {
            _settings["FileNames"] = tbxFilenames.Text;
            _settings["FileNames"] = _settings["FileNames"].Replace("\\", "");
            _settings["FileNames"] = _settings["FileNames"].Replace("/", "");
            _settings["FileNames"] = _settings["FileNames"].Replace(":", "");
            _settings["FileNames"] = _settings["FileNames"].Replace("*", "");
            _settings["FileNames"] = _settings["FileNames"].Replace("?", "");
            _settings["FileNames"] = _settings["FileNames"].Replace("\"", "");
            _settings["FileNames"] = _settings["FileNames"].Replace("<", "");
            _settings["FileNames"] = _settings["FileNames"].Replace(">", "");
            _settings["FileNames"] = _settings["FileNames"].Replace("|", "");
            FillPreview();
        }

        internal void FillPreview()
        {
            string pre = tbxFilenames.Text;
            pre = pre.Replace("{KW}", DateTimeTools.GetWeekOfYear(DateTime.Now).ToString());
            pre = pre.Replace("{AW}", DateTimeTools.GetActAppWeek(DateTime.Parse(DataManager.LoadBaseData()["beginn"]), DateTime.Now).ToString());
            pre = pre.Replace("{WS}", DateTimeTools.getWeekAsDays(DateTimeTools.GetWeekOfYear(DateTime.Now), DateTime.Now.Year)[0].ToShortDateString());
            pre = pre.Replace("{WE}", DateTimeTools.getWeekAsDays(DateTimeTools.GetWeekOfYear(DateTime.Now), DateTime.Now.Year)[6].ToShortDateString());
            pre = pre.Replace("{VN}", DataManager.LoadBaseData()["vorname"]);
            pre = pre.Replace("{NN}", DataManager.LoadBaseData()["nachname"]);
            lblPreview.Text = pre;
        }
    }
}

