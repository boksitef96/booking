using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Booking.Models
{
    public class Reservation
    {
        public int Id { set; get; }
        public ApplicationUser User { get; set; }
     //   [ForeignKey("Room")]
        public Room Room { get; set; }
        public float Price { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public String Comment { get; set; }
    }
}