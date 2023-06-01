using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm_Api.Models
{
    public class CustomerData
    {
        public class CustomerInfo
        {
            public string Card_No { get; set; }
            public string Child_DOB_5 { get; set; }
            public string Child_Name_5 { get; set; }
            public string Child_DOB_4 { get; set; }
            public string Child_Name_4 { get; set; }
            public string Child_DOB_3 { get; set; }
            public string Child_Name_3 { get; set; }
            public string Child_DOB_2 { get; set; }
            public string Child_Name_2 { get; set; }
            public string Child_DOB_1 { get; set; }
            public string Child_Name_1 { get; set; }
            public string Spouse_DOB { get; set; }
            public string Spouse_Name { get; set; }
            public string email { get; set; }
            public string MobileNo { get; set; }
            public string PhoneNo { get; set; }
            public string PIN { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string District { get; set; }
            public string Locality { get; set; }
            public string Area { get; set; }
            public string DOB { get; set; }
            public string Cust_Name { get; set; }
            public string CardType { get; set; }
            public string login_id { get; set; }
            public string logic { get; set; }
            public string DistType { get; set; }
            public string PayType { get; set; }
            public string Inst_Code { get; set; }
            public string ActDate { get; set; }
        }

        public class OrderFromWebSite
        {
            public string card_no { get; set; }
            public string unit_id { get; set; }
            public string item_id { get; set; }
            public string new_med { get; set; }
            public int qty { get; set; }
            public string del_date { get; set; }
            public string del_time { get; set; }
            public string ref_by { get; set; }
            public string remark { get; set; }
            public string file_path { get; set; }
            public string order_no { get; set; }
            public string logic { get; set; }
            public byte[] data { get; set; }
        }

        public class regulareQueries
        {
            public string card_no { get; set; }
            public string unit_id { get; set; }
            public string item_id { get; set; }
            public string time { get; set; }
            public string date { get; set; }
            public string logic { get; set; }
            public string loginID { get; set; }
            public string order_no { get; set; }
            public string rmd_date { get; set; }
            public string rmd_time { get; set; }
            public string remark { get; set; }
            public string callType { get; set; }
            public string uniqeID { get; set; }
            public string lastcallinfo { get; set; }


        }
    }
}