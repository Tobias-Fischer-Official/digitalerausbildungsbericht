using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Text.RegularExpressions;

namespace Digitaler_Ausbildungsbericht.Net
{
    public partial class frmFeedback : Office2007Form
    {
        public bool TestEmailRegex(string emailAddress)
        {
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
                  + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                  + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                  + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                  + @"[a-zA-Z]{2,}))$";
            Regex reStrict = new Regex(patternStrict);
            bool isStrictMatch = reStrict.IsMatch(emailAddress);
            return isStrictMatch;
        }

        public frmFeedback()
        {
            InitializeComponent();
            tbxName.Text = DataManager.LoadBaseData()["vorname"] + " " + DataManager.LoadBaseData()["nachname"];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (cbChooseSubject.Text != string.Empty && tbxEmail.Text != string.Empty && tbxName.Text != string.Empty && tbxText.Text != string.Empty)
                if (TestEmailRegex(tbxEmail.Text))
                {
                    SendStats.SendFeedback(tbxText.Text, tbxEmail.Text, tbxName.Text, cbChooseSubject.Text);
                    this.Close();
                }
                else
                    MessageBox.Show("Tragen sie bitte eine gültige E-Mail-Adresse ein", "E-Mail ungültig");
            else
                MessageBox.Show("Füllen sie bitte alle Felder aus", "Nicht alle angaben vorhanden");
        }
    }
}
