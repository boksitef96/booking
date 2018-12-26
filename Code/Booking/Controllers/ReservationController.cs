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

        [Route("add-new-reservation/{roomId}", Name = "add_new_reservation")]
        public ActionResult AddNewReservation(int roomId)
        {
            var reservation = new Reservation();

            var room = _context.Rooms.Where(r => r.Id == roomId).FirstOrDefault();
            reservation.Room = room;
            return View(reservation);
        }  

        [HttpPost]
        public ActionResult AddReservation(Reservation reservation)
        {
            var currentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            var user = _context.Users.Where(x => x.Email == currentUserName).FirstOrDefault();

            reservation.User = user;
            reservation.CreationDate = DateTime.Now;
            reservation.LastUpdate = DateTime.Now;

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}