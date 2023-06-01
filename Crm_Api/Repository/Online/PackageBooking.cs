
using Crm_Api.Models;
using Crm_Api.Repository.ApplicationResource;
using Crm_Api.Repository.Utilities;
using iTextSharp.text;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;


namespace Crm_Api.Repository.Online
{
    public class PackageBooking
    {
        public List<TransactionDetail> CallWebApiPayUMoneyByDate(out string ProcessInfo, ipTransaction obj)
        {
            List<TransactionDetail> trnLists = new List<TransactionDetail>();
            try
            {
                byte[] hash;
                string d = "DCqJFDrL" + "|" + "get_Transaction_Details" + "|" + obj.from + "|" + "8sIJ4ogpyz";
                var datab = Encoding.UTF8.GetBytes(d);
                using (SHA512 shaM = new SHA512Managed())
                {
                    hash = shaM.ComputeHash(datab);
                }
                string hasString = GetStringFromHash(hash);
                var client = new RestClient("https://info.payu.in/merchant/postservice?form=2");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("key", "DCqJFDrL");
                request.AddParameter("command", "get_Transaction_Details");
                request.AddParameter("var1", obj.from);
                request.AddParameter("var2", obj.to);
                request.AddParameter("hash", hasString);
                IRestResponse response = client.Execute(request);

                iPayUPaymentList payUResponse = JsonConvert.DeserializeObject<iPayUPaymentList>(response.Content, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Formatting = Formatting.None,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    FloatParseHandling = FloatParseHandling.Decimal
                });
                trnLists = payUResponse.Transaction_details;
                ProcessInfo = "Success";
            }
            catch (Exception ex)
            {
                ProcessInfo = ex.Message;
            }
            return trnLists;
        }
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2").ToLower());
            }
            return result.ToString();
        }
        public dataSet Online_DiagnosticPackageQueries(ipPackageQueries ipapp)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_MobileAppDb))
            {
                using (SqlCommand cmd = new SqlCommand("pOnline_DiagnosticPackageQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 16).Value = ipapp.unit_id;
                    cmd.Parameters.Add("@TokenNo", SqlDbType.VarChar, 10).Value = ipapp.TokenNo;
                    cmd.Parameters.Add("@BookingId", SqlDbType.VarChar, 50).Value = ipapp.BookingId;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = ipapp.fromdate;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = ipapp.todate;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = ipapp.prm_1;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = ipapp.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 200).Value = ipapp.Logic;
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
        public dataSet PackageConfirmation(ipPackageOnlineConfirmation ip)
        {
            dataSet dsResult = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_MobileAppDb))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand("pOnline_PackageConfirmation", con))
                {
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@BookingId", SqlDbType.VarChar, 50).Value = ip.BookingId;
                    cmd.Parameters.Add("@UnitId", SqlDbType.VarChar, 20).Value = ip.unit_id;
                    cmd.Parameters.Add("@Confirm_remark", SqlDbType.VarChar, 200).Value = ip.Confirm_remark;
                    cmd.Parameters.Add("@payment_link", SqlDbType.VarChar, 200).Value = ip.payment_link;
                    cmd.Parameters.Add("@meeting_link", SqlDbType.VarChar, 200).Value = ip.meeting_link;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = ip.login_id;
                    cmd.Parameters.Add("@AppType", SqlDbType.VarChar, 10).Value = ip.AppType;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = ip.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        string processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        if (processInfo.Contains("Success"))
                        {
                            tran.Commit();
                            dsResult.ResultSet = null;
                            dsResult.Msg = processInfo;
                            if (ip.Logic == "TakeConfirmation")
                            {
                                ipPackageQueries onl = new ipPackageQueries();
                                onl.BookingId = ip.BookingId;
                                onl.unit_id = ip.unit_id;
                                onl.prm_1 = "NotifyToManager";
                                onl.login_id = ip.login_id;
                                onl.Logic = "BookingDetail";
                                string t = BookingNotification(onl);
                                if (!t.Contains("Success"))
                                    dsResult.Msg = t;
                                dsResult.Msg = processInfo;
                            }
                        }
                        else
                        {
                            tran.Rollback();
                            dsResult.ResultSet = null;
                            dsResult.Msg = processInfo;
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        tran.Rollback();
                        dsResult.ResultSet = null;
                        dsResult.Msg = sqlEx.Message;
                    }
                    finally { con.Close(); }
                }
            }
            return dsResult;
        }
        public string BookingNotification(ipPackageQueries ipapp)
        {
            string result = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_MobileAppDb))
            {
                using (SqlCommand cmd = new SqlCommand("pOnline_DiagnosticPackageQueries", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 16).Value = ipapp.unit_id;
                    cmd.Parameters.Add("@TokenNo", SqlDbType.VarChar, 10).Value = ipapp.TokenNo;
                    cmd.Parameters.Add("@BookingId", SqlDbType.VarChar, 50).Value = ipapp.BookingId;
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = ipapp.fromdate;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = ipapp.todate;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = ipapp.prm_1;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 50).Value = ipapp.login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 200).Value = ipapp.Logic;
                    try
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        if (ipapp.prm_1 == "NotifyToManager")
                        {
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                try
                                {
                                    string mobile_no = ds.Tables[0].Rows[0]["managerMobile"].ToString();
                                    if (mobile_no.Length > 9)
                                    {
                                        string BookingId = ds.Tables[0].Rows[0]["BookingId"].ToString();
                                        string unitid = ds.Tables[0].Rows[0]["unit_id"].ToString();
                                        string sms = "Dear Manager of Chandan Group, Please Check New Package Booking at below link http://exprohelp.com/ChandanMIS/online/online/HealthCheckup?UnitId=" + unitid + "";
                                        Utilities.SmsClass smsService = new Utilities.SmsClass();
                                        string response = smsService.SendSms(mobile_no, sms);
                                        response = "InformToManager : " + response;
                                        string qry_sms = "insert into online_SmsLog(TrnType, TranNo, mobile_no,sms,login_id, response_message)";
                                        qry_sms += "values('NotifyToManager','" + BookingId + "','" + mobile_no + "', '" + sms + "', '-','" + response + "')";
                                        DBManager.QueryExecute(qry_sms, GlobalConfig.ConStr_MobileAppDb);
                                        result = "Successfully Sent";
                                    }
                                }
                                catch (Exception ex) { result = ex.Message; }
                            }
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                    }
                    finally { con.Close(); }
                    return result;
                }
            }
        }
        public dataSet CallWebApiPayUMoney(string methodRoute, ipTransaction obj)
        {
            dataSet ds = new dataSet();
            try
            {

                var client = new RestClient("https://www.payumoney.com/payment/op/getPaymentResponse?merchantKey=DCqJFDrL&merchantTransactionIds=" + obj.merchantTransactionIds + "");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("authorization", "FGO4m1XkpAAz6+uXT6TaJssy9q2L5FcOcgEGfYkzYYw=");
                IRestResponse response = client.Execute(request);
                ipPayUResponse payUResponse = JsonConvert.DeserializeObject<ipPayUResponse>(response.Content, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Formatting = Formatting.None,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    FloatParseHandling = FloatParseHandling.Decimal
                });
                DataTable dt = new DataTable();
                dt.Columns.Add("AppointmentId", typeof(string));
                dt.Columns.Add("paymentId", typeof(string));
                dt.Columns.Add("status", typeof(string));
                dt.Columns.Add("addedon", typeof(string));
                dt.Columns.Add("amount", typeof(string));
                dt.Columns.Add("mode", typeof(string));
                dt.Columns.Add("cmdType", typeof(string));
                if (payUResponse.result != null)
                {
                    var t = payUResponse.result.AsEnumerable().Where(x => x.postBackParam.status == "success");
                    int ct = 1;
                    string SyspayStatus = "-";
                    DataSet ds1 = DBManager.GetQueryResult("select payStatus from online_DiagnosticBooking where BookingId='" + obj.merchantTransactionIds + "' ", GlobalConfig.ConStr_MobileAppDb);
                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        SyspayStatus = ds1.Tables[0].Rows[0]["payStatus"].ToString();
                    }
                    foreach (PayUResult pr in payUResponse.result)
                    {
                        PayUBody pbd = pr.postBackParam;
                        DataRow dr = dt.NewRow();
                        dr["AppointmentId"] = obj.merchantTransactionIds;
                        dr["paymentId"] = pbd.paymentId;
                        dr["status"] = pbd.status;
                        dr["addedon"] = pbd.addedon;
                        dr["mode"] = pbd.mode;
                        dr["amount"] = pbd.amount;
                        if (pbd.status == "success" && t.Count() == 1)
                        {
                            if (SyspayStatus == "success")
                                dr["cmdType"] = "Payment Marked";
                            else
                                dr["cmdType"] = "Update";
                        }
                        if (pbd.status == "success" && t.Count() > 1)
                        {
                            if (ct == 1)
                            {
                                if (SyspayStatus == "success")
                                    dr["cmdType"] = "Payment Marked";
                                else
                                    dr["cmdType"] = "Update";
                            }
                            else
                            {
                                dr["cmdType"] = "Refund";
                            }
                            ct++;
                        }
                        dt.Rows.Add(dr);
                    }
                    DataSet ds2 = new DataSet();
                    ds2.Tables.Add(dt);
                    ds.ResultSet = ds2;
                    ds.Msg = "Found";
                }
                else
                {
                    ds.ResultSet = null;
                    ds.Msg = "No Payment Fount";
                }

            }
            catch (Exception ex)
            {

                ds.ResultSet = null;
                ds.Msg = ex.Message;
            }
            return ds;
        }
        public dataSet CallWebApiPayUMoney2(string methodRoute, ipTransaction obj)
        {
            dataSet ds = new dataSet();
            try
            {
                var jsonResponse = string.Empty;
                string Url = "https://www.payumoney.com/payment/op/getPaymentResponse?merchantKey=DCqJFDrL&merchantTransactionIds=" + obj.merchantTransactionIds + "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.Timeout = 12000;
                request.ContentType = "application/json";
                request.Headers.Add("authorization", "FGO4m1XkpAAz6+uXT6TaJssy9q2L5FcOcgEGfYkzYYw=");
                using (Stream s = request.GetResponse().GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(s))
                    {
                        jsonResponse = sr.ReadToEnd();
                    }
                }
                ipPayUResponse payUResponse = JsonConvert.DeserializeObject<ipPayUResponse>(jsonResponse, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Formatting = Formatting.None,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    FloatParseHandling = FloatParseHandling.Decimal
                });
                DataTable dt = new DataTable();
                dt.Columns.Add("AppointmentId", typeof(string));
                dt.Columns.Add("paymentId", typeof(string));
                dt.Columns.Add("status", typeof(string));
                dt.Columns.Add("addedon", typeof(string));
                dt.Columns.Add("amount", typeof(string));
                dt.Columns.Add("mode", typeof(string));
                dt.Columns.Add("cmdType", typeof(string));
                if (payUResponse.result != null)
                {
                    var t = payUResponse.result.AsEnumerable().Where(x => x.postBackParam.status == "success");
                    int ct = 1;
                    string SyspayStatus = "-";
                    DataSet ds1 = DBManager.GetQueryResult("select payStatus from online_DiagnosticBooking where BookingId='" + obj.merchantTransactionIds + "' ", GlobalConfig.ConStr_MobileAppDb);
                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        SyspayStatus = ds1.Tables[0].Rows[0]["payStatus"].ToString();
                    }
                    foreach (PayUResult pr in payUResponse.result)
                    {
                        PayUBody pbd = pr.postBackParam;
                        DataRow dr = dt.NewRow();
                        dr["AppointmentId"] = obj.merchantTransactionIds;
                        dr["paymentId"] = pbd.paymentId;
                        dr["status"] = pbd.status;
                        dr["addedon"] = pbd.addedon;
                        dr["mode"] = pbd.mode;
                        dr["amount"] = pbd.amount;
                        if (pbd.status == "success" && t.Count() == 1)
                        {
                            if (SyspayStatus == "success")
                                dr["cmdType"] = "Payment Marked";
                            else
                                dr["cmdType"] = "Update";
                        }
                        if (pbd.status == "success" && t.Count() > 1)
                        {
                            if (ct == 1)
                            {
                                if (SyspayStatus == "success")
                                    dr["cmdType"] = "Payment Marked";
                                else
                                    dr["cmdType"] = "Update";
                            }
                            else
                            {
                                dr["cmdType"] = "Refund";
                            }
                            ct++;
                        }
                        dt.Rows.Add(dr);
                    }
                    DataSet ds2 = new DataSet();
                    ds2.Tables.Add(dt);
                    ds.ResultSet = ds2;
                    ds.Msg = "Found";
                }
                else
                {
                    ds.ResultSet = null;
                    ds.Msg = "No Payment Fount";
                }


            }
            catch (Exception ex)
            {

                ds.ResultSet = null;
                ds.Msg = ex.Message;
            }
            return ds;
        }
        public string UpdatePaymentStatus(ipUpdatePayStatus ip)
        {
            string result = string.Empty;
            if (ip.command == "Update")
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_MobileAppDb))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    using (SqlCommand cmd = new SqlCommand("pOnline_PaymentUpdate", con))
                    {
                        string[] t = ip.StrValues.Split('|');
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@BookingId", SqlDbType.VarChar, 16).Value = ip.AppointmentId;
                        cmd.Parameters.Add("@paymentId", SqlDbType.VarChar, 50).Value = t[0];
                        cmd.Parameters.Add("@payStatus", SqlDbType.VarChar, 50).Value = t[1];
                        cmd.Parameters.Add("@payAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(t[2]);
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = ip.loginId;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = ip.Logic;
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                        cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                        try
                        {
                            cmd.ExecuteNonQuery();
                            string processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                            if (processInfo.Contains("Success"))
                            {
                                tran.Commit();
                                result = processInfo;
                            }
                            else
                            {
                                result = "Roll back";
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            tran.Rollback();
                            result = sqlEx.Message;
                        }
                        finally { con.Close(); }

                    }
                }
            }
            //if (ip.command == "Refund")
            //{
            //	try
            //	{
            //		string[] t = ip.StrValues.Split('|');
            //		var client = new RestClient("https://www.payumoney.com/treasury/merchant/refundPayment?merchantKey=E3Ghpv8l&paymentId=" + t[0] + "&refundAmount=" + t[2] + "");
            //		client.Timeout = -1;
            //		var request = new RestRequest(Method.POST);
            //		request.AddHeader("authorization", "uFROiqo2VnqSsIUCNji3+xRpiqM9ciu0gd+kuIuA9lc=");
            //		IRestResponse response = client.Execute(request);
            //		ipPayRefundResponse payURefundResponse = JsonConvert.DeserializeObject<ipPayRefundResponse>(response.Content, new JsonSerializerSettings
            //		{
            //			NullValueHandling = NullValueHandling.Ignore,
            //			MissingMemberHandling = MissingMemberHandling.Ignore,
            //			Formatting = Formatting.None,
            //			DateFormatHandling = DateFormatHandling.IsoDateFormat,
            //			FloatParseHandling = FloatParseHandling.Decimal
            //		});
            //		result = payURefundResponse.message;
            //	}
            //	catch (Exception ex) { result = ex.Message; }
            //}
            return result;
        }
        public string InsertMobileAppCoupons(CouponLogBO objBO)
        {
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_MobileAppDb))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pInsertMobileAppCoupons", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 12).Value = objBO.Mobile;
                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar, 100).Value = objBO.PatientName;
                    cmd.Parameters.Add("@CouponCode", SqlDbType.VarChar, 50).Value = objBO.CouponCode;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 100).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                    }
                    catch (Exception sqlEx)
                    {
                        processInfo = "Error Found   : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
    }
}