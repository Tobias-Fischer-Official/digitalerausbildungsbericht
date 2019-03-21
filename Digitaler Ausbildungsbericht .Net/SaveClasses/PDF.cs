using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text.pdf;
using System.Windows.Forms;
using System.Threading;
using PDFForms.Properties;
using System.Diagnostics;
using iTextSharp.text;
using System.Drawing;

namespace Digitaler_Ausbildungsbericht.Net
{
    internal static class PDF
    {
        internal static string SaveDaylyReport(string ausbwoche, string kalwoche, string jahr, string fullname, string beruf, string fachrichtung, Report[] reports, string firmenname, string saveDir, string ausbilder, string abteilung)
        {

            /* "{KW} = Kalenderwoche"+Environment.NewLine+
                "{AW} = Ausbildungswoche"+Environment.NewLine+
                "{WS} = Woche Startdatum"+Environment.NewLine+
                "{WE} = Woche Enddatum"+Environment.NewLine+
                "{VN} = Dein Vorname"+Environment.NewLine+
                "{NN} = Dein Nachnachname"+Environment.NewLine+         
            */
            string pre = DataManager.LoadSettings()["FileNames"];
            pre = pre.Replace("{KW}", kalwoche);
            pre = pre.Replace("{AW}", ausbwoche);
            pre = pre.Replace("{WS}", reports[0].Date);
            pre = pre.Replace("{WE}", reports[5] != null ? reports[reports.Length - 2].Date : reports[reports.Length - 3].Date);
            pre = pre.Replace("{VN}", DataManager.LoadBaseData()["vorname"]);
            pre = pre.Replace("{NN}", DataManager.LoadBaseData()["nachname"]);

            string newFile = saveDir + "\\" + pre + ".pdf";

            byte[] file = reports[5] != null ? Resources.nachweis_samstag.ToArray<byte>() : Resources.nachweis.ToArray<byte>();

            if (!File.Exists(newFile))
            {
                Thread thr = new Thread(SendStats.SendDocumentCreated);
                thr.Start();
            }
            else if (MessageBox.Show("Die Datei exisistiert bereits, möchten Sie diese überschreiben?", "Datei bereits vorhanden", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return string.Empty;

            PdfReader pdfReader = new PdfReader(file);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            try
            {
                AcroFields pdfFormFields = pdfStamper.AcroFields;

                decimal gesWorkHours = 0;
                decimal gesAwayHours = 0;

                List<string> abteilungen = new List<string>();
                foreach (Report r in reports)
                    if (r != null)
                    {
                        gesWorkHours += decimal.Parse(r.WorkHours);
                        gesAwayHours += decimal.Parse(r.AwayHours);
                        if (!abteilungen.Contains(r.Division) && r.Division != "---")
                            abteilungen.Add(r.Division);
                    }

                string abt = "";
                foreach (string s in abteilungen)
                    abt += s + ",";

                abt = abt.TrimEnd(',');
                abt = abt.Replace("Work", abteilung);
                abt = abt.Replace("School", "Berufsschule");

                DateTime beginnDate = DateTime.Parse(DataManager.LoadBaseData()["beginn"]);
                DateTime[] dta = DateTimeTools.GetWorkWeekStartAndEnd(beginnDate);
                if (dta[0].Month < beginnDate.Month)
                    ausbwoche = (int.Parse(ausbwoche) + 1).ToString();

                pdfFormFields.SetField("Ausbildungsnachweis über Ausbildungswoche Nr", ausbwoche);
                pdfFormFields.SetField("LW Nr", kalwoche);
                pdfFormFields.SetField("vom", reports[0].Date);
                pdfFormFields.SetField("bis zum", reports[5] != null ? reports[5].Date : reports[4].Date);
                pdfFormFields.SetField("im", jahr);
                pdfFormFields.SetField("Name", fullname);
                pdfFormFields.SetField("Ausbildungsberuf", beruf);
                pdfFormFields.SetField("Ausbildende Abteilung", abt);
                pdfFormFields.SetField("Fachrichtung", fachrichtung);
                pdfFormFields.SetField("Ausbildende Firma", firmenname);
                pdfFormFields.SetField("Ausbilder", ausbilder);

                pdfFormFields.SetField("Ausgeführte Arbeiten Unterweisungen Unterricht in der Berufsschule uswRow1", getTextofRTF(reports[0].ReportText));
                pdfFormFields.SetField("Gesamt StundenRow1", reports[0].WorkHours);

                pdfFormFields.SetField("Ausgeführte Arbeiten Unterweisungen Unterricht in der Berufsschule uswRow2", getTextofRTF(reports[1].ReportText));
                pdfFormFields.SetField("Gesamt StundenRow2", reports[1].WorkHours);

                pdfFormFields.SetField("Ausgeführte Arbeiten Unterweisungen Unterricht in der Berufsschule uswRow3", getTextofRTF(reports[2].ReportText));
                pdfFormFields.SetField("Gesamt StundenRow3", reports[2].WorkHours);

                pdfFormFields.SetField("Ausgeführte Arbeiten Unterweisungen Unterricht in der Berufsschule uswRow4", getTextofRTF(reports[3].ReportText));
                pdfFormFields.SetField("Gesamt StundenRow4", reports[3].WorkHours);

                pdfFormFields.SetField("Ausgeführte Arbeiten Unterweisungen Unterricht in der Berufsschule uswRow5", getTextofRTF(reports[4].ReportText));
                pdfFormFields.SetField("Gesamt StundenRow5", reports[4].WorkHours);

                if (reports[5] != null)
                {
                    pdfFormFields.SetField("Ausgeführte Arbeiten Unterweisungen Unterricht in der Berufsschule uswRow6", (getTextofRTF(reports[5].ReportText)));
                    pdfFormFields.SetField("Gesamt StundenRow6", reports[5].WorkHours);
                }
                //42/40 Rows, 7*6 bei 6 tagen und 8*5 bei 5 tagen
                int x = 0;
                int currReport = -1;

                for (int i = 1; i <= (reports[5] != null ? 40 : 42); i++)
                {
                    if (x % (reports[5] != null ? 7 : 8) == 0)
                    {
                        x = 0;
                        currReport++;
                    }
                    if (reports[currReport] != null)
                    {
                        try
                        {
                            if (reports[currReport].SingleHours != null)
                                if (reports[currReport].SingleHours[x] != "0" && reports[currReport].SingleHours[x] != "0,0")
                                    pdfFormFields.SetField("Einzel stundenRow" + i.ToString(), reports[currReport].SingleHours[x]);
                        }
                        catch (Exception e) { MessageBox.Show(e.ToString(), "Fehler bei Einzelstunden-Verarbeitung"); }
                    }
                    x++;
                }

                pdfFormFields.SetField("BemerkungRow1", reports[0].Comments[0]);
                pdfFormFields.SetField("BemerkungRow2", reports[0].Comments[1]);
                pdfFormFields.SetField("Gesamt StundenFehl stunden", gesAwayHours.ToString());
                pdfFormFields.SetField("Gesamt StundenAus Bildungs stunden", gesWorkHours.ToString());
                pdfFormFields.SetField("Datum", "Dieser Bericht wurde am " + DateTime.Now.ToShortDateString() + " erstellt.");

                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
            }
            catch (Exception e)
            {
               
                MessageBox.Show(e.ToString(), "Fehler bei Bericht speichern");
                //MessageBox.Show("Die Datei konnte nicht geöffnet werden, da sie im Moment in Verwendung ist (bitte die Datei schließen)", "Fehler beim Schreiben der Datei.");
            }
            finally
            {
                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
            }
            Thread.Sleep(500);
            placeWatermark(newFile);

            return newFile;
        }

        internal static string getTextofRTF(string RTF)
        {
            RichTextBox rtb = new RichTextBox();
            rtb.Rtf = RTF;
            return rtb.Text;
        }

        public static byte[] ImageToByte(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        internal static void placeWatermark(string newFile)
        {
            Bitmap bitmap = global::Res.Properties.Resources.reportBig;
            ImageConverter converter = new ImageConverter();
            byte[] imgBytes = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));

            using (Stream inputPdfStream = new FileStream(newFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream inputImageStream = new MemoryStream(imgBytes))
            using (Stream outputPdfStream = new FileStream(newFile + ".tmp", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var reader = new PdfReader(inputPdfStream);

                var stamper = new PdfStamper(reader, outputPdfStream);
                var pdfContentByte = stamper.GetOverContent(1);

                //Logo hinzufügen
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);
                image.ScaleToFit(25, 25);
                image.SetAbsolutePosition(reader.GetPageSize(reader.GetPageN(1)).Width - 30, reader.GetPageSize(reader.GetPageN(1)).Height - 30);
                pdfContentByte.AddImage(image);

                //Text hinzufügen
                PdfContentByte pdfPageContents = stamper.GetUnderContent(1);
                pdfPageContents.BeginText();
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.COURIER, Encoding.ASCII.EncodingName, true);
                pdfPageContents.SetFontAndSize(baseFont, 7f);
                pdfPageContents.SetRGBColorFill(42, 127, 255);
                pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Erstellt mit der kostenlosen Software",
                                                     reader.GetPageSize(reader.GetPageN(1)).Width - 30,
                                                     reader.GetPageSize(reader.GetPageN(1)).Height - 15, 0);
                pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "von http://ausbildungsbericht.net",
                                                     reader.GetPageSize(reader.GetPageN(1)).Width - 30,
                                                     reader.GetPageSize(reader.GetPageN(1)).Height - 24, 0);
                pdfPageContents.EndText();
                //Stamper schließen                
                stamper.Close();
                //temp file löschen und neues file speichern
                File.Copy(newFile + ".tmp", newFile, true);
                File.Delete(newFile + ".tmp");
            }
            //Statistiken senden
            SendStats.SendPDFSaved();
        }

