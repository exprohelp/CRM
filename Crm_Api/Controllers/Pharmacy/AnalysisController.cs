using Crm_Api.Models;
using Crm_Api.Repository.ApplicationResource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Crm_Api.Controllers.Pharmacy
{
    [RoutePrefix("api/Analysis")]
    public class AnalysisController : ApiController
    {
        private Repository.Pharmacy.Analysis.SalesReport repositoryAnalysis = new Repository.Pharmacy.Analysis.SalesReport();

        [HttpPost]
        [Route("MultiPurposeAnalysisQueries")]
        public HttpResponseMessage MultiPurposeAnalysisQueries([FromBody]SalesReportInfo objBO)
        {
            dataSet ds = repositoryAnalysis.MultiPurposeAnalysisQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

    }
}
