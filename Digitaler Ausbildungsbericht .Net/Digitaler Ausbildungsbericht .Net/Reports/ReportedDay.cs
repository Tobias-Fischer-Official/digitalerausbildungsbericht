using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digitaler_Ausbildungsbericht.Net
{
    public class ReportedDay
    {
        private bool _delivered;
        public bool Delivered
        {
            get { return _delivered; }
            set { _delivered = value; }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public ReportedDay()
        {

        }

        public ReportedDay(bool delivered, DateTime date)
        {
            _delivered = delivered;
            _date = date;
        }
    }
}
