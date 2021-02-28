using System;
using System.Collections.Generic;
using System.Linq;

namespace VacationRental.Api.Models
{
    public class RentalViewModel : ICloneable
    {
        public int Id { get; set; }
        public int Units { get; set; }
        public int PreparationTimeIndays => Startup.PreparationTimeInDays;

        private IList<Unit> _unitList;
        public IList<Unit> UnitList
        {
            get
            {
                _unitList = _unitList ?? ListOfUnits();
                return _unitList;
            }
            set { _unitList = value; }
        }

        private IList<Unit> ListOfUnits()
        {
            var result = new List<Unit>();
            if (Units == 0)
                return result;

            for (int i = 0; i < Units; i++)
                result.Add(new Unit(i+1, Id));

            return result;
        }

        public IList<BookingUnit> BookingUnitsByRental => UnitList.SelectMany(o => o.BookingUnits).ToList();

        public bool IsAvailableRange(int desirableUnitsQuantity, DateTime startDate, int numberOfNights)
        {
            if (!BookingUnitsByRental.Any())
                return true;

            var count = 0;
            foreach (var item in BookingUnitsByRental)
            {
                if (item.IsAvailableRange(startDate, numberOfNights))
                    count++;
            }

            return count >= desirableUnitsQuantity;
        }

        public void AddBookings(int idBooking, int minimunUnits, DateTime startDate, int numberOfNights)
        {
            var count = 0;
            foreach (var unit in UnitList)
            {
                if (count <= minimunUnits)
                {
                    unit.BookingUnits.Add(new BookingUnit(idBooking, this.Id, unit.Id, startDate, numberOfNights));
                }

                count++;
            }
        }

        public void SetPreparationTimeToCheckOverlap(int newPreparationTimeInDays)
        {
            foreach (var item in BookingUnitsByRental)
                item.SetPreparationTimeToCheckOverlap(newPreparationTimeInDays);
        }

        public object Clone()
        {
            RentalViewModel newItem = (RentalViewModel)this.MemberwiseClone();
            //newItem._unitList = null;
            //foreach (var oldUnit in UnitList)
            //{
            //    newItem.UnitList.Add((Unit)oldUnit.Clone());
            //}

            return newItem;
        }
    }
}
