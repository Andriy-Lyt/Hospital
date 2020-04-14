using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{    
    public class PublicationShortViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleImage { get; set; }        
    }

    public class ReportViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class PublicationViewModel : PublicationShortViewModel
    {
        public int? ParentId { get; set; }
        public string Body { get; set; }  
    }

    public class PublicationPageViewModel
    {
        public PublicationViewModel Publication { get; set; }
        public IList<PublicationShortViewModel> Children { get; set; }
        public bool IsEditable { get; set; }
    }        

    public class ReportingPageViewModel
    {
        public IList<ReportViewModel> Reports { get; set; }
        public bool IsEditable { get; set; }
    }
}