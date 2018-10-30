using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SharePoint.Client;

namespace SharePointMVC.Models
{
    public class ListContentViewModel
    {
        public List<string> ColumnTitles{ get; set; }
        
        public ListItemCollection ColumnValues { get; set; }


    }
}