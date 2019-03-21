using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitaler_Ausbildungsbericht.Net.CustomControls
{
    public class CustomWebControl : WebBrowser
    {
        public CustomWebControl()
        {
            this.DocumentCompleted += CustomWebControl_DocumentCompleted;
        }

        //Der folgende Coabschnitt verhindert, dass Links in einem Popup des Internetexplorer geöffnet werden,
        //und sorgt dafür dass stattdessen der Standartbrowser des Systems geöffnet wird.
        void CustomWebControl_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlElementCollection links = this.Document.Links;
            foreach (HtmlElement link in links)
            {
                link.DetachEventHandler("onclick", new EventHandler(LinkClicked));
                link.AttachEventHandler("onclick", new EventHandler(LinkClicked));
            }
        }

        private void LinkClicked(object sender, EventArgs e)
        {
            try
            {
                HtmlElement link = this.Document.ActiveElement;
                Process.Start(link.GetAttribute("href"));
            }
            catch { }
        }
    }
}
