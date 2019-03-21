using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Digitaler_Ausbildungsbericht.Net
{
    internal static class DateTimeTools
    {
        internal static int GetWeekOfYear(DateTime dt)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date = dt;
            Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        internal static int GetActAppYear(DateTime appStart, DateTime selectedDay)
        {
            TimeSpan ts = GetWorkWeekStartAndEnd(selectedDay)[0] - GetWorkWeekStartAndEnd(appStart)[0];
            double returns = ts.TotalDays / 364;
            return Convert.ToInt32(Math.Ceiling(returns));
        }

        internal static int GetActAppWeek(DateTime appStart, DateTime selectedDay)
        {
            TimeSpan ts = GetWorkWeekStartAndEnd(selectedDay)[0] - GetWorkWeekStartAndEnd(appStart)[0];
            int returns = Convert.ToInt32(ts.TotalDays / 7);
            return returns;
        }

        internal static DateTime[] GetWorkWeekStartAndEnd(DateTime dt)
        {
            DateTime[] dta = new DateTime[2];
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dta[0] = dt;
                    dta[1] = dt.AddDays(4);
                    break;
                case DayOfWeek.Tuesday:
                    dta[0] = dt.AddDays(-1);
                    dta[1] = dt.AddDays(3);
                    break;
                case DayOfWeek.Wednesday:
                    dta[0] = dt.AddDays(-2);
                    dta[1] = dt.AddDays(2);
                    break;
                case DayOfWeek.Thursday:
                    dta[0] = dt.AddDays(-3);
                    dta[1] = dt.AddDays(1);
                    break;
                case DayOfWeek.Friday:
                    dta[0] = dt.AddDays(-4);
                    dta[1] = dt;
                    break;
                case DayOfWeek.Saturday:
                    dta[0] = dt.AddDays(-5);
                    dta[1] = dt.AddDays(-1);
                    break;
                case DayOfWeek.Sunday:
                    dta[0] = dt.AddDays(-6);
                    dta[1] = dt.AddDays(-2);
                    break;
            }
            return dta;
        }

        internal static DateTime[] getWeekAsDays(int WeekNumber, int Year)
        {
            List<DateTime> dts = new List<DateTime>();
            //Aktuelle Woche raussuchen, alle Wochen zusammenrechnen und 1 abziehen = Aktuelle Woche
            DateTime dt = new DateTime(Year, 1, 1).AddDays(WeekNumber * 7).AddDays(-7);
            //Jweils 1 Tag zur Liste hinzufügen
            for (int i = 1; i <= 7; i++)
                dts.Add(dt.AddDays(-(int)dt.DayOfWeek + i));
            //Woche zurückgeben
            return dts.ToArray();
        }
    }
}
