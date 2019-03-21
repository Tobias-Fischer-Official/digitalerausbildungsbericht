using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digitaler_Ausbildungsbericht.Net
{
    public class ReportedWeek
    {
        private bool _delivered;
        public bool Delivered
        {
            get { return _delivered; }
            set { _delivered = value; }
        }

        private int _week;
        public int Week
        {
            get { return _week; }
            set { _week = value; }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public ReportedWeek()
        {

        }

        public ReportedWeek(bool delivered, int week, int year)
        {
            _delivered = delivered;
            _week = week;
            _year = year;
        }
    }
}
