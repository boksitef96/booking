using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booking.Controllers
{
    public class ReservationController : Controller
    {
        private ApplicationDbContext _context;

        public ReservationController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        [Route("add-new-reservation")]
        public ActionResult AddNewReservation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddReservation(Reservation reservation)
        {
            reservation.CreationDate = DateTime.Now;
            reservation.LastUpdate = DateTime.Now;

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


    }
}