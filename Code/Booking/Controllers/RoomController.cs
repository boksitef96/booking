using Booking.Models;
using Booking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Booking.Controllers
{
    public class RoomController : Controller
    {
        private ApplicationDbContext _context;

        public RoomController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Room
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("add-new-room/{accomodationId}", Name ="add_new_room")]
        public ActionResult AddNewRoom(int accomodationId)
        {
            var room = new Room();
            var accomodation = _context.Accomodations.Where(a => a.Id == accomodationId).FirstOrDefault();
            room.Accomodation = accomodation;
            return View(room);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddRoom(Room room, int accomodationId)
        {
            var currentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            var user = _context.Users.Where(x => x.Email == currentUserName).FirstOrDefault();
            var accomodation = _context.Accomodations.Where(a => a.Id == accomodationId).FirstOrDefault();

            room.User = user;
            room.CreationDate = DateTime.Now;
            room.LastUpdate = DateTime.Now;
            room.Accomodation = accomodation;

            accomodation.AvailableRooms++;
            //accomodation.Rooms.Add(room);

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("show-rooms/{accomodationId}/{type}", Name = "show_rooms")]
        public ActionResult ShowAllRoomsForAccomodation(int accomodationId, bool type)
        {
            var accomodation = _context.Accomodations.Where(a => a.Id == accomodationId).FirstOrDefault();
            var rooms = new List<Room>();
            rooms = _context.Rooms.Where(r => r.Accomodation.Id == accomodationId).ToList();
            City city = _context.Cities.Where(c => c.Id == accomodation.CityIdNumber).FirstOrDefault();
            AccomodationDetails accomomodationDetails = new AccomodationDetails
            {
                Accomodation = accomodation,
                Rooms = rooms,
                City=city
            };
            return View("ShowAllRoomsForAccomodation", accomomodationDetails);
        }
    }
}