using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm_Api.Models
{
    public class TeleCaller
    {
        public string CardNo { get; set; }
        public string MobileNo { get; set; }       
        public string CallBy { get; set; }
        public string CallRemark { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Prm1 { get; set; }
        public string Prm2 { get; set; }
        public string login_id { get; set; }
        public string Logic { get; set; }
    }
}