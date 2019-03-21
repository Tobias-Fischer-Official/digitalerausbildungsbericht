using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using Digitaler_Ausbildungsbericht.Net.Properties;
using System.Diagnostics;
using System.Xml;


namespace Digitaler_Ausbildungsbericht.Net
{
    internal static class DataManager
    {
        public const string SettingsPath = "Data";
        public const string ReportsPath = "Reports";
        private const string baseAddressProduction = "http://ausbildungsbericht.net/";
        private const string baseAddressDebug = "http://192.168.2.100/";
        public const string WebNewsPath = "mnews/news.php";
        public const string WebUpdatePath = "update/config.xml";

        //Gibt die entsprechende Dienst-Domain zurück
        //Betrifft die Statistiken und die Sicherung in der Cloud.
        public static string baseWebAddress
        {
            get {
                if (System.Diagnostics.Debugger.IsAttached)
                    return baseAddressProduction;
                else
                    return baseAddressProduction;
            }
        }

        //TODO Umstellen auf XML
        /// <summary>
        /// Lädt die Einstellungen des USers aus dem Dateisystem
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, string> LoadSettings()
        {
            Dictionary<string, string> returns = new Dictionary<string, string>();
            string[] csv = File.ReadAllLines(MakePath(SettingsPath) + @"\settings.csv");
            foreach (string s in csv)
                returns.Add(s.Split(';')[0], s.Split(';')[1]);
            return returns;
        }

        //TODO Methodenname auf 'GetPath' ändern
        /// <summary>
        /// Gibt den entsprechenden Pfad zu einem Ordner im Arbeitsverzeichnis zurück
        /// </summary>
        /// <param name="folder">Ordnername</param>
        /// <returns>FQDN zum Ordner</returns>
        public static string MakePath(string folder)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Digitaler Ausbildungsbericht.Net\\" + folder;
        }

