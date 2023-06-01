using Crm_Api.Models;
using Crm_Api.Repository.ApplicationResource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Crm_Api.Repository.TPA
{
    public class JustDial
    {
        public string InsertJustDialLeads(ipTPA objBO)
        {
            dataSet dsObj = new dataSet();
            string processInfo = string.Empty;
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnCSD))
            {
                using (SqlCommand cmd = new SqlCommand("pInsertJustDialLeads",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@leadid", SqlDbType.VarChar, 255).Value = objBO.leadid;
                    cmd.Parameters.Add("@leadtype", SqlDbType.VarChar, 255).Value = objBO.leadtype;
                    cmd.Parameters.Add("@prefix", SqlDbType.VarChar, 10).Value = objBO.prefix;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 255).Value = objBO.name;
                    cmd.Parameters.Add("@mobile", SqlDbType.VarChar, 50).Value = objBO.mobile;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar, 50).Value = objBO.phone;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = objBO.email;
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = objBO.date;
                    cmd.Parameters.Add("@category", SqlDbType.VarChar, 255).Value = objBO.category;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar, 255).Value = objBO.city;
                    cmd.Parameters.Add("@area", SqlDbType.VarChar, 255).Value = objBO.area;
                    cmd.Parameters.Add("@brancharea", SqlDbType.VarChar, 255).Value = objBO.brancharea;
                    cmd.Parameters.Add("@dncmobile", SqlDbType.Int).Value = objBO.dncmobile;
                    cmd.Parameters.Add("@dncphone", SqlDbType.Int).Value = objBO.dncphone;
                    cmd.Parameters.Add("@company", SqlDbType.VarChar, 255).Value = objBO.company;
                    cmd.Parameters.Add("@pincode", SqlDbType.VarChar, 50).Value = objBO.pincode;
                    cmd.Parameters.Add("@time", SqlDbType.Time).Value = objBO.time;
                    cmd.Parameters.Add("@branchpin", SqlDbType.VarChar, 50).Value = objBO.branchpin;
                    cmd.Parameters.Add("@parentid", SqlDbType.VarChar, 255).Value = objBO.parentid;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.Login_id;
                    cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 20).Value = objBO.Logic;
                    cmd.Parameters.Add("@result", SqlDbType.VarChar, 50).Value = "";
                    cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                    try
                    {
                        con.Close();
                        cmd.ExecuteNonQuery();
                        processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                        con.Close();
                    }
                    catch (SqlException sqlEx)
                    {
                        processInfo = "Error Found  : " + sqlEx.Message;
                    }
                    finally { con.Close(); }
                    return processInfo;
                }
            }
        }
    }
}