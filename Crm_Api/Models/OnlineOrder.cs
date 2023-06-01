using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm_Api.Models
{
    public class ipOnlineOrder
    {
        public string sh_code { get; set; }
        public string order_no { get; set; }
        public string prm_1 { get; set; }
        public string prm_2 { get; set; }
        public string prm_3 { get; set; }
        public string Logic { get; set; }
        public string login_id { get; set; }
        public string card_no { get; set; }
        public string unit_id { get; set; }
        public string item_id { get; set; }
        public string tran_id { get; set; }
        public string del_time { get; set; }
        public string new_med { get; set; }
        public string qty { get; set; }
        public string del_date { get; set; }
        public string ref_by { get; set; }
        public string remark { get; set; }
        public string file_path { get; set; }
        public string trf_to { get; set; }
        public string old_order_no { get; set; }
        public string sale_inv_no { get; set; }
        public string promo_flag { get; set; }
        public string Item_id { get; set; }
        public string salt_code { get; set; }
    }
    public class OrderReport: ipOnlineOrder
    {
        public string from { get; set; }
        public string to { get; set; }
            
    }

}