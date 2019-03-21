using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors.DateTimeAdv;

namespace Digitaler_Ausbildungsbericht.Net
{
    internal partial class EditorBaseData : UserControl
    {
        private Dictionary<string, string> _baseData;

        internal EditorBaseData()
        {
            InitializeComponent();
            _loadData();
        }

        private void _loadData()
        {
            _baseData = DataManager.LoadBaseData();
            if (_baseData["vorname"] != string.Empty)
                vorname.Text = _baseData["vorname"];
            if (_baseData["nachname"] != string.Empty)
                nachname.Text = _baseData["nachname"];
            if (_baseData["ausbilder"] != string.Empty)
                ausbilder.Text = _baseData["ausbilder"];
            if (_baseData["beruf"] != string.Empty)
                beruf.Text = _baseData["beruf"];
            if (_baseData["fachrichtung"] != string.Empty)
                fachrichtung.Text = _baseData["fachrichtung"];
            if (_baseData["firma"] != string.Empty)
                firma.Text = _baseData["firma"];
            if (_baseData["beginn"] != string.Empty)
                beginn.Text = _baseData["beginn"];
            if (_baseData["ende"] != string.Empty)
                ende.Text = _baseData["ende"];
            if (_baseData["abteilung"] != string.Empty)
                abteilung.Text = _baseData["abteilung"];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool save = true;
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(TextBoxX))
                {
                    TextBoxX tbx = (c as TextBoxX);
                    if (tbx.Text != String.Empty)
                    {
                        if (_baseData.ContainsKey(tbx.Name))
                            _baseData[tbx.Name] = tbx.Text;
                        else
                            _baseData.Add(tbx.Name, tbx.Text);
                    }
                    else
                    {
                        MessageBox.Show("Bitte alle Daten vollständig ausfüllen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        save = false;
                        break;
                    }
                }
                if (c.GetType() == typeof(DateTimeInput))
                {
                    DateTimeInput dti = (c as DateTimeInput);
                    if (!dti.IsEmpty)
                    {
                        if (_baseData.ContainsKey(dti.Name))
                            _baseData[dti.Name] = dti.Value.ToShortDateString();
                        else
                            _baseData.Add(dti.Name, dti.Value.ToShortDateString());
                    }
                    else
                    {
                        MessageBox.Show("Bitte alle Daten vollständig ausfüllen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        save = false;
                        break;
                    }
                }
            }
            if (save)
            {
                DataManager.SaveBaseData(_baseData);
                this.ParentForm.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
