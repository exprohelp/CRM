using Crm_Api.Models;
using Crm_Api.Repository.ApplicationResource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Crm_Api.Repository.TeleCalling
{
    public class TeleCalling
    {
        public string HIM_InsertCallLog(TeleCaller objBO)
        {
            string processInfo = string.Empty;
            SqlConnection con = new SqlConnection(GlobalConfig.ConStr_CustomerData);
            SqlCommand cmd = new SqlCommand("pHIM_InsertCallLog", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@CardNo", SqlDbType.VarChar,20).Value = objBO.CardNo;
            cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 10).Value = objBO.MobileNo;                                              
            cmd.Parameters.Add("@CallBy", SqlDbType.VarChar, 50).Value = objBO.CallBy;           
            cmd.Parameters.Add("@CallRemark", SqlDbType.VarChar, 50).Value = objBO.CallRemark;
            cmd.Parameters.Add("@ScheduleDate", SqlDbType.DateTime).Value = objBO.ScheduleDate;
            cmd.Parameters.Add("@Prm1", SqlDbType.VarChar,100).Value = objBO.Prm1;
            cmd.Parameters.Add("@Prm2", SqlDbType.VarChar,100).Value = objBO.Prm2;
            cmd.Parameters.Add("@login_id", SqlDbType.VarChar,10).Value = objBO.login_id;
            cmd.Parameters.Add("@Logic", SqlDbType.VarChar,50).Value = objBO.Logic;
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
    }
}