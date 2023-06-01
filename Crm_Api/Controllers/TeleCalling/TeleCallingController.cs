using Crm_Api.Models;
using Crm_Api.Repository.TeleCalling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Crm_Api.Controllers
{
    [RoutePrefix("api/TeleCalling")]
    public class TeleCallingController : ApiController
    {
        private TeleCalling repository = new TeleCalling();

        [HttpPost]
        [Route("HIM_InsertCallLog")]
        public HttpResponseMessage HIM_InsertCallLog([FromBody]TeleCaller objBO)
        {
            string result = repository.HIM_InsertCallLog(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
