using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacationRental.Api.Models
{
    public class BookingUnit : ICloneable
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

        // optional change preparation Time
        public void SetPreparationTimeToCheckOverlap(int newPreparationTimeInDays)
        {
            _preparationTimeInDays = newPreparationTimeInDays;
        }

        private int _preparationTimeInDays = Startup.PreparationTimeInDays;
        public int PreparationTimeInDays { 
            get
            {
                return _preparationTimeInDays != Startup.PreparationTimeInDays ? _preparationTimeInDays : _preparationTimeInDays;
            }
        }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
        public DateTime EndDate => Start.AddDays(Nights + PreparationTimeInDays);
        public DateTime EndDateWithoutPreparationTime => Start.AddDays(Nights);

        public bool IsAvailableRange(DateTime startDate, int numberOfNights)
        {
            DateTime checkDate = startDate.AddDays(numberOfNights+ PreparationTimeInDays);
            var isBooked = checkDate.Date >= Start.Date && checkDate.Date < EndDate.Date;
            return !isBooked;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
