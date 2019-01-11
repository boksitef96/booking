using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class AccomodationDetails
    {
        public Accomodation Accomodation { get; set; }
        public City City { get; set; }
        public List<Room> Rooms { get; set; }
    }
}