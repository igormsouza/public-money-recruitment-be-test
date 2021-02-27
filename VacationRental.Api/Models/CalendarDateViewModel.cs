using System;
using System.Collections.Generic;

namespace VacationRental.Api.Models
{
    public class CalendarDateViewModel
    {
        public DateTime Date { get; set; }

        private IList<CalendarBookingViewModel> _bookings;
        public IList<CalendarBookingViewModel> Bookings
        {
            get
            {
                _bookings = _bookings ?? new List<CalendarBookingViewModel>();
                return _bookings;
            }
            set { _bookings = value; }
        }

        private IList<CalendarBookingViewModel> _preparationTimes;
        public IList<CalendarBookingViewModel> PreparationTimes
        {
            get
            {
                _preparationTimes = _preparationTimes ?? new List<CalendarBookingViewModel>();
                return _preparationTimes;
            }
            set { _preparationTimes = value; }
        }
    }
}
