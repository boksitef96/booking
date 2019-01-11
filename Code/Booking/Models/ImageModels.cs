using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Booking.Models
{
    public class ImageModels
    {
        public int Id { get; set; }
        [DisplayName("Izaberi sliku")]
        public string ImagePath { get; set; }
        [DisplayName("Naziv slike")]
        public string Title { get; set; }
        public Accomodation Accomodation { get; set; }
        
    }
}