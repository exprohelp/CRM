using Crm_Api.Repository.ApplicationResource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static Crm_Api.Models.Common;
using static PharmacyAPI.Models.Stocks;

namespace PharmacyAPI.Repository
{
    public class Stocks
    {
        string _result = string.Empty;
        public DataSet Retail_ProductHelp(out string processInfo, string search, string logic, string unit_id)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy);
            try
            {
                using (SqlCommand cmd = new SqlCommand("pRetail_ProductHelp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@search", SqlDbType.VarChar, 50).Value = search;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = logic;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = unit_id;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        processInfo = "Success";
                        da.Fill(ds);

                    }
                }
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Fail : " + sqlEx.Message;
            }
            finally { con.Close(); }
            return ds;

        }
        public DataSet GetBatchNos(out string processInfo, string unit_id, string item_id, string logic, string p1)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
                {
                    using (SqlCommand cmd = new SqlCommand("pRetail_GetBatchNos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1500;
                        cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = unit_id;
                        cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 10).Value = item_id;
                        cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = logic;
                        cmd.Parameters.Add("@p1", SqlDbType.VarChar, 2000).Value = p1;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            if (con != null && con.State == ConnectionState.Closed)
                                con.Open();
                            da.Fill(ds);
                            processInfo = "Success";
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Fail : " + sqlEx.Message;
            }
            return ds;
        }
        public DataSet Stocks_Queries(out string processInfo, cm1 p)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
                {
                    using (SqlCommand cmd = new SqlCommand("pStocks_Queries", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1500;
                        cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                        cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = p.Logic;
                        cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = p.prm_1;
                        cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = p.prm_2;
                        cmd.Parameters.Add("@prm_3", SqlDbType.VarChar, 500).Value = p.prm_3;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                            processInfo = "Success";
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Fail : " + sqlEx.Message;
            }
            return ds;
        }
        public DataSet Tran_StockQueries(out string processInfo, cm2 p)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
                {
                    using (SqlCommand cmd = new SqlCommand("pTran_StockQueries", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1500;
                        cmd.Parameters.Add("@unit_id", SqlDbType.NVarChar, 10).Value = p.unit_id;
                        cmd.Parameters.Add("@trf_to", SqlDbType.VarChar, 16).Value = p.trf_tounit;
                        cmd.Parameters.Add("@tran_id", SqlDbType.VarChar, 16).Value = p.tran_id;
                        cmd.Parameters.Add("@logic", SqlDbType.NVarChar, 50).Value = p.Logic;
                        cmd.Parameters.Add("@prm_1", SqlDbType.NVarChar, 50).Value = p.prm_1;
                        cmd.Parameters.Add("@prm_2", SqlDbType.NVarChar, 50).Value = p.prm_2;
                        cmd.Parameters.Add("@prm_3", SqlDbType.NVarChar, 50).Value = p.prm_3;
                        cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = p.dtFrom;
                        cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = p.dtTo;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                            processInfo = "Success";
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Fail : " + sqlEx.Message;
            }
            return ds;
        }
        public DataSet Insert_Modify_DailyDispatch(out string processInfo, pm_DailyDispatch p)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
            {
                using (SqlCommand cmd = new SqlCommand("Insert_Modify_mst_DailyDispatch", con))
                {
                    con.Open();
                    SqlTransaction trn = con.BeginTransaction();
                    cmd.Transaction = trn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@ToUnitid", SqlDbType.VarChar, 10).Value = p.tounitid;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 10).Value = p.item_id;
                    cmd.Parameters.Add("@qtyInPack", SqlDbType.Int).Value = p.QtyInPacks;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = p.Logic;
                    cmd.Parameters.Add("@createdBy", SqlDbType.VarChar, 10).Value = p.login_id;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                            processInfo = cmd.Parameters["@result"].Value.ToString();
                            if (processInfo.Contains("Success"))
                                trn.Commit();
                            else
                                trn.Rollback();
                        }

                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found : " + sqlEx.Message; trn.Rollback();
                    }
                    finally { con.Close(); }
                    return ds;
                }
            }

        }
        #region Bulk Sales and Transfer
        public DataSet Bulk_PO_Distribution_Queries(out string processInfo, pm_BulkTrfSales p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            try
            {
                using (SqlCommand cmd = new SqlCommand("pBulk_PO_Distribution_Queries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 40).Value = p.logic;
                    cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.order_no;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = p.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = p.prm_2;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        processInfo = "Success";
                        da.Fill(ds);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Fail : " + sqlEx.Message;
            }
            finally { con.Close(); }
            return ds;
        }
        public void Bulk_PO_DispatchBySales(out string processInfo, pm_BulkTrfSales p)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pBulk_PO_DispatchBySales", con))
                {
                    con.Open();
                    SqlTransaction trn = con.BeginTransaction();
                    cmd.Transaction = trn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@sold_to", SqlDbType.VarChar, 10).Value = p.sold_to;
                    cmd.Parameters.Add("@party_id", SqlDbType.VarChar, 16).Value = p.party_id;
                    cmd.Parameters.Add("@account_id", SqlDbType.VarChar, 16).Value = p.account_id;
                    cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.order_no;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                    try
                    {

                        cmd.ExecuteNonQuery();
                        processInfo = cmd.Parameters["@result"].Value.ToString();
                        if (processInfo.Contains("Success"))
                            trn.Commit();
                        else
                            trn.Rollback();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found : " + sqlEx.Message; trn.Rollback();
                    }
                    finally { con.Close(); }
                }
            }

        }
        public void Bulk_SalesBySalesAvg(out string processInfo, pm_BulkTrfSales p)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pBulk_SalesBySalesAvg", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@sold_to", SqlDbType.VarChar, 10).Value = p.sold_to;
                    cmd.Parameters.Add("@party_id", SqlDbType.VarChar, 16).Value = p.party_id;
                    cmd.Parameters.Add("@account_id", SqlDbType.VarChar, 16).Value = p.account_id;
                    cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.order_no;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        processInfo = cmd.Parameters["@result"].Value.ToString();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
            }
        }
        public void Bulk_PO_DispatchByTransfer(out string processInfo, pm_BulkTrfSales p)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pBulk_PO_DispatchByTransfer", con))
                {
                    con.Open();
                    SqlTransaction trn = con.BeginTransaction();
                    cmd.Transaction = trn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@TransferToUnit", SqlDbType.VarChar, 10).Value = p.TransferToUnit;
                    cmd.Parameters.Add("@party_id", SqlDbType.VarChar, 16).Value = p.party_id;
                    cmd.Parameters.Add("@account_id", SqlDbType.VarChar, 16).Value = p.account_id;
                    cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.order_no;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                    try
                    {

                        cmd.ExecuteNonQuery();
                        processInfo = cmd.Parameters["@result"].Value.ToString();
                        if (processInfo.Contains("Success"))
                            trn.Commit();
                        else
                            trn.Rollback();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found : " + sqlEx.Message; trn.Rollback();
                    }
                    finally { con.Close(); }
                }
            }
        }
        public void Bulk_TransferBySalesAvg(out string processInfo, pm_BulkTrfSales p)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pBulk_TransferBySalesAvg", con))
                {
                    con.Open();
                    SqlTransaction trn = con.BeginTransaction();
                    cmd.Transaction = trn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@TransferToUnit", SqlDbType.VarChar, 10).Value = p.TransferToUnit;
                    cmd.Parameters.Add("@party_id", SqlDbType.VarChar, 16).Value = p.party_id;
                    cmd.Parameters.Add("@account_id", SqlDbType.VarChar, 16).Value = p.account_id;
                    cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.order_no;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                    try
                    {

                        cmd.ExecuteNonQuery();
                        processInfo = cmd.Parameters["@result"].Value.ToString();
                        if (processInfo.Contains("Success"))
                            trn.Commit();
                        else
                            trn.Rollback();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found : " + sqlEx.Message; trn.Rollback();
                    }
                    finally { con.Close(); }
                }
            }

        }
        #endregion
        #region Stock reconcilation
        public DataSet ProductMovementInfo(out string processInfo, pm_Stocks p)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
                {
                    using (SqlCommand cmd = new SqlCommand("pProductMovementInfo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1500;
                        cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                        cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 10).Value = p.item_id;
                        cmd.Parameters.Add("@master_key_id", SqlDbType.VarChar, 16).Value = p.master_key_id;
                        cmd.Parameters.Add("@searchKey", SqlDbType.VarChar, 40).Value = p.searchKey;
                        cmd.Parameters.Add("@logic", SqlDbType.VarChar, 40).Value = p.logic;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                            processInfo = "Success";
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Fail : " + sqlEx.Message;
            }
            return ds;
        }
        public void UpdateInsertOpeningStock(out string processInfo,pm_Stocks p)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pUpdateInsertOpeningStock", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@master_key_id", SqlDbType.VarChar, 16).Value = p.master_key_id;
                    cmd.Parameters.Add("@qty", SqlDbType.VarChar, 10).Value = p.qty;
                    cmd.Parameters.Add("@loginid", SqlDbType.VarChar, 10).Value = p.login_id;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = cmd.Parameters["@result"].Value.ToString();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
            }
        }
        #endregion
        #region Expiry Process Method
        public DataSet Expiry_WH_Importids(out string processInfo, pm_Transfer p)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
                {
                    using (SqlCommand cmd = new SqlCommand("pExpiry_WH_Importids", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1500;
                        cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                        cmd.Parameters.Add("@trf_id", SqlDbType.VarChar, 16).Value = p.transfer_id;
                        cmd.Parameters.Add("@groupby", SqlDbType.VarChar, 3).Value = p.prm_1;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                            processInfo = "Success";
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Fail : " + sqlEx.Message;
            }
            return ds;
        }
        public DataSet Expiry_WH_Complete_UnitIDs(out string processInfo, pm_Transfer p)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
                {
                    using (SqlCommand cmd = new SqlCommand("pExpiry_WH_Complete_UnitIDs", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1500;
                        cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                        cmd.Parameters.Add("@trf_id", SqlDbType.VarChar, 16).Value = p.transfer_id;
                        cmd.Parameters.Add("@TrfUnit_AccountNo", SqlDbType.VarChar, 16).Value = p.TrfUnit_AccountNo;
                        cmd.Parameters.Add("@logic", SqlDbType.VarChar, 16).Value = p.logic;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "N/A";
                        cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                            processInfo = cmd.Parameters["@result"].Value.ToString();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Fail : " + sqlEx.Message;
            }
            return ds;
        }
        public DataSet StockExpiryProcess_Methods(ref string trf_id, string unit_id, string logic, string login_id)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
                {
                    using (SqlCommand cmd = new SqlCommand("pStockExpiryProcess_Methods", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = unit_id;
                        cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = logic;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = login_id;
                        cmd.Parameters.Add("@trf_id", SqlDbType.VarChar, 16).Value = trf_id;
                        cmd.Parameters["@trf_id"].Direction = ParameterDirection.InputOutput;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                            trf_id = (string)cmd.Parameters["@trf_id"].Value.ToString();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                trf_id = "Fail : " + sqlEx.Message;
            }
            return ds;
        }
        public string Expiry_WH_Flaging(pm_Transfer p)
        {
            string processInfo = "";
            SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy);
            SqlCommand cmd = new SqlCommand("pExpiry_WH_Flaging", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@trf_id", SqlDbType.VarChar, 20).Value = p.transfer_id;
            cmd.Parameters.Add("@vendor_id", SqlDbType.VarChar, 16).Value = p.vendor_id;
            cmd.Parameters.Add("@account_id", SqlDbType.VarChar, 16).Value = p.party_account_no;
            cmd.Parameters.Add("@masterkey_id", SqlDbType.VarChar, 16).Value = p.master_key_id;
            cmd.Parameters.Add("@rcpt_qty", SqlDbType.Int).Value = p.RcptQty;
            cmd.Parameters.Add("@chkFlag", SqlDbType.Char, 1).Value = p.CheckFlag;
            cmd.Parameters.Add("@logic", SqlDbType.VarChar, 20).Value = p.logic;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 20).Value = "";
            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                processInfo = (string)cmd.Parameters["@result"].Value.ToString();
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Error Found   : " + sqlEx.Message;
            }
            finally { con.Close(); }
            return processInfo;
        }
        public void Expiry_WS_CompleteProcess(out string processInfo, cm2 p)
        {
            SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy);
            SqlCommand cmd = new SqlCommand("pExpiry_WS_CompleteProcess", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 5000;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@comp_id", SqlDbType.VarChar, 10).Value = p.comp_id;
            cmd.Parameters.Add("@date", SqlDbType.Date).Value = p.dtFrom;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 16).Value = p.login_id;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            try
            {
                if (con != null && con.State == ConnectionState.Closed)
                con.Open();
                cmd.ExecuteNonQuery();
                processInfo = (string)cmd.Parameters["@result"].Value.ToString();
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Error Found   : " + sqlEx.Message;
            }
            finally { con.Close(); }
            
        }

        #endregion
        #region Extra Stock method
        public DataSet  StockTransferBy_PurchaseID(out string processInfo, pm_Transfer p)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("pStockTransferBy_PurchaseID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@purch_id", SqlDbType.VarChar, 16).Value = p.transaction_id;
                        cmd.Parameters.Add("@trf_from", SqlDbType.VarChar, 10).Value = p.unit_id;
                        cmd.Parameters.Add("@trf_to", SqlDbType.VarChar, 10).Value = p.TransferToUnit;
                        cmd.Parameters.Add("@genFrom", SqlDbType.VarChar, 16).Value = p.tran_type;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {

                            da.Fill(ds);
                            processInfo = "Success";
                        }
                    }
                    con.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                processInfo= "Fail : " + sqlEx.Message;
            }
            return ds;
        }
        public DataSet Extra_StockQueries(out string processInfo, cm2 p)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("pExtra_StockQueries", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                        cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = p.Logic;
                        cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = p.prm_1;
                        cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = p.prm_2;
                        cmd.Parameters.Add("@prm_3", SqlDbType.VarChar, 50).Value = p.prm_3;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {

                            da.Fill(ds);
                            processInfo = "Success";
                        }
                    }
                    con.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Fail : " + sqlEx.Message;
            }
            return ds;
        }

        public string Extra_StockChecking(out string processInfo, pm_Transfer P)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pExtra_StockChecking", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = P.unit_id;
                    cmd.Parameters.Add("@trf_to", SqlDbType.VarChar, 16).Value = P.TransferToUnit;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 16).Value = P.login_id;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = cmd.Parameters["@result"].Value.ToString();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
            }
            return processInfo;
        }
        public string Extra_StockTransferToUnit(out string processInfo,pm_Transfer p)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pExtra_StockTransferToUnit", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value =p.unit_id;
                    cmd.Parameters.Add("@trf_to", SqlDbType.VarChar, 16).Value = p.TransferToUnit;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 16).Value = p.login_id;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = cmd.Parameters["@result"].Value.ToString();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
            }
            return processInfo;
        }
        public void Extra_SoldToCustomer(out string processInfo, pm_Transfer p)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pExtra_SoldToCustomer", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@sold_to", SqlDbType.VarChar, 10).Value = p.TransferToUnit;
                    cmd.Parameters.Add("@party_id", SqlDbType.VarChar, 16).Value = p.party_id;
                    cmd.Parameters.Add("@party_account_id", SqlDbType.VarChar, 16).Value = p.party_account_no;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = cmd.Parameters["@result"].Value.ToString();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
            }
        }

        #endregion
        public void Tran_Posting(out string processInfo, pm_Transfer p)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
            {
                using (SqlCommand cmd = new SqlCommand("pTran_Posting", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@nestedTranFlag", SqlDbType.VarChar, 1).Value = "Y";
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@tran_id", SqlDbType.VarChar, 16).Value = p.transaction_id;
                    cmd.Parameters.Add("@tran_type", SqlDbType.VarChar, 50).Value = p.tran_type;
                    cmd.Parameters.Add("@vendor_id", SqlDbType.VarChar, 16).Value = p.vendor_id;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                    cmd.Parameters.Add("@voucher_no", SqlDbType.VarChar, 50).Value = "N/A";
                    cmd.Parameters["@voucher_no"].Direction = ParameterDirection.InputOutput;
                    try
                    {

                        cmd.ExecuteNonQuery();
                        processInfo = cmd.Parameters["@voucher_no"].Value.ToString();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
            }
        }
        public DataSet Tran_InterUnitsInsert(out string processInfo, pm_Transfer p)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("pTran_InterUnitsInsert", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1500;
                        cmd.Parameters.Add("@logic", SqlDbType.NVarChar, 50).Value = p.logic;
                        cmd.Parameters.Add("@trf_from", SqlDbType.VarChar, 10).Value = p.unit_id;
                        cmd.Parameters.Add("@trf_to", SqlDbType.VarChar, 10).Value = p.TransferToUnit;
                        cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 7).Value = p.item_id;
                        cmd.Parameters.Add("@master_key_id", SqlDbType.VarChar, 16).Value = p.master_key_id;
                        cmd.Parameters.Add("@qty", SqlDbType.Int).Value = p.qty;
                        cmd.Parameters.Add("@cart_name", SqlDbType.NVarChar, 15).Value = p.cart_name;
                        cmd.Parameters.Add("@GenFrom", SqlDbType.NVarChar, 10).Value = p.GenFrom;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
                        cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                        cmd.Parameters.Add("@trf_id", SqlDbType.VarChar, 16).Value = p.transfer_id;
                        cmd.Parameters["@trf_id"].Direction = ParameterDirection.InputOutput;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                            con.Close();
                            processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                            processInfo = processInfo+"|"+(string)cmd.Parameters["@trf_id"].Value.ToString();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Fail : " + sqlEx.Message;
            }
            return ds;
        }
        public void AutoEntryForHospitalConsumption(out string processInfo, pm_Transfer p)
        {

            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
            {
                using (SqlCommand cmd = new SqlCommand("pAutoEntryForHospitalConsumption", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@trfToUnit", SqlDbType.VarChar, 10).Value = p.TransferToUnit;
                    cmd.Parameters.Add("@sale_Inv_No", SqlDbType.VarChar, 20).Value = p.transaction_id;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "-";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {

                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
            }
        }
        public void Transfer_DeleteRecord(out string processInfo, pm_Transfer p)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
            {
                using (SqlCommand cmd = new SqlCommand("pTransfer_DeleteRecord", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@trf_id", SqlDbType.VarChar, 16).Value = p.transfer_id;
                    cmd.Parameters.Add("@from", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@to", SqlDbType.VarChar, 16).Value = p.TransferToUnit;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 16).Value = p.item_id;
                    cmd.Parameters.Add("@master_key_id", SqlDbType.VarChar, 16).Value = p.master_key_id;
                    cmd.Parameters.Add("@qty", SqlDbType.VarChar, 16).Value = p.qty;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "N/A";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {

                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
            }
        }
        public DataSet StockAtOtherStores(out string processInfo, pm_Stocks p)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
            {
                SqlCommand cmd = new SqlCommand("pProductAtOtherStores", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 2500;
                cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 10).Value = p.item_id;
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                 
                    processInfo = "No Error";
                }
                catch (SqlException sqlEx)
                {
                    processInfo = sqlEx.ToString();
                }
                finally { con.Close(); }
            }
            return ds;
        }
        public DataSet Dispatch_Queries(out string processInfo, pm_Transfer p)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
            {
                SqlCommand cmd = new SqlCommand("pDispatch_Queries", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 2500;
                cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                cmd.Parameters.Add("@trf_from", SqlDbType.VarChar).Value = p.TransferFromUnit;
                cmd.Parameters.Add("@trf_to", SqlDbType.VarChar).Value = p.TransferToUnit;
                cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = p.logic;
                cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = p.prm_1;
                cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = p.prm_2;
                cmd.Parameters.Add("@prm_3", SqlDbType.Decimal).Value = p.prm_3;
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    processInfo = "No Error";
                }
                catch (SqlException sqlEx)
                {
                    processInfo = sqlEx.ToString();
                }
                finally { con.Close(); }
            }
            return ds;
        }
        public string Dispatch5_4(out string processInfo,  pm_Transfer p)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
            {
                SqlCommand cmd = new SqlCommand("pDispatch5_4", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 2500;
                cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                cmd.Parameters.Add("@order_No", SqlDbType.VarChar, 16).Value = p.orderNo;
                cmd.Parameters.Add("@trf_to", SqlDbType.VarChar, 10).Value = p.TransferToUnit;
                cmd.Parameters.Add("@OrderFor", SqlDbType.Money).Value = p.orderFor;
                cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
                cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                try
                {
                    cmd.ExecuteNonQuery();
                    processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                }
                catch (SqlException sqlEx)
                {
                    processInfo = "Error Found   : " + sqlEx.Message;
                }
                finally { con.Close(); }
            }
            return processInfo;
        }
        public void Dispatch_UpdateQueries(out string processInfo, pm_Transfer p)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
            {
                SqlCommand cmd = new SqlCommand("pDispatch_UpdateQueries", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 2500;
                cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                cmd.Parameters.Add("@logic", SqlDbType.VarChar, 30).Value = p.logic;
                cmd.Parameters.Add("@tran_id", SqlDbType.VarChar, 16).Value = p.transfer_id;
                cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 30).Value = p.prm_1;
                cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 30).Value = p.prm_2;
                cmd.Parameters.Add("@prm_3", SqlDbType.VarChar, 30).Value = p.prm_3;
                cmd.Parameters.Add("@qty", SqlDbType.Int).Value = p.qty;
                cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "-";
                cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                }
                catch (SqlException sqlEx)
                {
                    processInfo = "Error Found   : " + sqlEx.Message;
                }
                finally { con.Close(); }
            }
        }
        public void Receive_TrfId_InStock(out string processInfo,pm_Transfer p)
        {
            SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy);
            SqlCommand cmd = new SqlCommand("pReceive_TrfId_InStock", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@trf_id", SqlDbType.VarChar, 16).Value = p.transfer_id;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
            cmd.Parameters.Add("@voucher_no", SqlDbType.VarChar, 100).Value = "N/A";
            cmd.Parameters["@voucher_no"].Direction = ParameterDirection.InputOutput;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                processInfo = (string)cmd.Parameters["@voucher_no"].Value.ToString();
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Error Found   : " + sqlEx.Message;
            }
            finally { con.Close(); }
        }
        public void BulkTransaction_Processing(out string processInfo, pm_Transfer p)
        {
            SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy);
            SqlCommand cmd = new SqlCommand("pBulkTransaction_Processing", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@ToUnit", SqlDbType.VarChar, 10).Value = p.TransferToUnit;
            cmd.Parameters.Add("@processLogic", SqlDbType.VarChar, 40).Value = p.logic;
            cmd.Parameters.Add("@indent_no", SqlDbType.VarChar, 16).Value = p.transaction_id;
            cmd.Parameters.Add("@tran_type", SqlDbType.VarChar, 40).Value = p.tran_type;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "N/A";
            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                processInfo = (string)cmd.Parameters["@result"].Value.ToString();
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Error Found   : " + sqlEx.Message;
            }
            finally { con.Close(); }
        }
    }// Second Last
}