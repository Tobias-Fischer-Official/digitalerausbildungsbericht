using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitaler_Ausbildungsbericht.Net
{
    public partial class frmCloudRegistration : Office2007Form
    {
        public frmCloudRegistration()
        {
            InitializeComponent();
            this.webBrowser1.Navigate(DataManager.baseWebAddress + "services/register.php");
        }
    }
}
