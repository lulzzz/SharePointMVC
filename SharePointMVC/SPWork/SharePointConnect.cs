using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using SharePointMVC.Models;
using System.Linq.Expressions;

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

                    retList.Add(new SpListViewModel
                    {
                        Title = list.Title,
                        Type = list.BaseType.ToString()
                    });
                }
            }

            return retList;
        }

        public List<Dictionary<string, string>> GetSpecificList(string listname)
        {
            var web = _context.Web;
            var list = _context.Web.Lists.GetByTitle(listname);

            FieldCollection fields = list.Fields;
            _context.Load(fields);
            _context.Load(web);
            _context.Load(list, l => l.Fields);
            _context.ExecuteQuery();

            //This gets the column names as they are stored in Sharepoint, for example Title_x20_N.
            var columns = new List<string>();
            //This list should store the real names example Title_x20_N will be Title.
            var columnsNameDisplay = new List<string>();
            foreach (var f in list.Fields)
            {
                if (f.FieldTypeKind == FieldType.Text
                    || f.FieldTypeKind == FieldType.Number
                    || f.FieldTypeKind == FieldType.MaxItems
                    || f.FieldTypeKind == FieldType.Currency
                    || f.FieldTypeKind == FieldType.DateTime
                    || f.FieldTypeKind == FieldType.User
                    || f.FieldTypeKind == FieldType.Note)
                {
                    columns.Add(f.InternalName);
                    columnsNameDisplay.Add(f.Title);
                }
            }



            List<Expression<Func<ListItemCollection, object>>> allIncludes = new List<Expression<Func<ListItemCollection, object>>>();

            foreach (var c in columns)
            {
                allIncludes.Add(items => items.Include(item => item[c]));
            }


            ListItemCollection listItems = list.GetItems(CamlQuery.CreateAllItemsQuery());
            _context.Load(listItems, allIncludes.ToArray());
            _context.ExecuteQuery();


            var doneList = new List<Dictionary<string, string>>();

            foreach (var listItem in listItems)
            {
                var dictionary = new Dictionary<string, string>();
                var i = 0;

                var x = listItem.ContentType;

                foreach (var col in columns)
                {
                    //typeof(Microsoft.SharePoint.Client.FieldUserValue)
                    //var myType = listItem[col].GetType();

                    //TODO: Check is the column is a user, then deliver user as lookupvalue to doneList.
                    //TODO: Try-catch on user otherwise string.

                    try
                    {
                        listItem[col] = (Microsoft.SharePoint.Client.FieldUserValue) listItem[col];
                        dictionary.Add(columnsNameDisplay[i], listItem[col] == null ? string.Empty : listItem[col].ToString());
                        i++;
                    }
                    catch (Exception up)
                    {
                        dictionary.Add(columnsNameDisplay[i], listItem[col] == null ? string.Empty : listItem[col].ToString());
                        i++;
                    }
                    

                    



                }
                doneList.Add(dictionary);
            }

            return doneList;
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