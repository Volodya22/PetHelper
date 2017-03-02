using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;
using PetHelperApi.Models;

namespace WebClient.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Login()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public ActionResult Login(string flag)
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Register(string flag)
        {
            return RedirectToAction("Index", "Home");
        }
	}
}