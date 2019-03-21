using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TempUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            deleteFile("DevComponents.DotNetBar.Design.dll");
            deleteFile("DevComponents.DotNetBar2.dll");
            deleteFile("Hunspellx64.dll");
            deleteFile("Hunspellx86.dll");
            deleteFile("itextsharp.dll");
            deleteFile("NHunspell.dll");
            deleteFile("NHunspellExtender.dll");
            deleteFile("PDFForms.dll");
            deleteFile("Res.dll");

            File.WriteAllBytes(GetAppInstallFolder() + "DevComponents.DotNetBar.Design.dll", global::TempUpdater.Properties.Resources.DevComponents_DotNetBar_Design);
            File.WriteAllBytes(GetAppInstallFolder() + "DevComponents.DotNetBar2.dll", global::TempUpdater.Properties.Resources.DevComponents_DotNetBar2);
            File.WriteAllBytes(GetAppInstallFolder() + "Hunspellx64.dll", global::TempUpdater.Properties.Resources.Hunspellx64);
            File.WriteAllBytes(GetAppInstallFolder() + "Hunspellx86.dll", global::TempUpdater.Properties.Resources.Hunspellx86);
            File.WriteAllBytes(GetAppInstallFolder() + "itextsharp.dll", global::TempUpdater.Properties.Resources.itextsharp);
            File.WriteAllBytes(GetAppInstallFolder() + "NHunspell.dll", global::TempUpdater.Properties.Resources.NHunspell);
            File.WriteAllBytes(GetAppInstallFolder() + "NHunspellExtender.dll", global::TempUpdater.Properties.Resources.NHunspellExtender);
            File.WriteAllBytes(GetAppInstallFolder() + "PDFForms.dll", global::TempUpdater.Properties.Resources.PDFForms);
            File.WriteAllBytes(GetAppInstallFolder() + "Res.dll", global::TempUpdater.Properties.Resources.Res);

            File.WriteAllBytes(GetAppInstallFolder() + "Digitaler Ausbildungsbericht.Net2.exe", global::TempUpdater.Properties.Resources.Digitaler_Ausbildungsbericht_Net);
            Process p = new Process();
            p.StartInfo.FileName = GetAppInstallFolder() + "Digitaler Ausbildungsbericht.Net2.exe";
            p.StartInfo.Verb = "runas";
            p.Start();
        }

        static void deleteFile(string filename)
        {
            if (File.Exists(filename))
                try { File.Delete(filename); }
                catch { }
        }

        public static string GetAppInstallFolder()
        {
            if (Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) != string.Empty)
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Digital-Reports\\Digitaler Ausbildungsbericht.Net\\";
            else
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Digital-Reports\\Digitaler Ausbildungsbericht.Net\\";
        }
    }
}
