using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digitaler_Ausbildungsbericht.Net
{
    public class Report
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        //Test
        private string _date;
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private string _reportText;
        public string ReportText
        {
            get { return _reportText; }
            set { _reportText = value; }
        }

        private string _awayHours;
        public string AwayHours
        {
            get { return _awayHours; }
            set { _awayHours = value; }
        }

        private string _workHours;
        public string WorkHours
        {
            get { return _workHours; }
            set { _workHours = value; }
        }

        private string _division;
        public string Division
        {
            get { return _division; }
            set { _division = value; }
        }

        private string _ill;
        public string Ill
        {
            get { return _ill; }
            set { _ill = value; }
        }

        private string _holyday;
        public string Holyday
        {
            get { return _holyday; }
            set { _holyday = value; }
        }

        private string _delivered;
        public string Delivered
        {
            get { return _delivered; }
            set { _delivered = value; }
        }

        private string _appWeekNumber;
        public string AppWeekNumber
        {
            get { return _appWeekNumber; }
            set { _appWeekNumber = value; }
        }

        private string _companyText;
        public string CompanyText
        {
            get { return _companyText; }
            set { _companyText = value; }
        }

        private string _schoolText;
        public string SchoolText
        {
            get { return _schoolText; }
            set { _schoolText = value; }
        }

        private string _instructionsText;
        public string InstructionsText
        {
            get { return _instructionsText; }
            set { _instructionsText = value; }
        }

        private string _year;
        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private string _yearWeekNumber;
        public string YearWeekNumber
        {
            get { return _yearWeekNumber; }
            set { _yearWeekNumber = value; }
        }

        private string[] _singleHours;
        public string[] SingleHours
        {
            get { return _singleHours; }
            set { _singleHours = value; }
        }

        private string[] _comments;
        public string[] Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public bool IsMonday
        {
            get
            {
                DateTime dt = DateTime.Parse(this._date);
                if (dt.DayOfWeek == DayOfWeek.Monday)
                    return true;
                return false;
            }
        }

        public Report()
        {

        }

        public Report(string id, string date, string reportText, string awayHours, string workHours, string division, string ill, string holyday, string delivered, string[] singleHours, string[] comments)
        {
            _id = id;
            _date = date;
            _reportText = reportText;
            _awayHours = awayHours;
            _workHours = workHours;
            _division = division;
            _ill = ill;
            _holyday = holyday;
            _delivered = delivered;
            _singleHours = singleHours;
            _comments = comments;
        }

        public Report(string id, string appWeekNumber, string yearWeekNumber, string year, string companyText, string schoolText, string instructionsText, string delivered)
        {
            _id = id;
            _appWeekNumber = appWeekNumber;
            _companyText = companyText;
            _schoolText = schoolText;
            _instructionsText = instructionsText;
            _delivered = delivered;
            _year = year;
            _yearWeekNumber = yearWeekNumber;
        }


    }
}
