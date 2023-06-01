using Crm_Api.Repository.Utilities;
using PharmacyAPI.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Crm_Api.Models.Common;
using static PharmacyAPI.Models.Stocks;

namespace PharmacyAPI.Controllers
{
    public class StocksController : ApiController
    {
        private Repository.Stocks stkObj = new Repository.Stocks();
        string _result = string.Empty;
        [HttpPost]
        [Route("api/stocks/ProductHelp")]
        public HttpResponseMessage ProductHelpWithStock([FromBody]Search s)
        {
            DataSet dset = stkObj.Retail_ProductHelp(out string result, s.SearchKey, s.Logic, s.unit_id);
            var RetType = new datasetWithResult
            {
                result = dset,
                message = result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/StockWithBatchNos")]
        public HttpResponseMessage GetBatchNos([FromBody]productSearch ps)
        {
            DataSet ds = stkObj.GetBatchNos(out string result, ps.unit_id, ps.item_id, ps.logic, ps.prm_1);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/StocksQueries")]
        public HttpResponseMessage Stocks_Queries([FromBody]cm1 p)
        {
            DataSet ds = stkObj.Stocks_Queries(out string result,p);
            if (p.FileOutPutType == "Excel")
            {
                ExcelGenerator obj = new ExcelGenerator();
                return obj.GetExcelFile(ds);
            }
            else
            {
                var RetType = new datasetWithResult
                {
                    result = ds,
                    message = result
                };
                return Request.CreateResponse(HttpStatusCode.OK, RetType);
            }
          
        }
        [HttpPost]
        [Route("api/stocks/TranStockQueries")]
        public HttpResponseMessage TranStockQueries([FromBody]cm2 ps)
        {
            DataSet ds = stkObj.Tran_StockQueries(out string result, ps);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/InsertModifyDailyDispatch")]
        public HttpResponseMessage Insert_Modify_DailyDispatch([FromBody]pm_DailyDispatch p)
        {
            DataSet ds = stkObj.Insert_Modify_DailyDispatch(out string result, p);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        #region Expiry Methods
        [HttpPost]
        [Route("api/stocks/ExpiryWHImportids")]
        public HttpResponseMessage Expiry_WH_Importids([FromBody]pm_Transfer p)
        {
            DataSet ds = stkObj.Expiry_WH_Importids(out string result, p);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/ExpiryWHCompleteUnitIDs")]
        public HttpResponseMessage Expiry_WH_Complete_UnitIDs([FromBody]pm_Transfer p)
        {
            DataSet ds = stkObj.Expiry_WH_Complete_UnitIDs(out string result, p);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/StockExpiryProcessMethods")]
        public HttpResponseMessage StockExpiryProcess_Methods([FromBody]pm_Transfer p)
        {
            string trfid = p.transfer_id;
            DataSet ds = stkObj.StockExpiryProcess_Methods(ref trfid, p.unit_id, p.logic, p.login_id);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = trfid
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/Expiry_WH_Flaging")]
        public HttpResponseMessage Expiry_WH_Flaging([FromBody]pm_Transfer p)
        {
            _result = stkObj.Expiry_WH_Flaging(p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = _result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/Expiry_WS_CompleteProcess")]
        public HttpResponseMessage Expiry_WS_CompleteProcess([FromBody]cm2 p)
        {
            stkObj.Expiry_WS_CompleteProcess(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        
        #endregion
        [HttpPost]
        [Route("api/stocks/ProductMovementInfo")]
        public HttpResponseMessage ProductMovementInfo([FromBody]pm_Stocks p)
        {
            DataSet ds = stkObj.ProductMovementInfo(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/UpdateInsertOpeningStock")]
        public HttpResponseMessage UpdateInsertOpeningStock([FromBody]pm_Stocks p)
        {
            stkObj.UpdateInsertOpeningStock(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = null,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/TranPosting")]
        public HttpResponseMessage Tran_Posting([FromBody]pm_Transfer p)
        {
            stkObj.Tran_Posting(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = null,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        #region Bulk Distribution
        [HttpPost]
        [Route("api/stocks/BulkDistributionQueries")]
        public HttpResponseMessage BulkDistributionQueries([FromBody] pm_BulkTrfSales p)
        {
            DataSet ds = stkObj.Bulk_PO_Distribution_Queries(out string result, p);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        [HttpPost]
        [Route("api/stocks/BulkDispatchBySales")]
        public HttpResponseMessage BulkDispatchBySales([FromBody] pm_BulkTrfSales p)
        {
            stkObj.Bulk_PO_DispatchBySales(out string result, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/BulkSalesBySalesAvg")]
        public HttpResponseMessage BulkSalesBySalesAvg([FromBody] pm_BulkTrfSales p)
        {
            stkObj.Bulk_SalesBySalesAvg(out string result, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/BulkDispatchByTransfer")]
        public HttpResponseMessage BulkDispatchByTransfer([FromBody] pm_BulkTrfSales p)
        {
            stkObj.Bulk_PO_DispatchByTransfer(out string result, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/BulkTransferBySalesAvg")]
        public HttpResponseMessage BulkTransferBySalesAvg([FromBody] pm_BulkTrfSales p)
        {
            stkObj.Bulk_TransferBySalesAvg(out string result, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = result
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        #endregion
        #region Extra Process Methods
        [HttpPost]
        [Route("api/stocks/ExtraStockQueries")]
        public HttpResponseMessage Extra_StockQueries([FromBody]cm2 p)
        {
            DataSet ds = stkObj.Extra_StockQueries(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/StockTransferByPurchaseID")]
        public HttpResponseMessage StockTransferBy_PurchaseID([FromBody]pm_Transfer p)
        {
            DataSet ds = stkObj.StockTransferBy_PurchaseID(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/ExtraStockChecking")]
        public HttpResponseMessage ExtraStockChecking([FromBody]pm_Transfer p)
        {
            stkObj.Extra_StockChecking(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = null,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/ExtraStockTransferToUnit")]
        public HttpResponseMessage Extra_StockTransferToUnit([FromBody]pm_Transfer p)
        {
            stkObj.Extra_StockTransferToUnit(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = null,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/ExtraSoldToCustomer")]
        public HttpResponseMessage Extra_SoldToCustomer([FromBody]pm_Transfer p)
        {
            stkObj.Extra_SoldToCustomer(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = null,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        #endregion
        [HttpPost]
        [Route("api/stocks/TranInterUnitsInsert")]
        public HttpResponseMessage Tran_InterUnitsInsert([FromBody]pm_Transfer p)
        {
            string trf_id = p.transfer_id;
            DataSet ds = stkObj.Tran_InterUnitsInsert(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/TransDeleteRecord")]
        public HttpResponseMessage Trans_DeleteRecord([FromBody]pm_Transfer p)
        {
            string trf_id = p.transfer_id;
            stkObj.Transfer_DeleteRecord(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = null,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/StockAtOtherStores")]
        public HttpResponseMessage StockAtOtherStores([FromBody]pm_Stocks p)
        {
            DataSet ds = stkObj.StockAtOtherStores(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [Route("api/stocks/AutoEntryForHospitalConsumption")]
        public HttpResponseMessage AutoEntryForHospitalConsumption([FromBody]pm_Transfer p)
        {
           stkObj.AutoEntryForHospitalConsumption(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        [HttpPost]
        [Route("api/stocks/DispatchQueries")]
        public HttpResponseMessage Dispatch_Queries([FromBody]pm_Transfer p)
        {
            DataSet ds = stkObj.Dispatch_Queries(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = ds,
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/Dispatch54")]
        public HttpResponseMessage Dispatch5_4([FromBody]pm_Transfer p)
        {
            stkObj.Dispatch5_4(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/DispatchUpdateQueries")]
        public HttpResponseMessage Dispatch_UpdateQueries([FromBody]pm_Transfer p)
        {
            stkObj.Dispatch_UpdateQueries(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/ReceiveTrfIdInStock")]
        public HttpResponseMessage Receive_TrfId_InStock([FromBody]pm_Transfer p)
        {
            stkObj.Receive_TrfId_InStock(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }
        [HttpPost]
        [Route("api/stocks/BulkTransaction_Processing")]
        public HttpResponseMessage BulkTransaction_Processing([FromBody]pm_Transfer p)
        {
            stkObj.BulkTransaction_Processing(out string processInfo, p);
            var RetType = new datasetWithResult
            {
                result = { },
                message = processInfo
            };
            return Request.CreateResponse(HttpStatusCode.OK, RetType);
        }

        


    } //Second Last
}
