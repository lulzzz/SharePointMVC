using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Client;

namespace SharePointMVC.Models
{
    public class IncidentModel
    {
        public string Title { get; set; }

        public string ReportedBy { get; set; } 
        

        public DateTime DateReported { get; set; }

        public string DepartmentOccurrence { get; set; }

        public DateTime IncidentDate { get; set; }

        public DateTime IncidentClosed { get; set; }
        
        //string or dropdown?
        public string TypeOfError { get; set; }

        //string or dropdown?
        public string TypeOfIncident { get; set; }

        //string or dropdown?
        public string Classification { get; set; }

        //string or dropdown?
        public string Status { get; set; }

        public string DescriptionOfIncident { get; set; }

        public string PlanOfAction { get; set; }

        public string RessponsibleToSolve { get; set; }

        

    }
}