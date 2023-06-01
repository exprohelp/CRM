using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Crm_Api.Models.Common;

namespace Crm_Api.Controllers.Pharmacy
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {

        private Repository.Pharmacy.Product_DAL prodObj = new Repository.Pharmacy.Product_DAL();
        string _processInfo = string.Empty; DataSet _ds = new DataSet();
        [HttpPost]
        [Route("ProductQueries")]
        public HttpResponseMessage ProductQueries([FromBody] cm1 p)
        {
            _ds = prodObj.Product_Queries(out _processInfo, p);
            var RetType = new datasetWithResult
            {
                result = _ds,
                message = _processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("Product_Queries")]
        public HttpResponseMessage ProductsQueries([FromBody] cm1 p)
        {
            _ds = prodObj.ProductsQueries(out _processInfo,p);
            var RetType = new datasetWithResult
            {
                result = _ds,
                message = _processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
    }
}
