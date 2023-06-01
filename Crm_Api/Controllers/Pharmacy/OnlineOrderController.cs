using Crm_Api.Models;
using Crm_Api.Repository.Pharmacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Crm_Api.Controllers.Pharmacy
{
    [RoutePrefix("api/OnlineOrder")]
    public class OnlineOrderController : ApiController
    {
        private OnlineOrder repositoryOnline = new OnlineOrder();

        [HttpPost]
        [Route("OnlineOrderQueries")]
        public HttpResponseMessage OnlineOrderQueries([FromBody]ipOnlineOrder objBO)
        {
            dataSet ds = repositoryOnline.OnlineOrderQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("OnlineOrderInsert")]
        public HttpResponseMessage OnlineOrderInsert([FromBody]ipOnlineOrder objBO)
        {
            string result = repositoryOnline.OnlineOrderInsert(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        [HttpPost]
        [Route("RCMCompleteOrder")]
        public HttpResponseMessage RCMCompleteOrder([FromBody]ipOnlineOrder objBO)
        {
            string result = repositoryOnline.RCMCompleteOrder(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("UpdateTablesInfo")]
        public HttpResponseMessage UpdateTablesInfo([FromBody]ipOnlineOrder objBO)
        {
            string result = repositoryOnline.UpdateTablesInfo(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("OrderTrackingReport")]
        public HttpResponseMessage OrderTrackingReport([FromBody]OrderReport objBO)
        {
            dataSet ds = repositoryOnline.OrderTrackingReport(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
       
    }
}
