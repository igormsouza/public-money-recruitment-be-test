using System;
using System.Collections.Generic;

namespace VacationRental.Api.Models
{
    public class BookingViewModel
    {
        public BookingViewModel(int id, int rentalId, DateTime start, int nights, IList<BookingUnit> bookingUnits)
        {
            Id = id;
            RentalId = rentalId;
            Start = start;
            Nights = nights;
            BookingUnits = bookingUnits;
        }

        public int Id { get; set; }
        public int RentalId { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
        public IList<BookingUnit> BookingUnits { get; set; }
    }
}
