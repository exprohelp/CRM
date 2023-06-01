using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyAPI.Models
{
    public class Stocks
    {
        public class pm_BulkTrfSales
        {
            public string unit_id { get; set; }
            public string transfer_id { get; set; }
            public string order_no { get; set; }
            public string sold_to { get; set; }
            public string party_id { get; set; }
            public string account_id { get; set; }
            public string TransferToUnit { get; set; }
            public string logic { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public string login_id { get; set; }
        }
        public class pm_Transfer
        {
            public string unit_id { get; set; }
            public string orderNo { get; set; }
            public string orderFor { get; set; }
            public string transfer_id { get; set; }
            public string transaction_id { get; set; }
            public string vendor_id { get; set; }
            public string tran_type { get; set; }
            public string TransferFromUnit { get; set; }
            public string TransferToUnit { get; set; }
            public string TrfUnit_AccountNo { get; set; }
            public string party_id { get; set; }
            public string party_account_no { get; set; }
            public string logic { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public string prm_3 { get; set; }
            public string login_id { get; set; }
            public string dtFrom { get; set; }
            public string dtTo { get; set; }
            public string item_id { get; set; }
            public string master_key_id { get; set; }
            public int qty { get; set; }
            public string cart_name { get; set; }
            public string GenFrom { get; set; }
            public int RcptQty { get; set; }
            public string CheckFlag { get; set; }
        }
        public class pm_Stocks {
            public string unit_id { get; set; }
            public string item_id { get; set; }
            public string master_key_id { get; set; }
            public string searchKey { get; set; }
            public string logic { get; set; }
            public string prm_1 { get; set; }
            public string prm_2 { get; set; }
            public int qty { get; set; }
            public string dtFrom { get; set; }
            public string dtTo { get; set; }
            public string login_id { get; set; }

        }
    }
}