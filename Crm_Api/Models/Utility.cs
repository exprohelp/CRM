using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm_Api.Models
{
    //public class dataSet
    //{
    //	public string Msg { get; set; }
    //	public DataSet ResultSet { get; set; }
    //}
    public class ipUploadPDF
    {
        public string FileName;
        public string FileByteArray;
        public string Logic { get; set;}
    }
    public class SubmitStatus
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string purchId { get; set; }
        public string virtual_path { get; set; }
    }
    public class ipDocumentInfo
    {
        public string HospitalId { get; set; }
        public string TranId { get; set; }
        public string TrnType { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        public string login_id { get; set; }
    }
}