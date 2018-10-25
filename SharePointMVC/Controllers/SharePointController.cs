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
        public ActionResult Login(string email, string password)
        {
            /*
            if (email == "")
            {
                TempData["LoginFail"] = "Failed to login";
                return RedirectToAction("LoginIndex");
            }
            */

            _sPC = new SharePointConnect(email,password);
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

            WebTitleViewModel viewModel = new WebTitleViewModel {WebTitle = _sPC.GetWebTitle()};
            
            return View(viewModel);
        }

        public ActionResult AllLists()
        {
            if (!CheckSession())
            {
                return RedirectToAction("LoginIndex");
            }

            List<SpList> model = _sPC.GetAllSharePointLists();
            
            return View(model);
        }
    }
}