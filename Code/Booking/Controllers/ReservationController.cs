using Booking.ControlerViewModels;
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
        private RedisController redisDB;

        public ReservationController()
        {
            _context = new ApplicationDbContext();
            redisDB = new RedisController();
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
            reservation.DateStart = DateTime.Now;
            reservation.DateEnd = DateTime.Now;

            ReservationDates reservationDates = new ReservationDates
            {
                Reservation = reservation,
                Dates = redisDB.GetReservationsDatesForRoom(roomId)
            };
            
            return View(reservationDates);
        }  

        [HttpPost]
        public ActionResult AddReservation(Reservation reservation, int roomId)
        {
            var currentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            var user = _context.Users.Where(x => x.Email == currentUserName).FirstOrDefault();

            var room = _context.Rooms.Where(r => r.Id == roomId).FirstOrDefault();

            reservation.Room = room;
            reservation.User = user;
            reservation.CreationDate = DateTime.Now;
            reservation.LastUpdate = DateTime.Now;

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            redisDB.AddReservation(reservation);

            return RedirectToAction("Index", "Home");
        }
    }
}