        internal static string SaveWeeklyReport(string ausbwoche, string kalwoche, string jahr, string fullname, string beruf, string fachrichtung, Report report, string firmenname, string abteilung, string ausbilder, string pdfdir)
        {
            DateTime[] dtss = DateTimeTools.getWeekAsDays(int.Parse(kalwoche), int.Parse(jahr));
            string pre = DataManager.LoadSettings()["FileNames"];
            pre = pre.Replace("{KW}", kalwoche);
            pre = pre.Replace("{AW}", ausbwoche);
            pre = pre.Replace("{WS}", dtss[0].ToShortDateString());
            pre = pre.Replace("{WE}", dtss[dtss.Length - 1].ToShortDateString());
            pre = pre.Replace("{VN}", DataManager.LoadBaseData()["vorname"]);
            pre = pre.Replace("{NN}", DataManager.LoadBaseData()["nachname"]);

            string newFile = pdfdir + @"\" + pre + ".pdf";

            if (!File.Exists(newFile))
            {
                Thread thr = new Thread(SendStats.SendDocumentCreated);
                thr.Start();
            };


            PdfReader pdfReader = new PdfReader(Resources.nachweis_woche.ToArray<byte>());

            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));

            AcroFields pdfFormFields = pdfStamper.AcroFields;

            pdfFormFields.SetField("Ausbildungswoche", ausbwoche);
            pdfFormFields.SetField("LaufendeWoche", kalwoche);
            pdfFormFields.SetField("WochenstartDatum", DateTimeTools.getWeekAsDays(int.Parse(kalwoche), int.Parse(jahr))[0].ToShortDateString());
            pdfFormFields.SetField("WochenendeDatum", DateTimeTools.getWeekAsDays(int.Parse(kalwoche), int.Parse(jahr))[6].ToShortDateString());
            pdfFormFields.SetField("Ausbildungsjahr", jahr);
            pdfFormFields.SetField("AzubiName", fullname);
            pdfFormFields.SetField("Abteilung", abteilung);
            pdfFormFields.SetField("Firmenname", firmenname);
            pdfFormFields.SetField("Ausbilder", ausbilder);
            pdfFormFields.SetField("Ausbildungsberuf", beruf);
            pdfFormFields.SetField("Fachrichtung", fachrichtung);
            pdfFormFields.SetField("Betrieb", getTextofRTF(report.CompanyText));
            pdfFormFields.SetField("Unterweisungen", getTextofRTF(report.InstructionsText));
            pdfFormFields.SetField("Berufsschule", getTextofRTF(report.SchoolText));
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
            placeWatermark(newFile);
            return newFile;
        }

    }
}

