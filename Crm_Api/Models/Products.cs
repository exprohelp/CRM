using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyAPI.Models
{
    public class Products
    {
        public string hsn { get; set; }
        public string mfd_id { get; set; }
        public string mfd_name { get; set; }
        public string item_type { get; set; }
        public string item_id { get; set; }
        public string item_name { get; set; }
        public string generic_name { get; set; }
        public string flavour { get; set; }
        public int pack_qty { get; set; }
        public string pack_qty_unit { get; set; }
        public string pack_type { get; set; }
        public string category { get; set; }
        public string sch_category { get; set; }
        public string sell_type { get; set; }
        public string remark { get; set; }
        public string status_flag { get; set; }
        public string file_path { get; set; }
        public string login_id { get; set; }
        public DateTime cr_date { get; set; }
        public string salt_info { get; set; }
        public string his_flag { get; set; }
        public string logic { get; set; }
        public string fileext { get; set; }
    }
    public class prm_newProduct
    {

        public string unit_id { get; set; }
        public string item_id { get; set; }
        public string item_name { get; set; }
        public int qty { get; set; }
        public string action_flag { get; set; }
        public string sale_inv_no { get; set; }
        public string order_no { get; set; }
        public string logic { get; set; }
        public string login_id { get; set; }
    }
    public class pm_UpdateAlert
    {
        public string item_id { get; set; }
        public string alertMessage { get; set; }
        public string orderDays { get; set; }
        public string unitids { get; set; }
        public string login_id { get; set; }
    }
    public class pm_SaltInfo
    {
        public string item_id { get; set; }
        public string salt_code { get; set; }
        public string Salt_Name { get; set; }
        public string logic { get; set; }
        public string prm_1 { get; set; }
        public int prm_2 { get; set; }
        public string result { get; set; }
        public string str_code { get; set; }
        public string str_type { get; set; }
        public decimal Strength { get; set; }
        public string login_id { get; set; }
    }
    public class ipOpticalBooking
    {
        public string unit_id { set; get; }
        public string CustomerId { set; get; }
        public string PatientName { set; get; }
        public string MobileNo { set; get; }
        public string Address { set; get; }
        public string ItemIdList { set; get; }
        public string ItemNameList { set; get; }
        public decimal NetAmount { set; get; }
        public decimal AdvAmount { set; get; }
        public string login_id { set; get; }
        public string Logic { set; get; }
    }
    public class ipOpticalResponse
    {
        public string CustomerId { set; get; }
        public string Message { set; get; }
      
    }
}
