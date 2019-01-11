using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ControlerViewModels
{
    public class AccomodationCities
    {
        public Accomodation Accomodation { get; set; }
        public List<City> Cities { get; set; }
    }
}