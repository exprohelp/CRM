using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyAPI.Models
{
    public class Salts
    {
        public class pm_salts
        {
            public string unit_id { get; set; }
            public string item_id { get; set; }
            public string salt_Code { get; set; }
            public string logic { get; set; }
            public string prm_1 { get; set; }
            public int prm_2 { get; set; }
            public string salt_code { get; set; }
            public string Salt_Name { get; set; }
            public string result { get; set; }
            public string str_code { get; set; }
            public string str_type { get; set; }
            public decimal Strength { get; set; }
            public string login_id { get; set; }
        }
    }
}