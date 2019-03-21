using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Net;
using System.Security.Cryptography;
using Digitaler_Ausbildungsbericht.Net.Tools;

namespace Digitaler_Ausbildungsbericht.Net
{
    public partial class frmCloudLogin : Office2007Form
    {
        public frmCloudLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string hash = null;
            using (MD5 md5Hash = MD5.Create()) 
                hash = MD5Hasher.GetMd5Hash(md5Hash, tbxPassword.Text); 

            string key = null;
            string serviceUrl = DataManager.baseWebAddress + "services/cloudlogin.php?email=" + tbxEmail.Text + "&password=" + hash;
            try
            {
                using (WebClient wc = new WebClient())
                    key = wc.DownloadString(serviceUrl);
            }
            catch { MessageBox.Show("Verbindung zum Cloud-Dienst kann nicht hergestellt werden."); }
        }

        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmCloudRegistration registerForm = new frmCloudRegistration();
            registerForm.ShowDialog();
        }

       
    }
}