        /// <summary>
        /// Arbeitsverzeichnis des Programms
        /// </summary>
        /// <returns>Arbeitsverzeichnis</returns>
        public static string GetWorkingDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Digitaler Ausbildungsbericht.Net\\";
        }

        /// <summary>
        /// Installationsverzeichnis
        /// </summary>
        /// <returns>Installationsverzeichnis</returns>
        public static string GetAppInstallFolder()
        {
            if (Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) != string.Empty)
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Digital-Reports\\Digitaler Ausbildungsbericht.Net\\";
            else
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Digital-Reports\\Digitaler Ausbildungsbericht.Net\\";
        }

        //TODO auf XML umstellen
        /// <summary>
        /// Speichert die Einstellungen des Users als CSV
        /// </summary>
        /// <param name="Settings">Die Schlüssel-Wert-Sammlung der Einstellungen</param>
        internal static void SaveSettings(Dictionary<string, string> Settings)
        {
            List<string> Lines = new List<string>();
            foreach (KeyValuePair<string, string> kvp in Settings)
                Lines.Add(kvp.Key + ";" + kvp.Value);
            File.WriteAllLines(MakePath(SettingsPath) + @"\settings.csv", Lines.ToArray());
        }

        /// <summary>
        /// Basisdaten des Users werden geladen (Name, Nachname etc.)
        /// </summary>
        /// <returns>Dictionary mit den Basisdaten des Users</returns>
        internal static Dictionary<string, string> LoadBaseData()
        {
            Dictionary<string, string> returns = new Dictionary<string, string>();
            string[] csv = File.ReadAllLines(MakePath(SettingsPath) + @"\basedata.csv");
            foreach (string s in csv)
                returns.Add(s.Split(';')[0], s.Split(';')[1]);
            return returns;
        }

        /// <summary>
        /// Basisdaten des Users werden gespeichert (Name, Nachname etc.)
        /// </summary>
        /// <param name="BaseData">Die BAsisdaten als Key-Value-Pair-Sammlung</param>
        internal static void SaveBaseData(Dictionary<string, string> BaseData)
        {
            List<string> Lines = new List<string>();
            foreach (KeyValuePair<string, string> kvp in BaseData)
                Lines.Add(kvp.Key + ";" + kvp.Value);
            File.WriteAllLines(MakePath(SettingsPath) + @"\basedata.csv", Lines.ToArray());
        }

        /// <summary>
        /// Speichert den Bericht in Form einer XML-Datei auf der Festplatte
        /// </summary>
        /// <param name="Rep">Der zu speichernde Bericht</param>
        internal static void CreateDayReport(Report Rep)
        {
            string filename = MakePath(ReportsPath) + "\\" + Rep.Date;
            XmlWriter writer = XmlWriter.Create(filename + ".xml");                       
            new XmlSerializer(typeof(Report)).Serialize(writer, Rep);
            writer.Flush();
            writer.Close();
        }

        //TODO siehe obige Klasse, ersetzen durch boolean ob Wöchentlich oder Täglich
        internal static void CreateWeekReport(Report Rep)
        {
            string filename = MakePath(ReportsPath) + "\\W" + Rep.AppWeekNumber;
            XmlWriter writer = XmlWriter.Create(filename + ".xml");
            new XmlSerializer(typeof(Report)).Serialize(writer, Rep);
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// Holt den Bericht des angegebenen Datums aus dem Filesystem und deserialisiert die geladene XML-Datei
        /// </summary>
        /// <param name="Date">Datum des Berichts</param>
        /// <returns>Den entsprechenden Bericht</returns>
        internal static Report GetReportOfDate(DateTime Date)
        {
            string filename = GetFileNameByDate(Date);
            if (filename.Replace(GetWorkingDirectory() + ReportsPath + "\\", String.Empty) != string.Empty)
            {
                try
                {
                    XmlSerializer XmlSe = new XmlSerializer(typeof(Report));
                    TextReader reader = new StreamReader(filename);
                    Report r = (Report)XmlSe.Deserialize(reader);
                    reader.Close();
                    return r;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        internal static string GetFileNameByDate(DateTime dt)
        {
            string returns = string.Empty;
            string dts = dt.ToShortDateString();
            foreach (string s in Directory.GetFiles(MakePath(ReportsPath)))
                if (s.Contains(dts))
                    returns = s;
            return returns;
        }

        internal static Report GetReportOfWeek(int WeekNumber)
        {
            string filename = GetFileNameByWeek(WeekNumber);
            if (filename != string.Empty)
            {
                XmlSerializer XmlSe = new XmlSerializer(typeof(Report));
                XmlReader reader = XmlReader.Create(filename);
                Report r = (Report)XmlSe.Deserialize(reader);
                reader.Close();
                return r;
            }
            return null;
        }

        internal static string GetFileNameByWeek(int WeekNumber)
        {
            string returns = string.Empty;
            foreach (string s in Directory.GetFiles(MakePath(ReportsPath)))
                if (s == MakePath(ReportsPath) + "\\W" + WeekNumber.ToString() + ".xml")
                    returns = s;
            return returns;
        }

        internal static Report[] getWeeklyReports(DateTime startDate, bool saturday)
        {
            List<Report> Reports = new List<Report>();

            for (int i = 0; i <= (saturday ? 6 : 5); i++)
                Reports.Add(GetReportOfDate(startDate.AddDays(i)));

            return Reports.ToArray();
        }

        internal static ReportedDay[] GetAllReportedDays()
        {
            List<ReportedDay> repDays = new List<ReportedDay>();
            foreach (string file in Directory.GetFiles(MakePath(ReportsPath)))
            {
                XmlSerializer XmlSe = new XmlSerializer(typeof(Report));
                XmlReader reader = XmlReader.Create(file);
                Report returns = (Report)XmlSe.Deserialize(reader);
                reader.Close();
                repDays.Add(new ReportedDay(returns.Delivered == "1" ? true : false, DateTime.Parse(returns.Date)));
            }
            return repDays.ToArray();
        }

        internal static ReportedWeek[] getAllReportedWeeks()
        {
            List<ReportedWeek> repWeeks = new List<ReportedWeek>();
            foreach (string file in Directory.GetFiles(MakePath(ReportsPath)))
            {
                XmlSerializer XmlSe = new XmlSerializer(typeof(Report));
                XmlReader reader = XmlReader.Create(file);
                Report returns = (Report)XmlSe.Deserialize(reader);
                reader.Close();
                if (returns.YearWeekNumber != null)
                    repWeeks.Add(new ReportedWeek(returns.Delivered == "1" ? true : false, int.Parse(returns.YearWeekNumber), int.Parse(returns.Year)));
            }
            return repWeeks.ToArray();
        }

        internal static void ExtractAndOpenManual()
        {
            try
            {
                File.WriteAllBytes(GetWorkingDirectory() + "Manual.pdf", Resources.Anleitung___Digitaler_Ausbildungsbericht);
                Process.Start(GetWorkingDirectory() + "Manual.pdf");
            }
            catch
            {
                MessageBox.Show("Bitte installieren sie den Adobe Reader, um die Anleitung zu öffnen.", "Fehler - Adobe Reader nicht installiert");
            }
        }

        internal static Dictionary<string, string> LoadTxtBlocks()
        {
            string file = GetWorkingDirectory() + "blocks.tbs";
            Dictionary<string, string> returns = new Dictionary<string, string>();
            if (File.Exists(file))
            {
                string[] Lines = File.ReadAllText(file).Split('\f');
                foreach (string line in Lines)
                    if (line != string.Empty)
                        returns.Add(line.Split('\a')[0], line.Split('\a')[1]);
                return returns;
            }
            else
                return null;
        }

        internal static void SaveTxtBlocks(Dictionary<string, string> Blocks)
        {
            string Lines = string.Empty;
            foreach (KeyValuePair<string, string> kvp in Blocks)
            {
                string rtf = kvp.Value.Replace("\\par}", "}");
                rtf = kvp.Value.Replace("\\par\r\n}", "}");
                Lines += (kvp.Key + "\a" + rtf + "\f");
            }
            File.WriteAllText(GetWorkingDirectory() + "blocks.tbs", Lines);
        }

    }
}