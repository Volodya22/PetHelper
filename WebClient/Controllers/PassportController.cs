using System;
using System.Web.Mvc;

namespace WebClient.Controllers
{
    public class PassportController : Controller
    {
        //
        // GET: /Passport/
        public ActionResult Index(Guid id)
        {
            return View(id);
        }

        public ActionResult List()
        {
            return View();
        }
	}
}