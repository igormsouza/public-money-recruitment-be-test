using System;
using System.Collections.Generic;

namespace VacationRental.Api.Models
{
    public class CalendarViewModel
    {
        public CalendarViewModel(int rentalId, DateTime start, int nights)
        {
            RentalId = rentalId;
            Start = start;
            Nights = nights;

            for (var i = 0; i < nights; i++)
            {
                var date = new CalendarDateViewModel
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewModel>()
                };

                Dates.Add(date);
            }
        }

        public int RentalId { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }

        private IList<CalendarDateViewModel> _dates;
        public IList<CalendarDateViewModel> Dates
        {
            get
            {
                _dates = _dates ?? new List<CalendarDateViewModel>();
                return _dates;
            }
            set { _dates = value; }
        }

        public void DataBind(IList<BookingUnit> bookings)
        {
            foreach (var date in Dates)
            {
                foreach (var booking in bookings)
                {
                    if (booking.Start <= date.Date && booking.EndDateWithoutPreparationTime > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingViewModel { Id = booking.IdBooking, Unit = booking.IdUnit });
                    }

                    if (booking.EndDateWithoutPreparationTime <= date.Date && booking.EndDate > date.Date)
                    {
                        date.PreparationTimes.Add(new CalendarBookingViewModel { Id = booking.IdBooking, Unit = booking.IdUnit });
                    }
                }
            }
        }
    }
}
