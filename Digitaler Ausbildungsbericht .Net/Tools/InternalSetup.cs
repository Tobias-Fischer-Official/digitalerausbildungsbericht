using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Digitaler_Ausbildungsbericht.Net.Tools
{
    public static class InternalSetup
    {
        public static void setupAssist(int lastUndoneSetupStep,ref Dictionary<string,string> settings)
        {            
            switch (lastUndoneSetupStep)
            {
                case 0:
                    frmSetupFrame WelcomeForm = new frmSetupFrame();
                    WelcomeForm.AutoSize = true;
                    WelcomeForm.StartPosition = FormStartPosition.CenterScreen;
                    WelcomeForm.Text = "Einrichtung: Schritt 1 von 3 || Willkommen";

                    Welcome w = new Welcome();
                    w.Dock = DockStyle.Fill;
                    WelcomeForm.panel.Controls.Add(w);
                    WelcomeForm.CancelButton = w.btnCancel;
                    if (WelcomeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        settings = DataManager.LoadSettings();
                        settings["lastUndoneSetupStep"] = "1";
                        DataManager.SaveSettings(settings);
                        frmSetupFrame BaseData = new frmSetupFrame();
                        BaseData.Text = "Einrichtung: Schritt 2 von 3 || Stammdaten eintragen";
                        BaseData.AutoSize = true;
                        BaseData.StartPosition = FormStartPosition.CenterScreen;

                        EditorBaseData bde = new EditorBaseData();
                        bde.Dock = DockStyle.Fill;
                        BaseData.AcceptButton = bde.btnSave;

                        BaseData.panel.Controls.Add(bde);
                        if (BaseData.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            settings = DataManager.LoadSettings();
                            settings["lastUndoneSetupStep"] = "2";
                            DataManager.SaveSettings(settings);
                            frmSetupFrame SetupPreferences = new frmSetupFrame();
                            SetupPreferences.Text = "Einrichtung: Schritt 3 von 3 || Grundeinstellungen";
                            SetupPreferences.StartPosition = FormStartPosition.CenterScreen;
                            SetupPreferences.AutoSize = true;
                            SetupPreferences.Size = new System.Drawing.Size(0, 0);


                            EditorSettings bs = new EditorSettings();
                            bs.Dock = DockStyle.Fill;
                            SetupPreferences.AcceptButton = bs.btnSave;
                            SetupPreferences.CancelButton = bs.btnCancel;

                            SetupPreferences.panel.Controls.Add(bs);
                            if (SetupPreferences.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                settings = DataManager.LoadSettings();
                                settings["lastUndoneSetupStep"] = "3";
                                DataManager.SaveSettings(settings);
                                frmSetupFrame FirstHelp = new frmSetupFrame();
                            }//FirstHelp.Show();
                            else
                                Environment.Exit(0);
                        }
                        else
                            Environment.Exit(0);
                    }
                    else
                        Environment.Exit(0);
                    break;
                case 1:
                    frmSetupFrame BaseData1 = new frmSetupFrame();
                    BaseData1.Text = "Einrichtung: Schritt 2 von 3 || Stammdaten eintragen";
                    BaseData1.AutoSize = true;
                    BaseData1.StartPosition = FormStartPosition.CenterScreen;

                    EditorBaseData bde1 = new EditorBaseData();
                    bde1.Dock = DockStyle.Fill;
                    BaseData1.AcceptButton = bde1.btnSave;

                    BaseData1.panel.Controls.Add(bde1);
                    if (BaseData1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        frmSetupFrame SetupPreferences = new frmSetupFrame();
                        SetupPreferences.Text = "Einrichtung: Schritt 3 von 3 || Grundeinstellungen";
                        SetupPreferences.StartPosition = FormStartPosition.CenterScreen;
                        SetupPreferences.AutoSize = true;
                        SetupPreferences.Size = new System.Drawing.Size(0, 0);


                        EditorSettings bs = new EditorSettings();
                        bs.Dock = DockStyle.Fill;
                        SetupPreferences.AcceptButton = bs.btnSave;
                        SetupPreferences.CancelButton = bs.btnCancel;

                        SetupPreferences.panel.Controls.Add(bs);
                        settings = DataManager.LoadSettings();
                        settings["lastUndoneSetupStep"] = "2";
                        DataManager.SaveSettings(settings);
                        if (SetupPreferences.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            settings = DataManager.LoadSettings();
                            settings["lastUndoneSetupStep"] = "3";
                            DataManager.SaveSettings(settings);
                        }//FirstHelp.Show();

                        else
                            Environment.Exit(0);
                    }
                    else
                        Environment.Exit(0);
                    break;
                case 2:
                    frmSetupFrame SetupPreferences2 = new frmSetupFrame();
                    SetupPreferences2.Text = "Einrichtung: Schritt 3 von 3 || Grundeinstellungen";
                    SetupPreferences2.StartPosition = FormStartPosition.CenterScreen;
                    SetupPreferences2.AutoSize = true;
                    SetupPreferences2.Size = new System.Drawing.Size(0, 0);

                    EditorSettings bs2 = new EditorSettings();
                    bs2.Dock = DockStyle.Fill;
                    SetupPreferences2.AcceptButton = bs2.btnSave;
                    SetupPreferences2.CancelButton = bs2.btnCancel;

                    SetupPreferences2.panel.Controls.Add(bs2);
                    if (SetupPreferences2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        settings = DataManager.LoadSettings();
                        settings["lastUndoneSetupStep"] = "1";
                        DataManager.SaveSettings(settings);
                    }//FirstHelp.Show();
                    else
                        Environment.Exit(0);
                    break;
                case 3:
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
