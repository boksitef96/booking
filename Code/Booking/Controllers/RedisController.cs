using Booking.Helpers;
using Booking.Models;
using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Booking.Controllers
{
    public class RedisController : Controller
    {
        private ApplicationDbContext _context;
        RedisClient redis = new RedisClient("localhost", 6379);
        JavaScriptSerializer jsonS = new JavaScriptSerializer();

        public RedisController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Redis
        public ActionResult Index()
        {
            return View();
        }

        public void InitializeRedis()
        {
            InitializeRedisByStars();
            InitializeRedisNewest();
        }

        public void InitializeRedisByStars()
        {
            List<Accomodation> accomodations;
            accomodations = _context.Accomodations.Where(a => a.Stars > 3).ToList();
            int index = 0;
            redis.Del("accomodationByStars");
            foreach (Accomodation acomodation in accomodations)
            {
                if (index > 9) break;
                redis.LPush("accomodationByStars", jsonS.Serialize(acomodation));
                index++;
            }
        }

        public void InitializeRedisNewest()
        {
            List<Accomodation> accomodations;
            accomodations = _context.Accomodations.OrderByDescending(a=>a.Id).Take(10).ToList();
            accomodations.Reverse();
            redis.Del("accomodationNewest");
            foreach (Accomodation acomodation in accomodations)
            {
                redis.LPush("accomodationNewest", jsonS.Serialize(acomodation));
            }
        }

        public List<Accomodation> GetAboveAvergeStars()
        {
            List<Accomodation> acc = new List<Accomodation>();
            var a = redis.LRange("accomodationByStars", 0, 9);
            foreach (var ac in a)
            { 
                acc.Add(jsonS.Deserialize<Accomodation>(ac));
                redis.LTrim("accomodationByStars", 0, 9);
            }
            return acc;
        }

        public List<Accomodation> GetNewest()
        {
            List<Accomodation> acc = new List<Accomodation>();
            var a = redis.LRange("accomodationNewest", 0, 9);
            foreach (var ac in a)
            {
                acc.Add(jsonS.Deserialize<Accomodation>(ac));
                redis.LTrim("accomodationNewest", 0, 9);
            }
            return acc;
        }

        public void AddAccomodationToList(string key,Accomodation accomodation)
        {
            redis.LPush(key, jsonS.Serialize(accomodation));
        }

        public void AddReservation(Reservation reservation)
        {
            string key = "room:" + reservation.Room.Id.ToString() + ":reservations";
            var response = redis.Get(key);
            if (response != null)
            {
                var reservations = jsonS.Deserialize<List<ReservationsList>>(response);
                ReservationsList newReservation =  new ReservationsList
                {
                    RoomId = reservation.Room.Id,
                    ReservationId = reservation.Id,
                    DateStart = reservation.DateStart,
                    DateEnd = reservation.DateEnd
                };
                reservations.Add(newReservation);
                redis.Set(key, jsonS.Serialize(reservations));

            }
            else
            {
                var reservations = new List<Object>();
                ReservationsList newReservation = new ReservationsList
                {
                    RoomId = reservation.Room.Id,
                    ReservationId = reservation.Id,
                    DateStart = reservation.DateStart,
                    DateEnd = reservation.DateEnd
                };
                reservations.Add(newReservation);
                redis.Set(key, jsonS.Serialize(reservations));
            }
        }

        public List<DateTime> GetReservationsDatesForRoom(int roomId)
        {
            List<DateTime> dates = new List<DateTime>();
            var reservationsJson = redis.Get("room:" + roomId + ":reservations");
            var reservations = jsonS.Deserialize<List<Object>>(reservationsJson);
            foreach (var reservation in reservations)
            {
                //var properties = reservationObj;
                //DateTime startDate = reservation.DateStart;
                //DateTime endDate = reservation.DateEnd;
                //DateTime currentDate = startDate;
                //while (currentDate <= endDate)
                //{
                //    dates.Add(currentDate);

                //    currentDate = currentDate.AddDays(1);
                //}

            }
            return dates;
        }
    }
}