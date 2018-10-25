using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using SharePointMVC.Models;

namespace SharePointMVC.SPWork
{
    public class SharePointConnect
    {
        private string _url = "https://anchory.sharepoint.com/sites/Developersite";
        private readonly ClientContext _context;

        public SharePointConnect(string email, string password)
        {
            SecureString securePassword = ConvertPasswordToSecureString(password);
            _context = new ClientContext(_url)
            {
                Credentials = new SharePointOnlineCredentials(email, securePassword)
            };
        }


        public void DisposeSP()
        {
            _context.Dispose();
        }

        
        public bool SaveContext()
        {
            try
            {
                var web = _context.Web;
                _context.Load(web, w => w.Title);
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string GetWebTitle()
        {
                var web = _context.Web;
                _context.Load(web, w => w.Title);
                _context.ExecuteQuery();
                return web.Title;
        }

        //TODO: Get all lists and return them.
        public List<SpList> GetAllSharePointLists()
        {
            List<SpList> retList = new List<SpList>();

            var web = _context.Web;
            var lists = _context.Web.Lists;
            _context.Load(web, w => w.Title);
            _context.Load(lists);
            _context.ExecuteQuery();

            foreach (var list in lists)
            {
                if (list.BaseTemplate == 100)
                {
                    //TODO: Add all lists to an object that contains the title and maybe type?
                    retList.Add(new SpList
                    {
                        Title = list.Title,
                        Type = list.BaseType.ToString()
                    });
                }
            }

            return retList;
        }
        

        public SecureString ConvertPasswordToSecureString(string password)
        {
            SecureString securePassword = new SecureString();
            foreach (char c in password) securePassword.AppendChar(c);
            return securePassword;
        }

    }
}