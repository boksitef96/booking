using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Booking.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace Booking.Controllers
{
    public class ExampleController : Controller
    {
        private ApplicationDbContext _context;

        public ExampleController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult ShowExamples()
        {
            using (var client = new WebClient())
            {
                string createUrl = "https://api.openweathermap.org/data/2.5/weather?q=Nis,srb&APPID=8aa8eb55555024179f696bdbe89a97b4&";
                // client.Headers.Add("IH-Authorization", ihAuth);
                var url = new Uri(createUrl);
                try
                {
                    var response = client.DownloadString(url);
                }
                catch (Exception e)
                {
                    string error = e.Message;
                }
                var examples = _context.Exapmles.ToList();
                var model = new List<Example>();
                model = examples;
                return View(model);
            }
        }
    }
}