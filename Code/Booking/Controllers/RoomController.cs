using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Route("add-new-room")]
        public ActionResult AddNewRoom()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRoom(Room room)
        {
            room.CreationDate = DateTime.Now;
            room.LastUpdate = DateTime.Now;

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}