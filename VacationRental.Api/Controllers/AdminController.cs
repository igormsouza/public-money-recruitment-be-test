using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IDictionary<int, BookingViewModel> _bookings;
        private readonly AdminViewModel _adminData;

        public AdminController(
            IDictionary<int, RentalViewModel> rentals,
            IDictionary<int, BookingViewModel> bookings,
            AdminViewModel adminData)
        {
            _rentals = rentals;
            _bookings = bookings;
            _adminData = adminData;
        }

        [HttpGet]
        [Route("{data}")]
        public AdminViewModel Get()
        {
            return _adminData;
        }

        [HttpPost]
        public AdminReArangeResultViewModel Post(AdminReArangeViewModel model)
        {
            if (model.PreparationTimeIndays < 0)
                throw new ApplicationException("Preparation Time cannot be less than zero");

            // Optional first case: change PreparationTimeIndays

            if (_adminData.PreparationTimeIndays < model.PreparationTimeIndays && _rentals.Any())
            {
                var cloneRentals = _rentals.Select(o => (RentalViewModel)o.Value.Clone()).ToList();
                foreach (var cloneRental in cloneRentals)
                {
                    cloneRental.SetPreparationTimeToCheckOverlap(model.PreparationTimeIndays);
                }

                // change item logic
                foreach (var cloneRental in cloneRentals)
                {
                    foreach (var unit in cloneRental.UnitList)
                    {
                        foreach (var booking in unit.BookingUnits)
                        {
                            var otherBookings = unit.BookingUnits.Where(o => o.IdBooking != booking.IdBooking).ToList();
                            foreach (var otherBooking in otherBookings)
                            {
                                if (!otherBooking.IsAvailableRange(booking.Start, booking.Nights))
                                {
                                    var msgError = $"It is not possible to change the Preparation TimePreparation hiegher than his current value{_adminData.PreparationTimeIndays} {Environment.NewLine}";
                                    msgError += $"we already have a time booked for the range - day:{booking.Start} and nights:{booking.Nights}. Overlaped with IdBooking:{otherBooking.IdBooking}, IdRental:{otherBooking.IdRental}, IdUnit:{otherBooking.IdUnit}";
                                    throw new ApplicationException(msgError);
                                }
                            }
                        }
                    }
                }
            }

            _adminData.PreparationTimeIndays = model.PreparationTimeIndays;

            // Optional second case: change Units
            if (model.RentalId > 0)
            {
                if (!_rentals.ContainsKey(model.RentalId))
                    throw new ApplicationException("Rental not found");
                if (model.Units <= 0)
                    throw new ApplicationException("Unit must be bigger than zero");

                if (_rentals[model.RentalId].Units != model.Units && _rentals.Any())
                {
                    // change item logic

                }
                _rentals[model.RentalId].Units = model.Units;
            }

            return new AdminReArangeResultViewModel() { Result = true };
        }
    }
}
