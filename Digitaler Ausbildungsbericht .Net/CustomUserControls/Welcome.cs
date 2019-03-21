using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Digitaler_Ausbildungsbericht.Net.Properties;

namespace Digitaler_Ausbildungsbericht.Net
{
    internal partial class Welcome : UserControl
    {
        internal Welcome()
        {
            InitializeComponent();
            tbxLicense.Text = Resources.License;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = DialogResult.Cancel;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = DialogResult.OK;
        }


    }
}
