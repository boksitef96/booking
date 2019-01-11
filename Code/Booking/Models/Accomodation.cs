using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Booking.Models
{
    public class Accomodation
    {

        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public String Name { get; set; }

        public int CityIdNumber { get; set; }

        public City City { get; set; }

        public String Country { get; set; }

        public String Address { get; set; }

        public int AvailableRooms { get; set; }

        public String Description { get; set; }

        public int Stars { get; set; }

        public float Rating { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdate { get; set; }
      
        public ICollection<Room> Rooms { get; set; }

        public ICollection<ImageModels> Images { get; set; }

    }
}