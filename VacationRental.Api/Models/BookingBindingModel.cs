using System;

namespace VacationRental.Api.Models
{
    public class BookingBindingModel
    {
        public int RentalId { get; set; }

        public DateTime Start
        {
            get => _startIgnoreTime;
            set => _startIgnoreTime = value.Date;
        }

        private DateTime _startIgnoreTime;
        public int Nights { get; set; }

        /// <summary>
        /// If the value is less or equals than 0, it will pick the Rental's unit value
        /// </summary>
        public int MinimunUnits { get; set; }
    }
}
