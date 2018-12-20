using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.Models
{
    public class Room
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Accomodation Accomodation { get; set; }
        public int Capacity { get; set; }
        public float Price { get; set; }
        public String Descritpion { get; set; }
        public bool Wifi { get; set; }
        public bool TV { get; set; }
        public bool PetFriendly { get; set; }
        public bool Terrace { get; set; }
        public bool AC { get; set; }
        public bool Parking { get; set; }
    }
}