using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Digitaler_Ausbildungsbericht.Net
{
    internal static class SendStats
    {

        const string hwidAddress = "stats/stats.php?hwid=";
        static string version = Application.ProductVersion;
        static string ratingString = string.Empty;
        static string feedbackString = string.Empty;

        internal static void SendFeedback(string feedback, string mail, string name, string topic)
        {
            feedbackString = DataManager.baseWebAddress + hwidAddress + GetHwid() + "&request=feedback&type=" + topic + "&mail=" + mail + "&name=" + name + "&feedback=" + feedback + "&version=" + version;
            MessageBox.Show("Feedback wird gesendet", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Thread thr = new Thread(SendFeedback);
            thr.Start();
        }

        internal static void SendFeedback()
        {
            try
            {
                new WebClient().DownloadStringAsync(new Uri(feedbackString));
                MessageBox.Show("Feedback wird gesendet", "Danke für Ihre Meinung", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Feedback konnte nicht gesendet werden", "Verbindungsfehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static void SendStart()
        {
            try
            {
                new WebClient().DownloadStringAsync(new Uri(DataManager.baseWebAddress + hwidAddress + GetHwid() + "&request=start&version=" + version.ToString().Replace(".", string.Empty)));
            }
            catch { }
        }

        internal static void SendRating(string ratingtype, int rating)
        {
            ratingString = DataManager.baseWebAddress + hwidAddress + GetHwid() + "&request=rating&type=" + ratingtype + "&rating=" + rating.ToString() + "&version=" + version.ToString().Replace(".", string.Empty);
            Thread thr = new Thread(SendRating);
            thr.Start();
        }

        static void SendRating()
        {
            try
            {
                new WebClient().DownloadString(new Uri(ratingString));
            }
            catch
            {
                MessageBox.Show("Bewertung konnte nicht gesendet werden, Verbindungsfehler.");
            }
        }

        internal static void SendDocumentCreated()
        {
            try
            {
                Dictionary<string, string> baseData = DataManager.LoadBaseData();
                
                string data = DataManager.baseWebAddress + hwidAddress + GetHwid() +
                    "&request=report" +
                    "&version=" + version.ToString().Replace(".", string.Empty) +
                    "&type=" + DataManager.LoadSettings()["ReportType"] +
                    "&job=" + baseData["beruf"] +
                    "&field=" + baseData["fachrichtung"] +
                    "&start=" + baseData["beginn"] +
                    "&end=" + baseData["ende"];

                new WebClient().DownloadStringAsync(new Uri(data));
            }
            catch (Exception ex){
            
            }
        }

        internal static void SendPDFSaved()
        {
            try
            {
                Dictionary<string, string> baseData = DataManager.LoadBaseData();

                new WebClient().DownloadStringAsync(new Uri(DataManager.baseWebAddress + hwidAddress + GetHwid() +
                    "&request=pdfsaved" +
                    "&version=" + version.ToString().Replace(".", string.Empty)
                    ));
            }
            catch { }
        }

        internal static string GetHwid()
        {
            string drive = string.Empty;

            foreach (System.IO.DriveInfo compDrive in System.IO.DriveInfo.GetDrives())
                if (compDrive.IsReady)
                {
                    drive = compDrive.RootDirectory.ToString();
                    break;
                }

            drive = drive.EndsWith(":\\") ? drive.Substring(0, drive.Length - 2) : drive;

            string volumeSerial = string.Empty;
            System.Management.ManagementObject disk = new System.Management.ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            disk.Get();

            volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            string a = Environment.OSVersion.Version.ToString();
            string b = volumeSerial;
            string c = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();

            return a + b + c;
        }
    }
}
