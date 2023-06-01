using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm_Api.Models
{
    public class SalesReportInfo
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public string prm_1 { get; set; }
        public string prm_2 { get; set; }
        public string logic { get; set; }
        public string login_Id { get; set; }
    }
}