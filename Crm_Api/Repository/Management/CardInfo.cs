using Crm_Api.Models;
using Crm_Api.Repository.ApplicationResource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Crm_Api.Repository.Managment
{
    public class CardInfo
    {
        public dataSet CardInfoQueries(ipCardInfo objBO)
        {
            dataSet dsObj = new dataSet();
            using (SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy))
            {
                using (SqlCommand cmd = new SqlCommand("pCardInfo_Queries",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = objBO.UnitId;
                    cmd.Parameters.Add("@customerId", SqlDbType.VarChar, 20).Value = objBO.CustomerId;
                    cmd.Parameters.Add("@from", SqlDbType.VarChar, 10).Value = objBO.From;
                    cmd.Parameters.Add("@to", SqlDbType.VarChar, 10).Value = objBO.To;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar,50).Value = objBO.Prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = objBO.Prm_2;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = objBO.Logic;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = objBO.Login_Id;
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
    }
}