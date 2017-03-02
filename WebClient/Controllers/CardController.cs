using System;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class CardController : Controller
    {
        //
        // GET: /Card/
        public ActionResult Index(Guid id)
        {
            return View(id);
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
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
                FileService.SavePetImage(Id, fileName);
            }

            return View("List");
        }

        public ActionResult Edit(Guid id)
        {
            return View(new Pet { Id = id });
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
                FileService.SavePetImage(Id, fileName);
            }

            return View("List");
        }

        public ActionResult Delete(Guid id)
        {
            return View(new Pet { Id = id });
        }

        [HttpPost]
        public ActionResult Delete(string s)
        {
            return View("List");
        }
	}
}