using System;
using System.Collections.Generic;
using System.Linq;

namespace VacationRental.Api.Models
{
    public class AdminViewModel
    {
        private int _preparationTimeIndays;
        public int PreparationTimeIndays {
            get { return _preparationTimeIndays; } 
            set
            {
                _preparationTimeIndays = value;
                Startup.SetPreparationTimeInDays(_preparationTimeIndays);
            } 
        }
    }
}
