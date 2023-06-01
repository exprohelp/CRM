using System;

namespace PharmacyAPI.Models
{
	public class ipDiary
	{
		public int auto_id { get; set; }
		public string Prm1 { get; set; }
		public string Prm2 { get; set; }
		public int isActive { get; set; }
		public string Logic { get; set; }
	}
	public class DiaryBO : ipDiary
	{
		public string item_id { get; set; }
		public string item_name { get; set; }
		public string mfd_id { get; set; }
		public string mfd_name { get; set; }
		public string NPR { get; set; }
		public string MRP { get; set; }
		public string vendor_id { get; set; }
		public string vendor_name { get; set; }
		public string medical_repersentative { get; set; }
		public string mr_contoacNo { get; set; }
		public string paymentMode { get; set; }
		public string PaymentSite { get; set; }
		public string contact_no { get; set; }
		public string email { get; set; }
		public string Remarks { get; set; }
		public string Type { get; set; }
		public int priority { get; set; }
		public string createdBy { get; set; }
		public DateTime CreationDate { get; set; }
		public string UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}