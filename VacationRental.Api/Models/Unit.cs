using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacationRental.Api.Models
{
    public class Unit
    {
        public Unit(int id, int rentalId)
        {
            Id = id;
            RentalId = rentalId;
        }
        public int Id { get; set; }
        public int RentalId { get; set; }

        private IList<BookingUnit> _bookingUnit;
        public IList<BookingUnit> BookingUnit
        {
            get 
            {
                _bookingUnit = _bookingUnit ?? new List<BookingUnit>();
                return _bookingUnit;
            }
            set { _bookingUnit = value; }
        }
    }
}
