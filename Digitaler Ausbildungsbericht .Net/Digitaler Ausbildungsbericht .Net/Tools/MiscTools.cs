using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Management;
using System.Net;
using System.Windows.Forms;

namespace Digitaler_Ausbildungsbericht.Net
{
    internal static class MiscTools
    {
        internal static bool CheckFilesystem()
        {
            string workingDirectory = DataManager.GetWorkingDirectory();
            string[] folders = { "Data", "Reports", "SpellCheck" };
            foreach (string folder in folders)
                if (!Directory.Exists(workingDirectory + folder))
                    Directory.CreateDirectory(workingDirectory + folder);

            try
            {
                if (!File.Exists(workingDirectory + @"SpellCheck\de_DE.dic"))
                    File.WriteAllBytes(workingDirectory + @"SpellCheck\de_DE.dic", Res.Properties.Resources.de_DE_dic);
                if (!File.Exists(workingDirectory + @"SpellCheck\de_DE.aff"))
                    File.WriteAllBytes(workingDirectory + @"SpellCheck\de_DE.aff", Res.Properties.Resources.de_DE_aff);
            }
            catch { }

            if (!File.Exists(workingDirectory + @"Data\settings.csv"))
            {
                File.WriteAllText(DataManager.MakePath(DataManager.SettingsPath) + @"\settings.csv", Res.Properties.Resources.settings_csv);
                File.WriteAllText(DataManager.MakePath(DataManager.SettingsPath) + @"\basedata.csv", Res.Properties.Resources.basedata_csv);
                return true;
            }
            return false;
        }
    }
}
