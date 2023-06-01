using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Crm_Api.Models.Common;

using static PharmacyAPI.Models.Salts;

namespace PharmacyAPI.Controllers
{
    public class SaltController : ApiController
    {
        private Repository.Salts saltObj = new Repository.Salts();
        [HttpPost]
        [Route("api/salts/SaltQueries")]
        public HttpResponseMessage SaltQueries([FromBody] pm_salts p)
        {
            DataSet ds = saltObj.SaltQueries(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/salts/InsertSaltInfo")]
        public HttpResponseMessage InsertSaltInfo([FromBody] pm_salts p)
        {
            saltObj.InsertSaltInfo(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/salts/InsertStrengthInfo")]
        public HttpResponseMessage InsertStrengthInfo([FromBody] pm_salts p)
        {
            saltObj.InsertStrengthInfo(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/salts/InsertSubstitute")]
        public HttpResponseMessage InsertSubstitute([FromBody] pm_salts p)
        {
            saltObj.InsertStrengthInfo(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

    }
}
