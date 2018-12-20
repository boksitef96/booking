using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking.Models;

namespace Booking.Controllers
{
    public class AccomodationController : Controller
    {
        private ApplicationDbContext _context;

        public AccomodationController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Accomodation
        public ActionResult Index()
        {
            return View();
        }
        
        [Route("add-new-accomodation")]
        public ActionResult AddNewAccomodation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAccomodation(Accomodation accomodation)
        {
            accomodation.AvailableRooms = 0;
            accomodation.CreationDate = DateTime.Now;
            accomodation.LastUpdate = DateTime.Now;
            accomodation.Rating = 0;
            _context.Accomodations.Add(accomodation);
            _context.SaveChanges();

            return RedirectToAction("Index","Home");
        }
    }
}