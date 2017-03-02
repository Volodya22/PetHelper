using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;

namespace WebClient.Controllers
{
    public class VisitController : Controller
    {
        //
        // GET: /Visit/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string s)
        {
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(new ServiceForPet { Id = id });
        }

        [HttpPost]
        public ActionResult Edit(string s)
        {
            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(new ServiceForPet { Id = id });
        }

        [HttpPost]
        public ActionResult Delete(string s)
        {
            return View("Index");
        }
	}
}