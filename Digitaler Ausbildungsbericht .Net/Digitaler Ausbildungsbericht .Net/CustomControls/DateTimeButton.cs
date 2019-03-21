
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitaler_Ausbildungsbericht.Net
{
    internal class DateTimeButton : Label
    {
        internal DateTimeButton()
        {
            this.Size = new Size(25, 15);
        }

        internal DateTimeButton(string DateTimeString, DateTime DayDateTime)
        {
            this.Size = new Size(30, 20);
            _dateTimeString = DateTimeString;
            _dayDateTime = DayDateTime;
        }

        private DateTime _dayDateTime;
        internal DateTime DayDateTime
        {
            get { return _dayDateTime; }
            set { _dayDateTime = value; }
        }

        private string _dateTimeString;
        internal string DateTimeString
        {
            get { return _dateTimeString; }
            set { _dateTimeString = value; }
        }

        private Color _hoverColor;
        internal Color HoverColor
        {
            get { return _hoverColor; }
            set { _hoverColor = value; }
        }

        private Color _standardColor;
        internal Color StandardColor
        {
            get { return _standardColor; }
            set { _standardColor = value; }
        }

        private Color _markedColor;
        internal Color MarkedColor
        {
            get { return _markedColor; }
            set { _markedColor = value; }
        }
    }
}
