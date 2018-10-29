using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharePointMVC.Models;
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

        public bool CheckSession()
        {
            if (Session["SPC"] == null)
            {
                return false;
            }

            _sPC = (SharePointConnect)Session["SPC"];
            return true;
        }


        [HttpPost]
        public ActionResult Login(string email, string password, string url)
        {

            _sPC = new SharePointConnect(email, password, url);
            if (!_sPC.SaveContext())
            {
                TempData["LoginFail"] = "Failed to login";
                return RedirectToAction("LoginIndex");
            }
            Session["SPC"] = _sPC;
            return RedirectToAction("Index");
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
            if (!CheckSession())
            {
                return RedirectToAction("LoginIndex");
            }
            return View();
        }



        public ActionResult WebTitle()
        {

            if (!CheckSession())
            {
                return RedirectToAction("LoginIndex");
            }

            WebTitleViewModel viewModel = new WebTitleViewModel { WebTitle = _sPC.GetWebTitle() };

            return View(viewModel);
        }

        public ActionResult AllLists()
        {
            if (!CheckSession())
            {
                return RedirectToAction("LoginIndex");
            }

            List<SpListViewModel> model = _sPC.GetAllSharePointLists();

            return View(model);
        }

        public ActionResult ListDetails()
        {

            if (!CheckSession())
            {
                return RedirectToAction("LoginIndex");
            }

            /*
             * TODO: 1. Change "Listone" to a parameter from ListDetails.
             * TODO: 2. Store the data from the list in a viewmodel and present it in the View(viewmodel).
             * TODO: 3. Make a button in /SharePointController/Index that calls this method on the server.
             */

            _sPC.GetSpecificList("Listone");

            return View();
        }
    }
}