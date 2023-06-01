using Crm_Api.Repository.Pharmacy;
using Crm_Api.Repository.Utilities;
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
    [RoutePrefix("api/customerdata")]
    public class CustomerDataController : ApiController
    {
        private Repository.Pharmacy.CustomerData obj = new Repository.Pharmacy.CustomerData();

        string _result = string.Empty;
        DataSet dset = new DataSet();
        [HttpPost]
        [Route("rcmqueries")]
        public HttpResponseMessage RCM_Queries([FromBody]cm2 p)
        {
            dset = obj.RCM_Queries(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = dset,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        [HttpPost]
        [Route("MobileAuthenticateCoupon")]
        public HttpResponseMessage Mobile_AuthenticateCoupon([FromBody] customerOrders p)
        {
            obj.Mobile_AuthenticateCoupon(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = null,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        // Mobile_GetCoupons(out string processInfo, customerOrders p)
        [HttpPost]
        [Route("MobileGetCoupons")]
        public HttpResponseMessage Mobile_GetCoupons([FromBody] customerOrders p)
        {
            obj.Mobile_GetCoupons(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = null,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        [HttpPost]
        [Route("RCMSmallQueries")]
        public HttpResponseMessage RCM_SmallQueries([FromBody]regulareQueries p)
        {
            dset = obj.RCM_SmallQueries(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = dset,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        [HttpPost]
        [Route("RCMCreateDataLog")]
        public HttpResponseMessage RCMCreateDataLog([FromBody]regulareQueries p)
        {
            obj.RCMCreateDataLog(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("OnlineOrderQueries")]
        public HttpResponseMessage OnlineOrder_Queries([FromBody]cm2 p)
        {
            dset = obj.OnlineOrder_Queries(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = dset,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        [HttpPost]
        [Route("OrderInfoForUnits")]
        public HttpResponseMessage OrderInfo_ForUnits([FromBody]cm2 p)
        {
            dset = obj.OrderInfo_ForUnits(p);
            var RetType = new datasetWithResult
            {
                result = dset,
                message = "-"
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        [HttpPost]
        [Route("OnlineOrderInsert")]
        public HttpResponseMessage OnlineOrder_Insert([FromBody]OrderFromWebSite p)
        {
            _result = obj.OnlineOrder_Insert(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = null,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("RCMReOrderPendings")]
        public HttpResponseMessage RCMReOrderPendings([FromBody]customerOrders p)
        {
            _result = obj.RCM_ReOrderPendings(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        [HttpPost]
        [Route("RCMCompleteMobileOrder")]
        public HttpResponseMessage RCMCompleteMObileOrder([FromBody]customerOrders p)
        {
            _result = obj.RCM_CompleteMobileOrder(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("MarkOrderDelivered")]
        public HttpResponseMessage MarkOrderDelivered([FromBody]customerOrders p)
        {
            obj.Mark_OrderDelivered(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        [HttpPost]
        [Route("InsertRegularOrder")]
        public HttpResponseMessage InsertRegularOrder([FromBody]customerOrders p)
        {
            _result = obj.InsertRegularOrder(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("InsertOrderRemark")]
        public HttpResponseMessage Insert_OrderRemark([FromBody]customerOrders p)
        {
            obj.Insert_OrderRemark(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        [HttpPost]
        [Route("InsertCustHoldSalesInfo")]
        public HttpResponseMessage Insert_CustHoldSalesInfo([FromBody]customerOrders p)
        {
            obj.Insert_CustHoldSalesInfo(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        [HttpPost]
        [Route("InsertModifycustOrderInfo")]
        public HttpResponseMessage Insert_Modify_custOrderInfo([FromBody]customerOrders p)
        {
            obj.Insert_Modify_custOrderInfo(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("UpdateTablesInfo")]
        public HttpResponseMessage UpdateTablesInfo([FromBody] cm1 p)
        {
            obj.UpdateTablesInfo(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }      
        [HttpPost]
        [Route("OrderTrackingReport")]
        public HttpResponseMessage OrderTrackingReport([FromBody] cm3 p)
        {
            dset = obj.OrderTrackingReport(out string processInfo, p);           
            if (p.FileOutPutType == "Excel")
            {
                ExcelGenerator obj = new ExcelGenerator();
                return obj.GetExcelFile(dset);
            }
            else
            {
                var RetType = new datasetWithResult
                {
                    result = dset,
                    message = processInfo
                };
                return Request.CreateResponse(HttpStatusCode.OK, RetType);
            }
            
        }
        [HttpPost]
        [Route("RcmProductInfo")]
        public HttpResponseMessage RCM_ProductInfo([FromBody] customerOrders p)
        {
            dset = obj.RCM_ProductInfo(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = dset,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("RCMCallReport")]
        public HttpResponseMessage RCMCall_Report([FromBody] cm3 p)
        {
            dset = obj.RCMCall_Report(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = dset,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("RCMOnCallOrder")]
        public HttpResponseMessage RCM_OnCallOrder([FromBody] customerOrders p)
        {
            obj.RCM_OnCallOrder(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("MultiPurposeAnalysisQueries")]
        public HttpResponseMessage MultiPurpose_AnalysisQueries([FromBody] cm3 p)
        {
            dset = obj.MultiPurpose_AnalysisQueries(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = dset,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("ScheduleQueries")]
        public HttpResponseMessage Schedule_Queriess([FromBody] cm3 p)
        {
            dset = obj.Schedule_Queries(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = dset,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        //Schedule_Queries
        #region HealthCard Methods
        [HttpPost]
        [Route("CallingCardQueries")]
        public HttpResponseMessage CallingCard_queries([FromBody] cm2 p)
        {
            dset = obj.CallingCard_queries(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = dset,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        [HttpPost]
        [Route("GetACardInfo")]
        public HttpResponseMessage GetACardInfo([FromBody] cm1 p)
        {
            dset = obj.GetACardInfo(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = dset,
                message = processInfo
            };


            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("CardInfoInsert")]
        public HttpResponseMessage Card_Info_insert([FromBody] CustomerInfo p)
        {
            _result = obj.Card_Info_insert(p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = _result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("RCMCallReportQ")]
        public HttpResponseMessage RCMCallReport([FromBody] cm3 p)
        {
            dset = obj.RCMCall_Report(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = _result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("InsertCustCallLog")]
        public HttpResponseMessage Insert_CustCall_Log([FromBody] regulareQueries p)
        {
            _result = obj.Insert_CustCall_Log(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("CompleteOrder")]
        public HttpResponseMessage CompleteOrder([FromBody]CompleteOrder p)
        {
            //obj.RCMCreateDataLog(out string processInfo, p.regulareQueries);
            //if (processInfo.Contains("Success"))
            //{
            //    obj.Insert_CustCall_Log(out string processInfo1, p.regulareQueries);
            //    obj.RCM_CompleteOrder(out string processInfo2,p.customerOrders);
            //    _result = processInfo2;
            //}
            var RetType = new datasetWithResult
            {
                result = { },
                message = _result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        #endregion

        [HttpPost, Route("RCMCompleteOrderNew")]
        public HttpResponseMessage RCMCompleteOrderNew(RCMOrderInfo objBO)
        {
            string result = obj.RCMCompleteOrderNew(objBO);
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }

        //[HttpPost]
        //[Route("OrderTrackingReport")]
        //public HttpResponseMessage OrderTrackingReport([FromBody] cm3 p)
        //{
        //    dset = obj.OrderTrackingReport(out string processInfo, p);
        //    var RetType = new datasetWithResult
        //    {
        //        result = dset,
        //        message = processInfo
        //    };
        //    return Request.CreateResponse(HttpStatusCode.OK, RetType);
        //}


        #region Windows Application Menu Management
        //[HttpPost]
        //[Route("api/customerdata/MenuRightsQueries")]
        //public HttpResponseMessage MenuRightsQueries([FromBody]Models.Menu.pm_Menu_RightsQueries p)
        //{

        //    DataSet dset = obj.Menu_RightsQueries(out string processInfo, p);
        //    var RetType = new datasetWithResult
        //    {
        //        result = dset,
        //        message = processInfo
        //    };
        //    return Request.CreateResponse(HttpStatusCode.OK, RetType);
        //}
        //[HttpPost]
        //[Route("api/customerdata/InsertModifyMenuRights")]
        //public HttpResponseMessage Insert_Modify_Menu_Rights([FromBody]pm_Insert_Modify_menu_master p)
        //{

        //    string result = obj.Insert_Modify_Menu_Rights(p);

        //    var RetType = new datasetWithResult
        //    {
        //        result = null,
        //        message = result
        //    };
        //    return Request.CreateResponse(HttpStatusCode.OK, RetType);
        //}       

        #endregion
    }
}
