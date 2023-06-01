using Crm_Api.Models;
using Crm_Api.Repository.ApplicationResource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Crm_Api.Repository.Pharmacy
{
    public class OnlineOrder
    {
        public dataSet OnlineOrderQueries(ipOnlineOrder objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pOnlineOrder_Queries",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@sh_code",SqlDbType.VarChar,10).Value = objBO.sh_code;
                    cmd.Parameters.Add("@order_no",SqlDbType.VarChar,16).Value = objBO.order_no;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = objBO.prm_2;
                    cmd.Parameters.Add("@prm_3", SqlDbType.VarChar, 50).Value = objBO.prm_3;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 30).Value = objBO.Logic;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;
                        dsObj.Msg = "Success";
                        con.Close();
                    }
                    catch (SqlException sqlEx)
                    {
                        dsObj.ResultSet = null;
                        dsObj.Msg = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return dsObj;
                }
            }
        }
        public string OnlineOrderInsert(ipOnlineOrder objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pOnlineOrder_Insert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 20).Value = objBO.card_no;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.unit_id;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar,7).Value = objBO.item_id;
                    cmd.Parameters.Add("@new_med", SqlDbType.VarChar, 50).Value = objBO.new_med;
                    cmd.Parameters.Add("@qty", SqlDbType.Int).Value = objBO.qty;
                    cmd.Parameters.Add("@del_date", SqlDbType.DateTime).Value = objBO.del_date;
                    cmd.Parameters.Add("@del_time", SqlDbType.VarChar, 10).Value = objBO.del_time;
                    cmd.Parameters.Add("@ref_by", SqlDbType.VarChar, 50).Value = objBO.ref_by;
                    cmd.Parameters.Add("@remark", SqlDbType.VarChar, 100).Value = objBO.remark;
                    cmd.Parameters.Add("@file_path", SqlDbType.VarChar, 100).Value = objBO.file_path;
                    cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = objBO.order_no;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 20).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        con.Close();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
        public string RCMCompleteOrder(ipOnlineOrder objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pRCM_CompleteOrder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 20).Value = objBO.unit_id;
                    cmd.Parameters.Add("@trf_to", SqlDbType.VarChar, 10).Value = objBO.trf_to;
                    cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 7).Value = objBO.order_no;
                    cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 50).Value = objBO.card_no;
                    cmd.Parameters.Add("@del_date", SqlDbType.Int).Value = objBO.del_date;
                    cmd.Parameters.Add("@del_time", SqlDbType.DateTime).Value = objBO.del_time;
                    cmd.Parameters.Add("@remark", SqlDbType.VarChar, 10).Value = objBO.remark;
                    cmd.Parameters.Add("@ref_by", SqlDbType.VarChar, 50).Value = objBO.ref_by;
                    cmd.Parameters.Add("@old_order_no", SqlDbType.VarChar, 100).Value = objBO.old_order_no;
                    cmd.Parameters.Add("@sale_inv_no", SqlDbType.VarChar, 100).Value = objBO.sale_inv_no;
                    cmd.Parameters.Add("@promo_flag", SqlDbType.VarChar, 100).Value = objBO.promo_flag;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 16).Value = objBO.prm_1;
                    cmd.Parameters.Add("@completedBy", SqlDbType.VarChar, 10).Value = objBO.login_id;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 20).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        con.Close();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
        public string UpdateTablesInfo(ipOnlineOrder objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pUpdateTablesInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.unit_id;
                    cmd.Parameters.Add("@tran_id", SqlDbType.VarChar, 16).Value = objBO.tran_id;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 50).Value = objBO.item_id;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar,50).Value = objBO.prm_2;
                    cmd.Parameters.Add("@prm_3", SqlDbType.VarChar,50).Value = objBO.prm_3;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.login_id;                                    
                    try
                    {
                        con.Close();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                            processInfo = "Success";
                        else
                            processInfo = "Failed";
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
        public dataSet OrderTrackingReport (OrderReport objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pOrderTrackingReport",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar,10).Value = objBO.unit_id;
                    cmd.Parameters.Add("@from", SqlDbType.VarChar, 16).Value = objBO.from;
                    cmd.Parameters.Add("@to", SqlDbType.VarChar, 50).Value = objBO.to;
                    cmd.Parameters.Add("@prm_1", SqlDbType.NVarChar, 30).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.NVarChar, 16).Value = objBO.prm_2;
                    cmd.Parameters.Add("@logic", SqlDbType.NVarChar, 30).Value = objBO.Logic;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dsObj.ResultSet = ds;
                        dsObj.Msg = "Success";
                        con.Close();
                    }
                    catch (SqlException sqlEx)
                    {
                        dsObj.ResultSet = null;
                        dsObj.Msg = sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return dsObj;
                }
            }
        }

        //public dataSet SaltQueries(ipOnlineOrder objBO)
        //{
        //    dataSet dsObj = new dataSet();
        //    using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy)) 
        //    {
        //        using (SqlCommand cmd = new SqlCommand("pSaltQueries",con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandTimeout = 2500;
        //            cmd.Parameters.Add("@Item_id", SqlDbType.VarChar, 10).Value = objBO.Item_id;
        //            cmd.Parameters.Add("@salt_code", SqlDbType.VarChar,30).Value = objBO.salt_code;                   
        //            cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = objBO.prm_1;
        //            cmd.Parameters.Add("@prm_2", SqlDbType.Decimal,12).Value = objBO.prm_2;
        //            cmd.Parameters.Add("@logic", SqlDbType.VarChar,40).Value = objBO.Logic;
        //            try
        //            {
        //                con.Open();
        //                DataSet ds = new DataSet();
        //                SqlDataAdapter da = new SqlDataAdapter(cmd);
        //                da.Fill(ds);
        //                dsObj.ResultSet = ds;
        //                dsObj.Msg = "Success";
        //                con.Close();
        //            }
        //            catch (SqlException sqlEx)
        //            {
        //                dsObj.ResultSet = null;
        //                dsObj.Msg = sqlEx.Message;
        //            }
        //            finally { con.Close(); }
        //            return dsObj;
        //        }
        //    }
        //}
    }
}