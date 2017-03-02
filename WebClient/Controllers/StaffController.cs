using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class StaffController : Controller
    {
        //
        // GET: /Staff/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View(new List<User>());
        }

        public ActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Create(Guid Id, HttpPostedFileBase ImageUrl)
        {
            Thread.Sleep(100);

            if (ImageUrl != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageUrl.FileName);
                var physicalPath = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                ImageUrl.SaveAs(physicalPath);
                FileService.SaveUserImage(Id, fileName);
            }

            return View("Index");
        }

        public ActionResult Edit(Guid id)
        {
            return View(new User { Id = id });
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, HttpPostedFileBase ImageUrl)
        {
            Thread.Sleep(100);

            if (ImageUrl != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageUrl.FileName);
                var physicalPath = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                ImageUrl.SaveAs(physicalPath);
                FileService.SaveUserImage(Id, fileName);
            }

            return View("Index");
        }

        public ActionResult Delete(Guid id)
        {
            return View(new User { Id = id });
        }

        [HttpPost]
        public ActionResult Delete(string s)
        {
            return View("Index");
        }
	}
}