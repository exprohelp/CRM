using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Crm_Api.Models
{
    public class Common
    {
        public class cm1
        {
            [Display(Name = "Transfer To Unit")]
            public string trf_tounit { get; set; }
            [Display(Name = "Transaction Id")]
            public string tran_id { get; set; }
            [Display(Name = "Unit ID")]
            public string unit_id { get; set; }
            [Display(Name = "Item ID")]
            public string item_id { get; set; }
            [Display(Name = "Logic")]
            public string Logic { get; set; }
            [Display(Name = "Created By")]
            public string login_id { get; set; }
            [Display(Name = "Parameter-1")]
            public string prm_1 { get; set; }
            [Display(Name = "Parameter-2")]
            public string prm_2 { get; set; }
            [Display(Name = "Parameter-3")]
            public string prm_3 { get; set; }
            [Display(Name = "Parameter-4")]
            public string prm_4 { get; set; }
            public string orderNo { get; set; }
            public string FileOutPutType { get; set; }
            public string tranType { get; set; }
            public string roleid { get; set; }
            public string searchString { get; set; }
        }
        public class dataSet
        {
            public string Msg { get; set; }
            public DataSet ResultSet { get; set; }
        }
        public class cm2 : cm1
        {
            public string comp_id { get; set; }
            public string dtFrom { get; set; }
            public string dtTo { get; set; }
            public string batch_no { get; set; }
            public int mth { get; set; }
            public int qty { get; set; }

        }
        public class cm3 : cm2
        {
            public int prm_num { get; set; }
            public DataTable dt { get; set; }
            public string empCode { get; set; }
            public string ReportType { get; set; }

        }
        public class datasetWithResult
        {
            public DataSet result { get; set; }
            public string message { get; set; }
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
            public int autoid { get; set; }
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
        public class customerOrders
        {

            public string card_no { get; set; }
            public string old_order_no { get; set; }
            public int qty { get; set; }
            public string new_med { get; set; }
            public string logic { get; set; }
            public string login_id { get; set; }
            public string unit_id { get; set; }
            public string item_id { get; set; }
            public string order_no { get; set; }
            public string mobileno { get; set; }
            public string division { get; set; }
            public string couponNo { get; set; }
            public string TransferToUnit { get; set; }
            public string delivery_date { get; set; }
            public string delivery_time { get; set; }
            public string remarks { get; set; }
            public string ref_by { get; set; }
            public string sale_inv_no { get; set; }
            public string promo_flag { get; set; }
            public string prm_1 { get; set; }
            public string delivery_by { get; set; }
            public string rec_flag { get; set; }
            public string remarkfrom { get; set; }
            public string master_key_id { get; set; }
            public string old_item_id { get; set; }
            public string cancel_flag { get; set; }
            public string cust_name { get; set; }
            public string newProductName { get; set; }

        }
        public class CompleteOrder
        {
            public regulareQueries regulareQueries { get; set; }
            public customerOrders customerOrders { get; set; }
        }

        public class RCMOrderInfo
        {
            public string card_no { get; set; }
            public DateTime nextcall { get; set; }
            public string old_order_no { get; set; }
            public int qty { get; set; }
            public DateTime rmd_date { get; set; }
            public string rmd_time { get; set; }
            public string callType { get; set; }
            public string completedBy { get; set; }
            public string new_med { get; set; }
            public string logic { get; set; }
            public string login_id { get; set; }
            public string unit_id { get; set; }
            public string item_id { get; set; }
            public string order_no { get; set; }
            public string mobileno { get; set; }
            public string division { get; set; }
            public string couponNo { get; set; }
            public string TransferToUnit { get; set; }
            public DateTime delivery_date { get; set; }
            public string delivery_time { get; set; }
            public string remarks { get; set; }
            public string ref_by { get; set; }
            public string sale_inv_no { get; set; }
            public string promo_flag { get; set; }
            public string home_delflag { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public string delivery_by { get; set; }
            public string rec_flag { get; set; }
            public string remarkfrom { get; set; }
            public string master_key_id { get; set; }
            public string old_item_id { get; set; }
            public string cancel_flag { get; set; }
            public string cust_name { get; set; }
            public string newProductName { get; set; }
            public string ItemId { get; set; }
            public string ItemName { get; set; }
            public int Qty { get; set; }
            public string NewMed { get; set; }
            public int LastQty { get; set; }
            public string trf_to { get; set; }
            public string del_date { get; set; }
            public string del_time { get; set; }
            public string remark { get; set; }
            public string MessageForUnit { get; set; }
            public string NextCallDate { get; set; }
            public string NextCallTime { get; set; }
            public string TransferTo { get; set; }
            public string LastCallInfo { get; set; }
            public string UniqeID { get; set; }
            public string CallType { get;  set; }
        }
        
        public class purchSearch
        {
            public string unit_id { get; set; }
            public string purch_id { get; set; }
            public string logic { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public string login_id { get; set; }
        }
        public class transferSearch
        {
            public string unit_id { get; set; }
            public string trf_id { get; set; }
            public string logic { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public string login_id { get; set; }
        }
       
        public class Search
        {
            public string unit_id { get; set; }
            public string Logic { get; set; }
            public string po_number { get; set; }
            public string SearchKey { get; set; }
            public string Prm_1 { get; set; }
            public string Prm_2 { get; set; }
            public string LoginId { get; set; }
            public string FileOutPutType { get; set; }

        }
        public class productSearch
        {
            public string unit_id { get; set; }
            public string item_id { get; set; }
            public string searchKey { get; set; }
            public string logic { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public string login_id { get; set; }
        }
        public class pm_AppVersion
        {
            public string unit_id { get; set; }
            public string machine_id { get; set; }
            public string new_version { get; set; }
            public string cur_version { get; set; }
            public DateTime checkedOn { get; set; }
        }
        public class pm_DailyDispatch
        {
            public string unit_id { get; set; }
            public string tounitid { get; set; }
            public string item_id { get; set; }
            public int QtyInPacks { get; set; }
            public string Logic { get; set; }
            public string login_id { get; set; }
        }
        public class pm_promoQueries
        {
            public string unit_id { get; set; }
            public string item_id { get; set; }
            public string searchKey { get; set; }
            public int qty { get; set; }
            public decimal Disc_per { get; set; }
            public string Logic { get; set; }
            public string login_id { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public string category { get; set; }
            public string master_key_id { get; set; }
            public string input_date { get; set; }
        }
        public class VendorMasterBO
        {
            public int auto_id { get; set; }
            public string unit_id { get; set; }
            public string vendor_id { get; set; }
            public string hosp_id { get; set; }
            public string vendor_name { get; set; }
            public string contact_person { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string address3 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string pin { get; set; }
            public string ledgerid { get; set; }
            public string contact_no { get; set; }
            public string email { get; set; }
            public string payment_mode { get; set; }
            public int payment_days { get; set; }
            public string gst_no { get; set; }
            public string drug_lic_no { get; set; }
            public string notes { get; set; }
            public string active_flag { get; set; }
            public string internalUnitID { get; set; }
            public string login_id { get; set; }
            public string Logic { get; set; }
        }
    }
}