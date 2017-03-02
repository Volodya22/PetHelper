using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;

namespace WebClient.Controllers
{
    public class ServiceController : Controller
    {
        //
        // GET: /Service/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View(new Service());
        }

        [HttpPost]
        public ActionResult Create(string s)
        {
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(new Service { Id = id});
        }

        [HttpPost]
        public ActionResult Edit(string s)
        {
            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(new Service { Id = id });
        }

        [HttpPost]
        public ActionResult Delete(string s)
        {
            return View("Index");
        }
	}
}