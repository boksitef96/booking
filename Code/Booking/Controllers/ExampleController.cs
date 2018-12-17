using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Booking.Models;

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
            var examples = _context.Exapmles.ToList();
            var model = new List<Example>();
            model = examples;
            return View(model);
        }
    }
}