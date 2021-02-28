using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacationRental.Api.Models
{
    public class Unit : ICloneable
    {
        public Unit(int id, int rentalId)
        {
            Id = id;
            RentalId = rentalId;
        }
        public int Id { get; set; }
        public int RentalId { get; set; }

        private IList<BookingUnit> _bookingUnits;
        public IList<BookingUnit> BookingUnits
        {
            get 
            {
                _bookingUnits = _bookingUnits ?? new List<BookingUnit>();
                return _bookingUnits;
            }
            set { _bookingUnits = value; }
        }

        public object Clone()
        {
            Unit newItem = (Unit)this.MemberwiseClone();
            //newItem._bookingUnits = null;
            //foreach (var oldBooking in BookingUnits)
            //{
            //    newItem.BookingUnits.Add((BookingUnit)oldBooking.Clone());
            //}

            return newItem;
        }
    }
}
