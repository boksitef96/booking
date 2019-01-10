using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking.Models;
using Booking.ViewModels;

namespace Booking.Controllers
{
    public class ImageController : Controller
    {
        private ApplicationDbContext sqlDB;

        public ImageController()
        {
            sqlDB = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            sqlDB.Dispose();
        }

        [Authorize]
        [Route("add-new-image/{accomodationId}", Name = "add_new_image")]
        public ActionResult AddNewImage(int accomodationId)
        {
            ImageObject image = new ImageObject();
            var accomodation = sqlDB.Accomodations.Where(a => a.Id == accomodationId).FirstOrDefault();
            image.Accomodation = accomodation;

            return View(image);
        }

        [Authorize]
        [Route("add-image", Name = "add_image")]
        public ActionResult AddImage(ImageObject imageObject, int accomodationId)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageObject.ImageFile.FileName);
            string extension = Path.GetExtension(imageObject.ImageFile.FileName);
            fileName = fileName + "" + extension;
            imageObject.ImagePath = "~/Content/images/accomodation/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/images/accomodation/"), fileName);
            imageObject.ImageFile.SaveAs(fileName);

            ImageModels image = new ImageModels();
            image.ImagePath = imageObject.ImagePath;
            image.Title = imageObject.Title;

            var accomodation = sqlDB.Accomodations.Where(a => a.Id == accomodationId).FirstOrDefault();
            image.Accomodation = accomodation;
            
            sqlDB.Images.Add(image);
            sqlDB.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [Route("show-images/{accomodationId}", Name = "show_images")]
        public ActionResult ShowImages(int accomodationId)
        {
            List<ImageModels> images = sqlDB.Images.Where(a => a.Accomodation.Id == accomodationId).ToList();
           
            return View(images);
        }
    }
}