using Crm_Api.Models;
using Crm_Api.Repository.TPA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Crm_Api.Controllers.JustDialLeads
{
    [RoutePrefix("api/TPA")]
    public class TPAController : ApiController
    {
        private JustDial repositoryJustDial = new JustDial();


        [HttpPost]
        [Route("InsertJustDialLeads")]
        public HttpResponseMessage InsertJustDialLeads([FromBody]ipTPA objBO)
        {
            string result = repositoryJustDial.InsertJustDialLeads(objBO);
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }
    }
}
