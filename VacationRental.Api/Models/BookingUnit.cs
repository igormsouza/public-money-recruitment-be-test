using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacationRental.Api.Models
{
    public class BookingUnit
    {
        public BookingUnit(int idBooking, int idRental, int idUnit, DateTime start, int nights)
        {
            IdBooking = idBooking;
            IdRental = idRental;
            IdUnit = idUnit;
            Start = start;
            Nights = nights;
        }
        public int IdBooking { get; set; }
        public int IdRental { get; set; }
        public int IdUnit { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
        public DateTime EndDate => Start.AddDays(Nights + Startup.PreparationTimeInDays);
        public DateTime EndDateWithoutPreparationTime => Start.AddDays(Nights);

        public bool IsAvailableRange(DateTime startDate, int numberOfNights)
        {
            DateTime checkDate = startDate.AddDays(numberOfNights); 
            var isBooked = checkDate.Date >= Start.Date && checkDate.Date < EndDate.Date;
            return !isBooked;
        }
    }
}
