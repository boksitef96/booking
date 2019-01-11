using Booking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class ImageObject
    {
        public int Id { get; set; }
        [DisplayName("Izaberi sliku")]
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public Accomodation Accomodation { get; set; }
    }
}