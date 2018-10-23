using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SharePointMVC.Controllers
{
    public class SharePointController : Controller
    {
        // GET: SharePoint
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public void Login(string password, string email)
        {
            var x = password;
        }
    }
}