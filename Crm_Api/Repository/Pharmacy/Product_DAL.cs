using Crm_Api.Repository.ApplicationResource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static Crm_Api.Models.Common;

namespace Crm_Api.Repository.Pharmacy
{
    public class Product_DAL
    {
        string _result = string.Empty;
        public DataSet Product_Queries(out string processInfo, cm1 pm)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy);
            try
            {
                using (SqlCommand cmd = new SqlCommand("pCRM_ProductQueries",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = pm.unit_id;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 100).Value = pm.Logic;
                    cmd.Parameters.Add("@searchString", SqlDbType.VarChar, 100).Value = pm.searchString;
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = pm.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = pm.prm_2;
                    cmd.Parameters.Add("@prm_3", SqlDbType.VarChar, 50).Value = pm.prm_3;
                    cmd.Parameters.Add("@prm_4", SqlDbType.VarChar, 50).Value = pm.prm_4;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = pm.login_id;
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
        public DataSet ProductsQueries(out string processInfo, cm1 pm)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy);
            try
            {
                using (SqlCommand cmd = new SqlCommand("pProduct_Queries",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@unit_id", SqlDbType.VarChar, 10).Value = pm.unit_id;
                    cmd.Parameters.Add("@logic", SqlDbType.VarChar, 50).Value = pm.Logic;              
                    cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = pm.prm_1;
                    cmd.Parameters.Add("@prm_2", SqlDbType.VarChar, 50).Value = pm.prm_2;
                    cmd.Parameters.Add("@prm_3", SqlDbType.VarChar, 50).Value = pm.prm_3;
                    cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 10).Value = pm.login_id;
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
    }
}