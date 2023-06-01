
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crm_Api.Models;
using Crm_Api.Repository.Utilities;
using Crm_Api.Repository.Online;
using System.Collections.Generic;

namespace Crm_Api.Controllers
{
    //[EnableCors(origins: "https://chandanOnline.in", headers: "*", methods: "*")]
    public class OnlineDiagnosticController : ApiController
    {      
        private PackageBooking repositoryPackage = new PackageBooking();

		#region PackageBooking
		[HttpPost]
		[Route("api/OnlineDiagnostic/Base64")]
		public HttpResponseMessage Base64([FromBody] ipPackageQueries objBO)
		{
			ExcelGenerator obj = new ExcelGenerator();
			return obj.GetPDFFile();
		}
		[HttpPost]
        [Route("api/OnlineDiagnostic/Online_DiagnosticPackageQueries")]
        public HttpResponseMessage Online_DiagnosticPackageQueries([FromBody] ipPackageQueries ipapp)
        {
            dataSet ds = repositoryPackage.Online_DiagnosticPackageQueries(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("api/OnlineDiagnostic/OnlinePackageNotification")]
        public HttpResponseMessage OnlinePackageNotification([FromBody] ipPackageQueries ipapp)
        {
            string result = repositoryPackage.BookingNotification(ipapp);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("api/OnlineDiagnostic/PackageConfirmation")]
        public HttpResponseMessage PackageConfirmation([FromBody] ipPackageOnlineConfirmation ipConfirm)
        {
            dataSet ds = repositoryPackage.PackageConfirmation(ipConfirm);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }	
		[HttpPost]
		[Route("api/OnlineDiagnostic/getPaymentResponse")]
		public HttpResponseMessage getPaymentResponse([FromBody] ipTransaction obj)
		{
			dataSet dsResult = repositoryPackage.CallWebApiPayUMoney("getPaymentResponse", obj);
			return Request.CreateResponse(HttpStatusCode.OK, dsResult);
		}
		[HttpPost]
		[Route("api/OnlineDiagnostic/getPaymentResponse2")]
		public HttpResponseMessage getPaymentResponse2([FromBody] ipTransaction obj)
		{
			dataSet dsResult = repositoryPackage.CallWebApiPayUMoney2("getPaymentResponse", obj);
			return Request.CreateResponse(HttpStatusCode.OK, dsResult);
		}
		[HttpPost]
		[Route("api/OnlineDiagnostic/InsertMobileAppCoupons")]
		public HttpResponseMessage InsertMobileAppCoupons([FromBody] CouponLogBO objBO)
		{
			string result = repositoryPackage.InsertMobileAppCoupons(objBO);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		[Route("api/OnlineDiagnostic/UpdatePaymentStatus")]
		public HttpResponseMessage UpdatePaymentStatus([FromBody] ipUpdatePayStatus obj)
		{
			string result = repositoryPackage.UpdatePaymentStatus(obj);
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
        [HttpPost]
        [Route("api/OnlineDiagnostic/CallWebApiPayUMoneyByDate")]
        public HttpResponseMessage CallWebApiPayUMoneyByDate([FromBody] ipTransaction obj)
        {
            string ResultInfo = string.Empty;
            List<TransactionDetail> dsResult = repositoryPackage.CallWebApiPayUMoneyByDate(out ResultInfo, obj);
            return Request.CreateResponse(HttpStatusCode.OK, dsResult);
        }
        #endregion PackageBooking
    }
}
