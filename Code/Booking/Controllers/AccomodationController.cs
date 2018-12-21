using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking.Models;
using Booking.Controllers;


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
        
        [Authorize]
        [Route("add-new-accomodation", Name = "add_new_accomodation")]
        public ActionResult AddNewAccomodation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAccomodation(Accomodation accomodation)
        {
            var currentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            var user = _context.Users.Where(x => x.Email == currentUserName).FirstOrDefault();
            accomodation.User = user;
            accomodation.AvailableRooms = 0;
            accomodation.CreationDate = DateTime.Now;
            accomodation.LastUpdate = DateTime.Now;
            accomodation.Rating = 0;
            _context.Accomodations.Add(accomodation);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("show-all-accomoations", Name ="show_all_accomodations")]
        public ActionResult ShowAllAccomodations()
        {
            var accomodations = _context.Accomodations.ToList();
            return View(accomodations);
        }
    }
}