using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digitaler_Ausbildungsbericht.Net
{
    internal class SelectedDayChangedEventArgs : EventArgs
    {
        private DateTime _selectedDate;
        internal DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; }
        }

        internal SelectedDayChangedEventArgs(DateTime dt)
        {
            _selectedDate = dt;
        }
    }
}
