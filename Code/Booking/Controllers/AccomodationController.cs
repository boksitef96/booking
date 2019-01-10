using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking.Models;
using Booking.Controllers;
using CSRedis;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Booking.Helpers;
using Booking.ControlerViewModels;

namespace Booking.Controllers
{
    public class AccomodationController : Controller
    {
        private ApplicationDbContext sqlDB;
        public RedisController redisDB;

        public AccomodationController()
        {
            sqlDB = new ApplicationDbContext();
            redisDB = new RedisController();
        }

        protected override void Dispose(bool disposing)
        {
            sqlDB.Dispose();
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
            Accomodation accomodation = new Accomodation();
            List<City> cities = sqlDB.Cities.ToList();
            AccomodationCities accomodationCities = new AccomodationCities
            {
                Accomodation = accomodation,
                Cities = cities
            };
            return View(accomodationCities);
        }

        [HttpPost]
        public ActionResult AddAccomodation(Accomodation accomodation)
        {
            var currentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            var user = sqlDB.Users.Where(x => x.Email == currentUserName).FirstOrDefault();
          
            accomodation.User = user;
            accomodation.AvailableRooms = 0;
            accomodation.CreationDate = DateTime.Now;
            accomodation.LastUpdate = DateTime.Now;
            accomodation.Rating = 0;
            var city = sqlDB.Cities.Where(x => x.Id == accomodation.CityIdNumber).FirstOrDefault();
            accomodation.City = city;
            sqlDB.Accomodations.Add(accomodation);
            if (accomodation.Stars > 3)
            {
                redisDB.AddAccomodationToList("accomodationByStars", accomodation);
            }
            redisDB.AddAccomodationToList("accomodationNewest", accomodation);
            sqlDB.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("show-all-accomoations/{type}", Name ="show_all_accomodations")]
        public ActionResult ShowAllAccomodations(string type)
        {
            //var accomodations = sqlDB.Accomodations.ToList();
            List<Accomodation> accomodations=null;
            if (type == "newest")
            {
                accomodations = redisDB.GetNewest();
            }
            if (type == "stars")
            {
                accomodations = redisDB.GetAboveAvergeStars();
            }
            if (type == "all")
            {
                accomodations = sqlDB.Accomodations.ToList();
            }

            return View(accomodations);
        }

        [HttpGet]
        [Authorize]
        [Route("show-user-accomoations", Name = "show_user_accomodations")]
        public ActionResult ShowAllAccomodationsByUser()
        {
            var currentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            var user = sqlDB.Users.Where(x => x.Email == currentUserName).FirstOrDefault();
           
            var accomodations = sqlDB.Accomodations.Where(a => a.User.Id == user.Id).ToList();
            return View(accomodations);
        }

        [Authorize]
        public ActionResult DeleteAccomodation(int id)
        {
            var accomodation = sqlDB.Accomodations.Where(a => a.Id == id).FirstOrDefault();

            sqlDB.Accomodations.Remove(accomodation);
            sqlDB.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public int GetLastAccomodation()
        {
            Accomodation accomodation = sqlDB.Accomodations.OrderByDescending(a => a.Id).FirstOrDefault();
            return accomodation.Id;
        }

        public int GetAccountIdByRoom(int roomId)
        {
            List<Accomodation> accomodations = sqlDB.Accomodations.ToList();
            List<Room> rooms = sqlDB.Rooms.ToList();
            Accomodation accomodation = new Accomodation();

            foreach (Room roomObj in rooms)
            {
                if (roomObj.Id == roomId)
                {
                    accomodation = roomObj.Accomodation;
                }
            }
            var accomodationId = accomodation.Id;

            return accomodationId;
        }
    }
}