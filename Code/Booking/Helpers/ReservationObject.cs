using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.Helpers
{
    public class ReservationObject
    {
        public int RoomId { get; set; }
        public int ReservationId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}