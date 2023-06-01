using Crm_Api.Repository.ApplicationResource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static Crm_Api.Models.Common;
//using static Crm_Api.Models.CustomerData;

namespace Crm_Api.Repository.Pharmacy
{
    public class CustomerData
    {
        string _result = string.Empty;
        //public DataSet Menu_RightsQueries(out string processInfo, pm_Menu_RightsQueries p)
        //{
        //    DataSet ds = new DataSet();
        //    SqlConnection con = new SqlConnection(GlobalConfig.strConnMGM);
        //    SqlCommand cmd = new SqlCommand("pMenu_RightsQueries", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandTimeout = 2500;
        //    cmd.Parameters.Add("@AppName", SqlDbType.VarChar, 50).Value = p.appName;
        //    cmd.Parameters.Add("@emp_code", SqlDbType.VarChar, 10).Value = p.emp_code;
        //    cmd.Parameters.Add("@element_name", SqlDbType.VarChar, 50).Value = p.element_name;
        //    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = p.prm_1;
        //    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = p.prm_2;
        //    cmd.Parameters.Add("@prm_3", SqlDbType.Int).Value = p.prm_3;
        //    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 30).Value = p.Logic;
        //    try
        //    {
        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //        con.Close();
        //        processInfo = "No Error";
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        processInfo = sqlEx.ToString();
        //    }
        //    finally { con.Close(); }
        //    return ds;
        //}
        //public string Insert_Modify_Menu_Rights(pm_Insert_Modify_menu_master p)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(GlobalConfig.strConnMGM))
        //        {
        //            using (SqlCommand com = new SqlCommand("dbo.Insert_Modify_Menu_Rights", con))
        //            {

        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.Add("@AppName", SqlDbType.VarChar, 50).Value = p.AppName;
        //                com.Parameters.Add("@element_name", SqlDbType.VarChar, 50).Value = p.element_name;
        //                com.Parameters.Add("@emp_code", SqlDbType.VarChar, 20).Value = p.empCode;
        //                com.Parameters.Add("@isActive", SqlDbType.Char, 50).Value = p.isActive;
        //                com.Parameters.Add("@login_id", SqlDbType.VarChar, 20).Value = p.loginid;
        //                com.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
        //                com.Parameters["@result"].Direction = ParameterDirection.InputOutput;
        //                con.Open();
        //                com.ExecuteNonQuery();
        //                _result = com.Parameters["@result"].Value.ToString();

        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        _result = "Error Found : " + sqlEx.Message;
        //    }
        //    return _result;
        //}    
        public DataSet RCM_Queries(out string processInfo, cm2 p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            try
            {
                using (SqlCommand cmd = new SqlCommand("pRCM_Queries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.tran_id;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 7).Value = p.item_id;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = p.prm_1;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = p.dtFrom;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = p.Logic;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        if (con != null && con.State == ConnectionState.Closed)
                            con.Open();
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
        public DataSet OrderInfo_ForUnits(cm2 p)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
                SqlCommand cmd = new SqlCommand("pOrderInfo_ForUnits", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1500;
                cmd.Parameters.Add("unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                cmd.Parameters.Add("order_no", SqlDbType.VarChar, 16).Value = p.tran_id;
                cmd.Parameters.Add("what", SqlDbType.VarChar, 10).Value = p.prm_1;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds;
            }
            catch (Exception)
            {
                return ds;
            }
        }
        public string Insert_CustCall_Log(out string processInfo, regulareQueries p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("Insert_CustCall_Log", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@Auto_id", SqlDbType.BigInt).Value = p.uniqeID;
            cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 16).Value = p.card_no;
            cmd.Parameters.Add("@call_type", SqlDbType.NVarChar, 20).Value = p.callType;
            cmd.Parameters.Add("@login_id", SqlDbType.NVarChar, 20).Value = p.loginID;
            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 200).Value = p.remark;
            cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = p.logic;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                processInfo = (string)cmd.Parameters["@result"].Value.ToString();
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Error Found: " + sqlEx.Message;
            }
            finally { con.Close(); }
            return processInfo;
        }
        public DataSet RCM_SmallQueries(out string processInfo, regulareQueries p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            try
            {
                using (SqlCommand cmd = new SqlCommand("pRCM_SmallQueries",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@card_no", SqlDbType.VarChar,20).Value = p.card_no;
                    cmd.Parameters.Add("@date", SqlDbType.VarChar, 10).Value = p.date;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@time", SqlDbType.VarChar, 10).Value = p.time;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = p.logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = p.loginID;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        if (con != null && con.State == ConnectionState.Closed)
                            con.Open();
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
        public DataSet OnlineOrder_Queries(out string processInfo, cm2 p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            try
            {
                using (SqlCommand cmd = new SqlCommand("pOnlineOrder_Queries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@sh_code", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 20).Value = p.tran_id;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = p.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = p.prm_2;
                    cmd.Parameters.Add("@prm_3", SqlDbType.Int).Value = p.prm_3;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = p.Logic;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        if (con != null && con.State == ConnectionState.Closed)
                            con.Open();
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
        public string OnlineOrder_Insert(out string processInfo, OrderFromWebSite p)
        {
            string msg = string.Empty;
            string Ret_order_no = string.Empty;
            if (p.logic == "Upload" && p.data.Length > 0)
            {
                string[] ext = p.file_path.Split('.');
                string uniqueFileName = ext[0].Replace('/', '_') + "_" + System.DateTime.Now.ToString("ddMMyyyyhhMMss");
                p.file_path = "I:\\HealthCard\\Prescription\\" + p.card_no + "\\" + uniqueFileName + "." + ext[ext.Length - 1].Trim();
                UpLoadHealthCardScanedDocument(out msg, p.file_path, p.data);
                if (msg != "OK")
                { p.file_path = string.Empty; }
            }
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pOnlineOrder_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 20).Value = p.card_no;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 7).Value = p.item_id;
            cmd.Parameters.Add("@new_med", SqlDbType.VarChar, 50).Value = p.new_med;
            cmd.Parameters.Add("@qty", SqlDbType.Int).Value = p.qty;
            cmd.Parameters.Add("@del_date", SqlDbType.DateTime).Value = p.del_date;
            cmd.Parameters.Add("@del_time", SqlDbType.VarChar, 10).Value = p.del_time;
            cmd.Parameters.Add("@ref_by", SqlDbType.VarChar, 50).Value = p.ref_by;
            cmd.Parameters.Add("@remark", SqlDbType.VarChar, 100).Value = p.remark;
            cmd.Parameters.Add("@logic", SqlDbType.VarChar, 20).Value = p.logic;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add("@file_path", SqlDbType.VarChar, 100).Value = p.file_path;
            cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 20).Value = p.order_no;
            cmd.Parameters["@order_no"].Direction = ParameterDirection.InputOutput;
            try
            {
                cmd.ExecuteNonQuery();
                processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                Ret_order_no = (string)cmd.Parameters["@order_no"].Value.ToString();
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Error Found   : " + sqlEx.Message;
            }
            finally { con.Close(); }
            return Ret_order_no;
        }
        public string UpLoadHealthCardScanedDocument(out string msg, string doc_path, byte[] data)
        {
            string[] t = doc_path.Split('\\');
            string Location = "";
            for (int i = 0; i < t.Length - 1; i++)
            {
                Location += t[i] + "\\";
            }
            try
            {
                if (System.IO.Directory.Exists(Location))
                {
                    System.IO.File.WriteAllBytes(doc_path, data);
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Location);
                    System.IO.File.WriteAllBytes(doc_path, data);
                }
                msg = "OK";
            }
            catch (Exception ex) { msg = ex.Message; }
            return doc_path;
        }
        public string RCM_ReOrderPendings(out string processInfo, customerOrders p)
        {
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);

            try
            {
                using (SqlCommand cmd = new SqlCommand("pRCM_ReOrderPendings", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 20).Value = p.card_no;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
                    cmd.Parameters.Add("@old_order_no", SqlDbType.VarChar, 16).Value = p.old_order_no;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
                    cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 7).Value = p.item_id;
                    cmd.Parameters.Add("@new_med", SqlDbType.VarChar, 50).Value = p.new_med;
                    cmd.Parameters.Add("@qty", SqlDbType.Int).Value = p.qty;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 30).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.order_no;
                    cmd.Parameters["@order_no"].Direction = ParameterDirection.InputOutput;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        if (con != null && con.State == ConnectionState.Closed)
                            con.Open();
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        processInfo += "|" + (string)cmd.Parameters["@order_no"].Value.ToString();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                processInfo = "Fail : " + sqlEx.Message;
            }
            finally { con.Close(); }
            return processInfo;
        }
        public void Mobile_GetCoupons(out string processInfo, customerOrders p)
        {
            string couponCode = ""; string result = "";
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("MobileAppDb.dbo.pMobile_GetCoupons", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@mobileNo", SqlDbType.VarChar, 20).Value = p.mobileno;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = p.login_id;
            cmd.Parameters.Add("@ByApp", SqlDbType.VarChar, 50).Value = "WithoutApp";
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 50).Value = p.unit_id;
            cmd.Parameters.Add("@couponCode", SqlDbType.VarChar, 10).Value = "";
            cmd.Parameters["@couponCode"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
            cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                couponCode = (string)cmd.Parameters["@couponCode"].Value.ToString();
                result = (string)cmd.Parameters["@result"].Value.ToString();
                processInfo = result + "|" + couponCode;
            }
            catch (SqlException sqlEx)
            {
                processInfo = sqlEx.Message + result;
            }
            finally { con.Close(); }

        }
        public void Mobile_AuthenticateCoupon(out string processInfo, customerOrders p)
        {

            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("MobileAppDb.dbo.pMobile_AuthenticateCoupon", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@division", SqlDbType.VarChar, 16).Value = p.division;
            cmd.Parameters.Add("@mobileNo", SqlDbType.VarChar, 10).Value = p.mobileno;
            cmd.Parameters.Add("@couponCode", SqlDbType.VarChar, 15).Value = p.couponNo;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
        public string RCM_CompleteMobileOrder(out string processInfo, customerOrders p)
        {

            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pRCM_CompleteMobileOrder", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.order_no;
            cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 20).Value = p.card_no;
            cmd.Parameters.Add("@del_date", SqlDbType.DateTime).Value = p.delivery_date;
            cmd.Parameters.Add("@del_time", SqlDbType.VarChar, 10).Value = p.delivery_time;
            cmd.Parameters.Add("@remark", SqlDbType.VarChar, 500).Value = p.remarks;
            cmd.Parameters.Add("@ref_by", SqlDbType.VarChar, 50).Value = p.ref_by;
            cmd.Parameters.Add("@old_order_no", SqlDbType.VarChar, 16).Value = p.old_order_no;
            cmd.Parameters.Add("@sale_inv_no", SqlDbType.VarChar, 16).Value = p.sale_inv_no;
            cmd.Parameters.Add("@promo_flag", SqlDbType.VarChar, 1).Value = p.promo_flag;
            cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 30).Value = p.prm_1;
            cmd.Parameters.Add("@logic", SqlDbType.VarChar, 20).Value = p.logic;
            cmd.Parameters.Add("@completedBy", SqlDbType.VarChar, 10).Value = p.login_id;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
            return processInfo;
        }
        public void Mark_OrderDelivered(out string processInfo, customerOrders p)
        {

            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pMark_OrderDelivered", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.order_no;
            cmd.Parameters.Add("@deld_date", SqlDbType.DateTime).Value = p.delivery_date;
            cmd.Parameters.Add("@deld_time", SqlDbType.VarChar, 10).Value = p.delivery_time;
            cmd.Parameters.Add("@deld_by", SqlDbType.VarChar, 10).Value = p.delivery_by;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 30).Value = "";
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
        public string InsertRegularOrder(out string processInfo, customerOrders p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pInsertRegularOrder", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 20).Value = p.card_no;
            cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 7).Value = p.item_id;
            cmd.Parameters.Add("@new_med", SqlDbType.VarChar, 50).Value = p.new_med;
            cmd.Parameters.Add("@rec_flag", SqlDbType.Char, 1).Value = p.rec_flag;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
            cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = p.logic;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
            return processInfo;
        }
        public void Insert_OrderRemark(out string processInfo, customerOrders p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pInsert_OrderRemark", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@RemarkFrom", SqlDbType.VarChar, 50).Value = p.remarkfrom;
            cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.order_no;
            cmd.Parameters.Add("@remark", SqlDbType.VarChar, 200).Value = p.remarks;
            cmd.Parameters.Add("@sale_inv_no", SqlDbType.VarChar, 10).Value = p.sale_inv_no;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
        public void Insert_CustHoldSalesInfo(out string processInfo, customerOrders p)
        {
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pInsert_CustHoldSalesInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.order_no;
            cmd.Parameters.Add("@sale_inv_no", SqlDbType.VarChar, 16).Value = p.sale_inv_no;
            cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 10).Value = p.item_id;
            cmd.Parameters.Add("@master_key_id", SqlDbType.VarChar, 16).Value = p.master_key_id;
            cmd.Parameters.Add("@qty", SqlDbType.Int).Value = p.qty;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
            cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = p.logic;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
        public void Insert_Modify_custOrderInfo(out string processInfo, customerOrders p)
        {
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("Insert_Modify_custOrderInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.order_no;
            cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 7).Value = p.item_id;
            cmd.Parameters.Add("@old_item_id", SqlDbType.VarChar, 7).Value = p.old_item_id;
            cmd.Parameters.Add("@new_med", SqlDbType.VarChar, 50).Value = p.new_med;
            cmd.Parameters.Add("@qty", SqlDbType.Int).Value = p.qty;
            cmd.Parameters.Add("@cancel_flag", SqlDbType.Char, 1).Value = p.cancel_flag;
            cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = p.logic;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
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
        public void UpdateTablesInfo(out string processInfo, cm1 p)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pUpdateTablesInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 500).Value = p.unit_id;
                    cmd.Parameters.Add("@tran_id", SqlDbType.VarChar, 20).Value = p.tran_id;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = p.Logic;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = p.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = p.prm_2;
                    cmd.Parameters.Add("@prm_3", SqlDbType.VarChar, 50).Value = p.prm_3;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = p.login_id;
                    try
                    {
                        if (con != null && con.State == ConnectionState.Closed)
                            con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                            processInfo = "Successfully Updated.";
                        else
                            processInfo = "Fail To Update";

                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = sqlEx.ToString();
                    }
                    finally { con.Close(); }
                }
            }
        }
        public DataSet Schedule_Queries(out string processInfo, cm3 p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pSchedule_Queries", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@unit_id", SqlDbType.NVarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@emp_code", SqlDbType.VarChar, 20).Value = p.empCode;
            cmd.Parameters.Add("@logic", SqlDbType.NVarChar, 50).Value = p.Logic;
            cmd.Parameters.Add("@prm_1", SqlDbType.NVarChar, 40).Value = p.prm_1;
            cmd.Parameters.Add("@prm_2", SqlDbType.NVarChar, 40).Value = p.prm_2;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                processInfo = "No Error";
            }
            catch (SqlException sqlEx)
            {
                processInfo = sqlEx.ToString();
            }
            finally { con.Close(); }
            return ds;
        }

        public DataSet OrderTrackingReport(out string processInfo, cm3 p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pOrderTrackingReport", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@unit_id", SqlDbType.NVarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@from", SqlDbType.VarChar, 10).Value = p.dtFrom;
            cmd.Parameters.Add("@to", SqlDbType.VarChar, 10).Value = p.dtTo;
            cmd.Parameters.Add("@logic", SqlDbType.NVarChar, 30).Value = p.Logic;
            cmd.Parameters.Add("@prm_1", SqlDbType.NVarChar, 30).Value = p.prm_1;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                processInfo = "No Error";
            }
            catch (SqlException sqlEx)
            {
                processInfo = sqlEx.ToString();
            }
            finally { con.Close(); }
            return ds;
        }

        public DataSet RCMCall_Report(out string processInfo, cm3 p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pRCMCall_Report", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@from", SqlDbType.VarChar, 10).Value = p.dtFrom;
            cmd.Parameters.Add("@to", SqlDbType.VarChar, 10).Value = p.dtTo;
            cmd.Parameters.Add("@logic", SqlDbType.NVarChar, 30).Value = p.Logic;
            cmd.Parameters.Add("@prm_1", SqlDbType.NVarChar, 30).Value = p.prm_1;
            cmd.Parameters.Add("@prm_2", SqlDbType.NVarChar, 30).Value = p.prm_2;
            cmd.Parameters.Add("@login_id", SqlDbType.NVarChar, 10).Value = p.login_id;
            try
            {
                if (con != null && con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                processInfo = "No Error";
            }
            catch (SqlException sqlEx)
            {
                processInfo = sqlEx.ToString();
            }
            finally { con.Close(); }
            return ds;
        }
        public DataSet MultiPurpose_AnalysisQueries(out string processInfo, cm3 p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pMultiPurpose_AnalysisQueries", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@from", SqlDbType.VarChar, 10).Value = p.dtFrom;
            cmd.Parameters.Add("@to", SqlDbType.VarChar, 10).Value = p.dtTo;
            cmd.Parameters.Add("@logic", SqlDbType.NVarChar, 30).Value = p.Logic;
            cmd.Parameters.Add("@prm_1", SqlDbType.NVarChar, 30).Value = p.prm_1;
            cmd.Parameters.Add("@prm_2", SqlDbType.NVarChar, 30).Value = p.prm_2;
            cmd.Parameters.Add("@login_id", SqlDbType.NVarChar, 10).Value = p.login_id;
            try
            {
                if (con != null && con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                processInfo = "No Error";
            }
            catch (SqlException sqlEx)
            {
                processInfo = sqlEx.ToString();
            }
            finally { con.Close(); }
            return ds;
        }
        public void RCMCreateDataLog(out string processInfo, regulareQueries p)
        {
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pRCM_CreateDataLog", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@card_no", SqlDbType.VarChar).Value = p.card_no;
            cmd.Parameters.Add("@rmd_date", SqlDbType.DateTime).Value = p.rmd_date;
            cmd.Parameters.Add("@rmd_time", SqlDbType.VarChar).Value = p.rmd_time;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar).Value = p.unit_id;
            cmd.Parameters.Add("@remark", SqlDbType.VarChar).Value = p.remark;
            cmd.Parameters.Add("@lastcallinfo", SqlDbType.VarChar).Value = p.lastcallinfo;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar).Value = p.loginID;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = p.logic;
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

        public DataSet RCM_ProductInfo(out string processInfo, customerOrders p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pRCM_ProductInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@logic", SqlDbType.VarChar, 20).Value = p.logic;
            cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = p.prm_1;
            cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 20).Value = p.card_no;
            cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 20).Value = p.order_no;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
            try
            {
                if (con != null && con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                processInfo = "No Error";
            }
            catch (SqlException sqlEx)
            {
                processInfo = sqlEx.ToString();
            }
            finally { con.Close(); }
            return ds;
        }
        public void RCM_OnCallOrder(out string processInfo, customerOrders p)
        {
            SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD);
            SqlCommand cmd = new SqlCommand("pRCM_OnCallOrderWeb", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 20).Value = p.card_no;
            cmd.Parameters.Add("@cust_name", SqlDbType.VarChar, 80).Value = p.cust_name;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = p.order_no; 
            cmd.Parameters.Add("@prod_code", SqlDbType.VarChar, 50).Value = p.item_id;
            cmd.Parameters.Add("@delivery_date", SqlDbType.DateTime).Value = p.delivery_date;
            cmd.Parameters.Add("@delivery_time", SqlDbType.VarChar, 20).Value = p.delivery_time;
            cmd.Parameters.Add("@qty", SqlDbType.Int).Value = p.qty;
            cmd.Parameters.Add("@new_med", SqlDbType.VarChar, 50).Value = p.newProductName;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "-";
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
        #region HealthCard Methods
        public DataSet CallingCard_queries(out string processInfo, cm2 p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnMGM);
            SqlCommand cmd = new SqlCommand("pCallingCard_queries", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = p.unit_id;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = p.login_id;
            cmd.Parameters.Add("@logic", SqlDbType.VarChar, 20).Value = p.Logic;
            cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 40).Value = p.prm_1;
            cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 40).Value = p.prm_2;
            cmd.Parameters.Add("@prm_3", SqlDbType.VarChar, 40).Value = p.prm_3;
            try
            {
                if (con != null && con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                processInfo = "No Error";
            }
            catch (SqlException sqlEx)
            {
                processInfo = sqlEx.ToString();
            }
            finally { con.Close(); }
            return ds;
        }

        public DataSet GetACardInfo(out string processInfo, cm1 p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnMGM);
            SqlCommand cmd = new SqlCommand("pGetaHealthCardInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1500;

            SqlParameter prm_Card_No = new SqlParameter("@Card_No", SqlDbType.VarChar, 20);
            prm_Card_No.Value = p.tran_id;
            prm_Card_No.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Card_No);
            if (con != null && con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            processInfo = "Success";
            return ds;
        }
        public string Card_Info_insert(CustomerInfo obj)
        {
            SqlConnection con = new SqlConnection(GlobalConfig.strConnMGM);
            SqlCommand cmd = new SqlCommand("Ins_CustCardInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1500;

            SqlParameter prm_logic = new SqlParameter("@logic", SqlDbType.VarChar, 6);
            prm_logic.Value = obj.logic;
            prm_logic.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_logic);

            SqlParameter prm_login_id = new SqlParameter("@loginid", SqlDbType.VarChar, 10);
            prm_login_id.Value = obj.login_id;
            prm_login_id.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_login_id);

            SqlParameter prm_CardType = new SqlParameter("@Card_Type", SqlDbType.VarChar, 25);
            prm_CardType.Value = obj.CardType;
            prm_CardType.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_CardType);

            SqlParameter prm_Cust_Name = new SqlParameter("@Cust_Name", SqlDbType.VarChar, 50);
            prm_Cust_Name.Value = obj.Cust_Name;
            prm_Cust_Name.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Cust_Name);

            SqlParameter prm_DOB = new SqlParameter("@DOB", SqlDbType.DateTime);
            prm_DOB.Value = obj.DOB;
            prm_DOB.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_DOB);

            SqlParameter prm_Area = new SqlParameter("@Area", SqlDbType.VarChar, 50);
            prm_Area.Value = obj.Area;
            prm_Area.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Area);

            SqlParameter prm_Locality = new SqlParameter("@Locality", SqlDbType.VarChar, 50);
            prm_Locality.Value = obj.Locality;
            prm_Locality.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Locality);

            SqlParameter prm_District = new SqlParameter("@District", SqlDbType.VarChar, 50);
            prm_District.Value = obj.District;
            prm_District.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_District);

            SqlParameter prm_State = new SqlParameter("@State", SqlDbType.VarChar, 25);
            prm_State.Value = obj.State;
            prm_State.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_State);

            SqlParameter prm_Country = new SqlParameter("@Country", SqlDbType.VarChar, 20);
            prm_Country.Value = obj.Country;
            prm_Country.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Country);

            SqlParameter prm_PIN = new SqlParameter("@PIN", SqlDbType.VarChar, 6);
            prm_PIN.Value = obj.PIN;
            prm_PIN.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_PIN);

            SqlParameter prm_PhoneNo = new SqlParameter("@PhoneNo", SqlDbType.VarChar, 20);
            prm_PhoneNo.Value = obj.PhoneNo;
            prm_PhoneNo.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_PhoneNo);

            SqlParameter prm_MobileNo = new SqlParameter("@MobileNo", SqlDbType.VarChar, 10);
            prm_MobileNo.Value = obj.MobileNo;
            prm_MobileNo.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_MobileNo);

            SqlParameter prm_email = new SqlParameter("@email", SqlDbType.VarChar, 100);
            prm_email.Value = obj.email;
            prm_email.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_email);

            SqlParameter prm_Spouse_Name = new SqlParameter("@Spouse_Name", SqlDbType.VarChar, 50);
            prm_Spouse_Name.Value = obj.Spouse_Name;
            prm_Spouse_Name.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Spouse_Name);

            SqlParameter prm_Spouse_DOB = new SqlParameter("@Spouse_DOB", SqlDbType.DateTime, 23);
            prm_Spouse_DOB.Value = obj.Spouse_DOB;
            prm_Spouse_DOB.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Spouse_DOB);

            SqlParameter prm_Child_Name_1 = new SqlParameter("@Child_Name_1", SqlDbType.VarChar, 50);
            prm_Child_Name_1.Value = obj.Child_Name_1;
            prm_Child_Name_1.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Child_Name_1);

            SqlParameter prm_Child_DOB_1 = new SqlParameter("@Child_DOB_1", SqlDbType.DateTime);
            prm_Child_DOB_1.Value = obj.Child_DOB_1;
            prm_Child_DOB_1.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Child_DOB_1);

            SqlParameter prm_Child_Name_2 = new SqlParameter("@Child_Name_2", SqlDbType.VarChar, 50);
            prm_Child_Name_2.Value = obj.Child_Name_2;
            prm_Child_Name_2.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Child_Name_2);

            SqlParameter prm_Child_DOB_2 = new SqlParameter("@Child_DOB_2", SqlDbType.DateTime);
            prm_Child_DOB_2.Value = obj.Child_DOB_2;
            prm_Child_DOB_2.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Child_DOB_2);

            SqlParameter prm_Child_Name_3 = new SqlParameter("@Child_Name_3", SqlDbType.VarChar, 50);
            prm_Child_Name_3.Value = obj.Child_Name_3;
            prm_Child_Name_3.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Child_Name_3);

            SqlParameter prm_Child_DOB_3 = new SqlParameter("@Child_DOB_3", SqlDbType.DateTime);
            prm_Child_DOB_3.Value = obj.Child_DOB_3;
            prm_Child_DOB_3.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Child_DOB_3);

            SqlParameter prm_Child_Name_4 = new SqlParameter("@Child_Name_4", SqlDbType.VarChar, 50);
            prm_Child_Name_4.Value = obj.Child_Name_4;
            prm_Child_Name_4.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Child_Name_4);

            SqlParameter prm_Child_DOB_4 = new SqlParameter("@Child_DOB_4", SqlDbType.DateTime);
            prm_Child_DOB_4.Value = obj.Child_DOB_4;
            prm_Child_DOB_4.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Child_DOB_4);

            SqlParameter prm_Child_Name_5 = new SqlParameter("@Child_Name_5", SqlDbType.VarChar, 50);
            prm_Child_Name_5.Value = obj.Child_Name_5;
            prm_Child_Name_5.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Child_Name_5);

            SqlParameter prm_Child_DOB_5 = new SqlParameter("@Child_DOB_5", SqlDbType.DateTime);
            prm_Child_DOB_5.Value = obj.Child_DOB_5;
            prm_Child_DOB_5.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Child_DOB_5);

            SqlParameter prm_Card_No = new SqlParameter("@Card_No", SqlDbType.VarChar, 20);
            prm_Card_No.Value = obj.Card_No;
            prm_Card_No.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Card_No);

            SqlParameter prm_Inst_Code = new SqlParameter("@Inst_Code", SqlDbType.VarChar, 10);
            prm_Inst_Code.Value = obj.Inst_Code;
            prm_Inst_Code.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(prm_Inst_Code);

            SqlParameter dist_type = new SqlParameter("@dist_type", SqlDbType.VarChar, 10);
            dist_type.Value = obj.DistType;
            dist_type.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dist_type);

            SqlParameter prm_result = new SqlParameter("@result", SqlDbType.VarChar, 40);
            prm_result.Value = "result ";
            prm_result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(prm_result);
            try
            {
                if (con != null && con.State == ConnectionState.Closed)
                    con.Open();
                cmd.ExecuteNonQuery();
                return (string)prm_result.Value;
            }
            catch (SqlException sqlEx)
            {
                return "Error Found   : " + sqlEx.Message;
            }
            finally
            {
                con.Close();
            }
        }

        //public string RCMCompleteOrder(List<RCMOrderInfo> objBO)
        //{
        //    string processInfo = string.Empty;
        //    string prm_1 = string.Empty;
        //    string unit_id = string.Empty;
        //    string trf_to = string.Empty;
        //    string order_no = string.Empty;
        //    string card_no = string.Empty;
        //    string del_date = string.Empty;
        //    string del_time = string.Empty;
        //    string remark = string.Empty;
        //    string ref_by = string.Empty;
        //    string old_order_no = string.Empty;
        //    string sale_inv_no = string.Empty;
        //    string promo_flag = string.Empty;   
        //    string home_delflag = string.Empty;          
        //    string NextCallDate = string.Empty;   
        //    string NextCallTime = string.Empty;   
        //    string MessageForUnit = string.Empty;   
        //    string LastCallInfo = string.Empty;   
        //    string CallType = string.Empty;   
        //    string UniqeID = string.Empty;   
        //    string login_id = string.Empty;
        //    string logic = string.Empty;
        //    if (objBO.Count > 0)
        //    {
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("ItemId", typeof(string));
        //        dt.Columns.Add("ItemName", typeof(string));
        //        dt.Columns.Add("NewMed", typeof(string));
        //        dt.Columns.Add("LastQty", typeof(int));
        //        dt.Columns.Add("Qty", typeof(int));
        //        foreach (RCMOrderInfo obj in objBO)
        //        {
        //            unit_id = obj.unit_id;
        //            login_id = obj.login_id;
        //            prm_1 = obj.prm_1;
        //            trf_to = obj.trf_to;
        //            order_no = obj.order_no;
        //            card_no = obj.card_no;
        //            del_date = obj.del_date;
        //            del_time = obj.del_time;
        //            remark = obj.remark;
        //            ref_by = obj.ref_by;
        //            sale_inv_no = obj.sale_inv_no;
        //            old_order_no = obj.old_order_no;
        //            promo_flag = obj.promo_flag;
        //            home_delflag = obj.home_delflag;
        //            NextCallDate = obj.NextCallDate;
        //            NextCallTime = obj.NextCallTime;
        //            MessageForUnit = obj.MessageForUnit;
        //            LastCallInfo = obj.LastCallInfo;
        //            UniqeID = obj.UniqeID;
        //            CallType = obj.CallType;
        //            logic = obj.logic;
        //            DataRow dr = dt.NewRow();
        //            dr["ItemId"] = obj.ItemId;
        //            dr["ItemName"] = obj.ItemName;
        //            dr["NewMed"] = obj.NewMed;
        //            dr["LastQty"] = obj.LastQty;
        //            dr["Qty"] = obj.Qty;
        //            dt.Rows.Add(dr);
        //        }
        //        using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("pRCMCompleteOrder",con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandTimeout = 2500;
        //                cmd.Parameters.AddWithValue("udt_CompleteOrder", dt);
        //                cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = unit_id;
        //                cmd.Parameters.Add("@trf_to", SqlDbType.VarChar, 50).Value = trf_to;
        //                cmd.Parameters.Add("@order_no", SqlDbType.VarChar, 16).Value = order_no;
        //                cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 20).Value = card_no;
        //                cmd.Parameters.Add("@del_date", SqlDbType.DateTime).Value = del_date;
        //                cmd.Parameters.Add("@del_time", SqlDbType.VarChar, 10).Value = del_time;
        //                cmd.Parameters.Add("@remark", SqlDbType.VarChar, 500).Value = remark;
        //                cmd.Parameters.Add("@ref_by", SqlDbType.VarChar, 50).Value = ref_by;
        //                cmd.Parameters.Add("@old_order_no", SqlDbType.VarChar, 16).Value = old_order_no;
        //                cmd.Parameters.Add("@sale_inv_no", SqlDbType.VarChar, 16).Value = sale_inv_no;
        //                cmd.Parameters.Add("@promo_flag", SqlDbType.VarChar, 1).Value = promo_flag;
        //                cmd.Parameters.Add("@home_delflag", SqlDbType.VarChar, 1).Value = home_delflag;
        //                cmd.Parameters.Add("@prm_1", SqlDbType.VarChar,30).Value = prm_1;
        //                cmd.Parameters.Add("@NextCallDate", SqlDbType.DateTime).Value = NextCallDate;
        //                cmd.Parameters.Add("@NextCallTime", SqlDbType.DateTime).Value = NextCallTime;
        //                cmd.Parameters.Add("@MessageForUnit", SqlDbType.VarChar,100).Value = MessageForUnit;
        //                cmd.Parameters.Add("@LastCallInfo", SqlDbType.VarChar,20).Value = LastCallInfo;
        //                cmd.Parameters.Add("@UniqeID", SqlDbType.Int).Value = UniqeID;
        //                cmd.Parameters.Add("@CallType", SqlDbType.VarChar,10).Value = CallType;
        //                cmd.Parameters.Add("@logic", SqlDbType.VarChar, 20).Value = logic;
        //                cmd.Parameters.Add("@completedBy", SqlDbType.VarChar, 10).Value = '-';
        //                cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = login_id;
        //                cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
        //                cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
        //                try
        //                {
        //                    con.Open();
        //                    cmd.ExecuteNonQuery();
        //                    processInfo = (string)cmd.Parameters["@result"].Value.ToString();
        //                    con.Close();
        //                }
        //                catch (SqlException sqlEx)
        //                {
        //                    processInfo = "Error Found : " + sqlEx.Message;
        //                }
        //                finally { con.Close(); }
        //                return processInfo;
        //            }
        //        }
        //    }
        //    return processInfo;
        //}

        public string RCMCompleteOrderNew(RCMOrderInfo objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pRCM_CompleteOrderNew", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.unit_id;
                    cmd.Parameters.Add("@trf_to", SqlDbType.VarChar, 10).Value = objBO.trf_to;
                    cmd.Parameters.Add("@order_no", SqlDbType.VarChar,16).Value = objBO.order_no;
                    cmd.Parameters.Add("@card_no", SqlDbType.VarChar, 20).Value = objBO.card_no;
                    cmd.Parameters.Add("@del_date", SqlDbType.DateTime).Value = objBO.del_date;
                    cmd.Parameters.Add("@del_time", SqlDbType.VarChar, 10).Value = objBO.del_time;                    
                    cmd.Parameters.Add("@rmd_date", SqlDbType.Date).Value = objBO.rmd_date;
                    cmd.Parameters.Add("@rmd_time", SqlDbType.VarChar).Value = objBO.rmd_time;
                    cmd.Parameters.Add("@callType", SqlDbType.VarChar).Value = objBO.callType;
                    cmd.Parameters.Add("@remark", SqlDbType.VarChar, 500).Value = objBO.remark;                    
                    cmd.Parameters.Add("@ref_by", SqlDbType.VarChar, 50).Value = objBO.ref_by;
                    cmd.Parameters.Add("@old_order_no", SqlDbType.VarChar, 16).Value = objBO.old_order_no;
                    cmd.Parameters.Add("@sale_inv_no", SqlDbType.VarChar, 16).Value = objBO.sale_inv_no;
                    cmd.Parameters.Add("@promo_flag", SqlDbType.VarChar, 1).Value = objBO.promo_flag;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar,50).Value = objBO.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar,500).Value = objBO.prm_2;
                    cmd.Parameters.Add("@completedBy", SqlDbType.VarChar, 10).Value = objBO.completedBy;                   
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar,20).Value = objBO.logic;
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
                        processInfo = "Error Found : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
        #endregion
    }
}


