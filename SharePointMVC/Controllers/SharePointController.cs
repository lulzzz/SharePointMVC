using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharePointMVC.SPWork;

namespace SharePointMVC.Controllers
{
    public class SharePointController : Controller
    {
        private SharePointConnect _sPC;

        // GET: SharePoint
        public ActionResult LoginIndex()
        {
            if (TempData["LoginFail"] != null)
            {
                ViewBag.LoginTxt = TempData["LoginFail"].ToString();
            }
            return View();
        }


        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            _sPC = new SharePointConnect(email,password);
            bool checkLogin = _sPC.SaveContext();

            if (checkLogin)
            {
                Session["SPC"] = _sPC;
                return RedirectToAction("Index");
            }
            TempData["LoginFail"] = "Failed to login";
            return RedirectToAction("LoginIndex");



        }

        [HttpPost]
        public ActionResult Logout()
        {
            _sPC = (SharePointConnect)Session["SPC"];
            _sPC.DisposeSP();
            Session.Remove("SPC");
            return RedirectToAction("LoginIndex");
        }

        
        public ActionResult Index()
        {
            if (Session["SPC"] == null)
            {
                return RedirectToAction("LoginIndex");
            }
            return View();
        }


        public ActionResult WebTitle()
        {
            if (Session["SPC"] == null)
            {
                return RedirectToAction("LoginIndex");
            }

            _sPC = (SharePointConnect)Session["SPC"];

            var title = _sPC.GetWebTitle();
            ViewBag.test = title;
            return View();
        }

        public ActionResult AllLists()
        {
            if (Session["SPC"] == null)
            {
                return RedirectToAction("LoginIndex");
            }

            _sPC = (SharePointConnect)Session["SPC"];
            
            //TODO: Get all lists here in a variable.
            return View();
        }
    }
}