using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IDictionary<int, BookingViewModel> _bookings;

        public BookingsController(
            IDictionary<int, RentalViewModel> rentals,
            IDictionary<int, BookingViewModel> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public BookingViewModel Get(int bookingId)
        {
            if (!_bookings.ContainsKey(bookingId))
                throw new ApplicationException("Booking not found");

            return _bookings[bookingId];
        }

        [HttpPost]
        public ResourceIdViewModel Post(BookingBindingModel model)
        {
            if (model.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");
            if (!_rentals.ContainsKey(model.RentalId))
                throw new ApplicationException("Rental not found");
            
            if (model.MinimunUnits <= 0)
                model.MinimunUnits = _rentals[model.RentalId].Units;

            if (!_rentals[model.RentalId].IsAvailableRange(model.MinimunUnits, model.Start, model.Nights))
                throw new ApplicationException("Not available");

            var key = new ResourceIdViewModel { Id = _bookings.Keys.Count + 1 };

            _rentals[model.RentalId].AddBookings(key.Id, model.MinimunUnits, model.Start, model.Nights);

            var bookings = _rentals[model.RentalId].BookingUnitsByRental.Where(o => o.IdBooking == key.Id).ToList();
            _bookings.Add(key.Id, new BookingViewModel(key.Id, model.RentalId, model.Start.Date, model.Nights, bookings));

            return key;
        }
    }
}