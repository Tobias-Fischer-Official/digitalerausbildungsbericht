using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Reflection;
using Digitaler_Ausbildungsbericht.Net.Properties;

namespace Digitaler_Ausbildungsbericht.Net
{
    internal partial class frmAbout : Office2007RibbonForm
    {
        internal frmAbout()
        {
            InitializeComponent();
            lblVersion.Text = Application.ProductVersion;
            tbxLicense.Text = Resources.License;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
