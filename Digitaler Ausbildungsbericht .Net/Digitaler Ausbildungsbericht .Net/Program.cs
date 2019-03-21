using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security.Principal;
using System.Reflection;

namespace Digitaler_Ausbildungsbericht.Net
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]

        public static void Main()
        {
            try
            {
                //MessageBox.Show(Assembly.GetExecutingAssembly().Location);
                if (Assembly.GetExecutingAssembly().Location.EndsWith("Digitaler Ausbildungsbericht.Net2.exe"))
                {
                    File.Delete(DataManager.GetAppInstallFolder() + "Digitaler Ausbildungsbericht.Net.exe");
                    File.Copy(DataManager.GetAppInstallFolder() + "Digitaler Ausbildungsbericht.Net2.exe", DataManager.GetAppInstallFolder() + "Digitaler Ausbildungsbericht.Net.exe", true);
                    Process.Start(DataManager.GetAppInstallFolder() + "Digitaler Ausbildungsbericht.Net.exe");
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()+ "\r\nBitte für weitere Infos auf http://ausbildungsbericht.net vorbeischauen.");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
