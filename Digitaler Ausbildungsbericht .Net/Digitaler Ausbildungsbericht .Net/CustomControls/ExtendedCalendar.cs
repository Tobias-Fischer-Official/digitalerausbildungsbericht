using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Digitaler_Ausbildungsbericht.Net
{
    public partial class ExtendedCalendar : UserControl
    {
        int actMonth;
        int actYear;
        public event EventHandler<DaySelectedEventArgs> onDaySelected;

        DateTimeButton _selectedDateTimeButton;
        Dictionary<DateTime, Color> _markedDates = new Dictionary<DateTime, Color>();

        DateTimeButton _lastEntered;
        Color _selectedDateColor = Color.SkyBlue;
        Color _lastEnteredColor;
        Color _colorBeforeSelection;

        DateTime _selectedDate;

        public ExtendedCalendar()
        {
            actMonth = DateTime.Now.Month;
            actYear = DateTime.Now.Year;

            InitializeComponent();

            lblMonthFwd.MouseEnter += new EventHandler(topMenuMouseEnter);
            lblMonthBack.MouseEnter += new EventHandler(topMenuMouseEnter);
            lblYearBack.MouseEnter += new EventHandler(topMenuMouseEnter);
            lblYearFwd.MouseEnter += new EventHandler(topMenuMouseEnter);

            lblMonthFwd.MouseLeave += new EventHandler(topMenuMouseLeave);
            lblMonthBack.MouseLeave += new EventHandler(topMenuMouseLeave);
            lblYearBack.MouseLeave += new EventHandler(topMenuMouseLeave);
            lblYearFwd.MouseLeave += new EventHandler(topMenuMouseLeave);

            lblMonthFwd.Click += new EventHandler(lblMonthFwd_Click);
            lblMonthBack.Click += new EventHandler(lblMonthBack_Click);
            lblYearBack.Click += new EventHandler(lblYearBack_Click);
            lblYearFwd.Click += new EventHandler(lblYearFwd_Click);

            FillMonth(actMonth, actYear);
        }

        internal void ColorDates()
        {
            foreach (KeyValuePair<DateTime, Color> kvp in _markedDates)
                if (grpControls.Controls.ContainsKey(kvp.Key.ToShortDateString().Replace(" 00:00:00", "")))
                {
                    (grpControls.Controls[kvp.Key.ToShortDateString().Replace(" 00:00:00", "")] as DateTimeButton).BackColor = kvp.Value;
                    (grpControls.Controls[kvp.Key.ToShortDateString().Replace(" 00:00:00", "")] as DateTimeButton).StandardColor = kvp.Value;
                }
        }

        internal void FillMonth(int Month, int Year)
        {
            grpControls.Controls.Clear();
            foreach (DateTimeButton dtb in GenerateDtbCollection(Month, Year))
                grpControls.Controls.Add(dtb);
            lblMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(actMonth);
            lblYear.Text = Year.ToString();
            ColorDates();
            grpControls.Dock = DockStyle.Fill;
        }

        void lblYearFwd_Click(object sender, EventArgs e)
        {
            this.actYear++;
            FillMonth(actMonth, actYear);
            lblYear.Text = actYear.ToString();
        }

        void lblYearBack_Click(object sender, EventArgs e)
        {
            this.actYear--;
            FillMonth(actMonth, actYear);
            lblYear.Text = actYear.ToString();
        }

        void lblMonthBack_Click(object sender, EventArgs e)
        {
            this.actMonth--;
            if (actMonth < 1)
            {
                actMonth = 12;
                this.actYear--;
                lblYear.Text = actYear.ToString();
            }
            FillMonth(actMonth, actYear);
            lblMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(actMonth);
        }

        void lblMonthFwd_Click(object sender, EventArgs e)
        {
            this.actMonth++;
            if (actMonth > 12)
            {
                actMonth = 1;
                this.actYear++;
                lblYear.Text = actYear.ToString();
            }

            FillMonth(actMonth, actYear);
            lblMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(actMonth);
        }

        void topMenuMouseEnter(object sender, EventArgs e)
        {
            (sender as Label).BackColor = Color.Blue;
        }

        void topMenuMouseLeave(object sender, EventArgs e)
        {
            (sender as Label).BackColor = Color.Transparent;
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                foreach (Control c in grpControls.Controls)
                    if (c.Name.Contains(".") && c.Name == value.ToShortDateString())
                    {
                        c.BackColor = SelectedDateColor;
                        _selectedDateTimeButton = (c as DateTimeButton);
                    }
                _selectedDate = value;
            }
        }

        internal Dictionary<DateTime, Color> MarkedDates
        {
            get { return _markedDates; }
        }

        public Color SelectedDateColor
        {
            get { return _selectedDateColor; }
            set { _selectedDateColor = value; }
        }

        private int[] GetActual4WeeksOfYear(DateTime dt)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            int w1 = cal.GetWeekOfYear(dt.AddDays(-dt.Day + 0), dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            int w2 = cal.GetWeekOfYear(dt.AddDays(-dt.Day + 7), dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            int w3 = cal.GetWeekOfYear(dt.AddDays(-dt.Day + 14), dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            int w4 = cal.GetWeekOfYear(dt.AddDays(-dt.Day + 21), dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            int w5 = cal.GetWeekOfYear(dt.AddDays(-dt.Day + 28), dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            int w6 = cal.GetWeekOfYear(dt.AddDays(-dt.Day + 35), dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            return new int[] { w1, w2, w3, w4, w5, w6 };
        }

        private int GetBlankTrailingDays(DateTime dt)
        {
            DateTime firstDay = dt.AddDays(-dt.Day);
            DayOfWeek firstDayName = firstDay.DayOfWeek;
            switch (firstDayName)
            {
                case DayOfWeek.Monday: return 1;
                case DayOfWeek.Tuesday: return 2;
                case DayOfWeek.Wednesday: return 3;
                case DayOfWeek.Thursday: return 4;
                case DayOfWeek.Friday: return 5;
                case DayOfWeek.Saturday: return 6;
                case DayOfWeek.Sunday: return 7;
                default: return 0;
            }
        }

        private List<DateTimeButton> GenerateDtbCollection(int Month, int Year)
        {
            DateTime MonthToFill = new DateTime(Year, Month, 1);
            int[] weeks = GetActual4WeeksOfYear(MonthToFill);
            int blank = GetBlankTrailingDays(MonthToFill);
            int monthlengt = DateTime.DaysInMonth(Year, Month);

            List<DateTimeButton> lbs = new List<DateTimeButton>();

            lbs.Add(new DateTimeButton());
            DateTimeButton mo = new DateTimeButton(); mo.Text = "Mo"; mo.Name = "WeekDay1";
            DateTimeButton di = new DateTimeButton(); di.Text = "Di"; di.Name = "WeekDay2";
            DateTimeButton mi = new DateTimeButton(); mi.Text = "Mi"; mi.Name = "WeekDay3";
            DateTimeButton Do = new DateTimeButton(); Do.Text = "Do"; Do.Name = "WeekDay4";
            DateTimeButton fr = new DateTimeButton(); fr.Text = "Fr"; fr.Name = "WeekDay5";
            DateTimeButton sa = new DateTimeButton(); sa.Text = "Sa"; sa.Name = "WeekDay6";
            DateTimeButton so = new DateTimeButton(); so.Text = "So"; so.Name = "WeekDay7";

            string MonthStr = Month.ToString();
            if (MonthStr.Length == 1)
                MonthStr = "0" + MonthStr;

            if (blank >= 7)
                blank = 0;

            for (int i = 0; i <= monthlengt - 1; i++)
            {
                DateTimeButton day = new DateTimeButton();
                day.Text = (i + 1).ToString();
                string name = (i + 1).ToString();
                name = name.Length == 1 ? "0" + name : name;
                day.Name = name + "." + MonthStr + "." + Year;
                lbs.Add(day);
            }

            lbs.InsertRange(1, new DateTimeButton[] { mo, di, mi, Do, fr, sa, so });

            for (int i = 0; i <= blank - 1; i++)
            {
                DateTimeButton blankL = new DateTimeButton();
                blankL.Text = " ";
                blankL.Name = "blank" + i.ToString();
                lbs.Insert(i + 8, blankL);
            }

            int z = 0;
            for (int i = 0; i <= 5; i++)
            {
                z += 8;
                DateTimeButton dtb = new DateTimeButton();
                dtb.Text = weeks[i].ToString();
                dtb.Name = "WeekNumber" + weeks[i].ToString();
                if (lbs.Count > z)
                    lbs.Insert(z, dtb);
            }

            int x = 3;
            int y = 8;

            foreach (DateTimeButton l in lbs)
            {
                l.Margin = new Padding(0);

                l.MinimumSize = new System.Drawing.Size(25, 15);
                l.MaximumSize = new System.Drawing.Size(25, 15);

                l.Location = new Point(x, y);
                l.Paint += new PaintEventHandler(l_Paint);

                l.TextAlign = ContentAlignment.MiddleCenter;
                l.BorderStyle = System.Windows.Forms.BorderStyle.None;
                x += 25;

                if (x == ((8 * 25) + 3))
                {
                    y += 15;
                    x = 3;
                }

                if (l.Name.Contains("."))
                {
                    DateTime dt = DateTime.Parse(l.Name);
                    if (_markedDates.ContainsKey(dt))
                    {
                        l.BackColor = _markedDates[dt];
                        l.StandardColor = _markedDates[dt];
                    }

                    if (dt.ToShortDateString() == DateTime.Now.ToShortDateString())
                        l.BorderStyle = BorderStyle.FixedSingle;

                    //if (l.Name == _selectedDate.ToShortDateString())
                    //    l.BackColor = _selectedDateColor;

                    l.MouseEnter += new EventHandler(l_Enter);
                    l.MouseLeave += new EventHandler(l_MouseLeave);
                    l.Click += new EventHandler(l_click);
                }

            }
            return lbs;
        }

        void l_click(object sender, EventArgs e)
        {
            DateTimeButton dtb = (sender as DateTimeButton);
            if (_selectedDateTimeButton != dtb)
            {
                _colorBeforeSelection = dtb.StandardColor;
                dtb.BackColor = Color.LightBlue;
                _selectedDate = DateTime.Parse(dtb.Name);

                if (_selectedDateTimeButton != null)
                    _selectedDateTimeButton.BackColor = _selectedDateTimeButton.StandardColor;

                _selectedDateTimeButton = dtb;

                if (onDaySelected != null)
                    onDaySelected(this, new DaySelectedEventArgs(DateTime.Parse((sender as DateTimeButton).Name)));

                SelectedDate = DateTime.Parse((sender as DateTimeButton).Name);
            }
        }

        void l_Paint(object sender, PaintEventArgs e)
        {
            DateTimeButton dtb = (sender as DateTimeButton);
            if (dtb.Name.StartsWith("WeekDay"))
                e.Graphics.DrawLine(Pens.Black, new Point(0, dtb.Height - 1), new Point(dtb.Width, dtb.Height - 1));
            else if (dtb.Name.StartsWith("WeekNumber"))
                dtb.BackColor = Color.LightGray;
        }

        void l_Enter(object sender, EventArgs e)
        {
            if (_selectedDateTimeButton != (sender as DateTimeButton))
            {
                _lastEntered = (sender as DateTimeButton);
                _lastEnteredColor = (sender as DateTimeButton).BackColor;
                (sender as DateTimeButton).BackColor = Color.Blue;
            }
        }

        void l_MouseLeave(object sender, EventArgs e)
        {
            if (_selectedDateTimeButton != (sender as DateTimeButton))
            {
                if ((sender as DateTimeButton) != _selectedDateTimeButton)
                    (sender as DateTimeButton).BackColor = _lastEntered.BackColor;
                _lastEntered.BackColor = _lastEnteredColor;
            }
        }

    }

    public partial class DaySelectedEventArgs : EventArgs
    {
        private DateTime _selectedDate;
        internal DateTime SelectedDate
        {
            get { return _selectedDate; }
        }

        internal DaySelectedEventArgs(DateTime date)
        {
            _selectedDate = date;
        }
    }
}