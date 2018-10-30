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
        private readonly string _url;
        private readonly ClientContext _context;

        public SharePointConnect(string email, string password, string url)
        {
            _url = url;
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
        public List<SpListViewModel> GetAllSharePointLists()
        {
            List<SpListViewModel> retList = new List<SpListViewModel>();

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
                    retList.Add(new SpListViewModel
                    {
                        Title = list.Title,
                        Type = list.BaseType.ToString()
                    });
                }
            }

            return retList;
        }

        public void GetSpecificList(string listname)
        {
            var web = _context.Web;
            var list = _context.Web.Lists.GetByTitle(listname);
            var listValues = list.GetItems(CamlQuery.CreateAllItemsQuery());
            var user = _context.Web.CurrentUser;

            _context.Load(web);
            _context.Load(list, l => l.Fields);
            _context.Load(listValues, l => l.Include(i => i.FieldValuesAsText));
            _context.ExecuteQuery();

            ListContentViewModel retList = new ListContentViewModel();
            List<string> stringColumnList = new List<string>();

            //TODO: Get list items and map them.
            foreach (var columTitle in list.Fields)
            {
                stringColumnList.Add(columTitle.Title);
            }

            retList.ColumnTitles = stringColumnList;


            var x = retList;

            foreach (var columnValue in listValues )
            {
                foreach (var columnName in retList.ColumnTitles)
                {
                    var y = columnValue[columnName].ToString();
                }
                
            }
        }

        public List<ListOneModel> GetListOneTESTING(string listname)
        {
            var web = _context.Web;
            var oList = _context.Web.Lists.GetByTitle(listname).GetItems(CamlQuery.CreateAllItemsQuery());
            _context.Load(web);
            _context.Load(oList, items => items.Include(
                item => item["Title"],
                item => item["Number"],
                item => item["Texty"]));
            
            _context.ExecuteQuery();

            List<ListOneModel> retList = new List<ListOneModel>();
            foreach (var item in oList)
            {
                retList.Add(new ListOneModel
                {
                    Title = item["Title"].ToString(),
                    Number = item["Number"].ToString(),
                    Texty = item["Title"].ToString()
                });
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