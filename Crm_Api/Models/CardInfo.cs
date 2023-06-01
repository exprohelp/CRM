using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm_Api.Models
{
    public class ipCardInfo
    {
        public string UnitId { get; set; }
        public string CustomerId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Prm_1 { get; set; }
        public string Prm_2 { get; set; }
        public string Logic { get; set; }
        public string Login_Id { get; set; }
    }
}