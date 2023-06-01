using Crm_Api.Repository.ApplicationResource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static PharmacyAPI.Models.Salts;

namespace PharmacyAPI.Repository
{
    public class Salts
    {
        public DataSet SaltQueries(out string processInfo, pm_salts p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy);
            SqlCommand cmd = new SqlCommand("pSaltQueries", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@Item_id", SqlDbType.VarChar, 10).Value = p.item_id;
            cmd.Parameters.Add("@salt_Code", SqlDbType.VarChar, 10).Value = p.salt_Code;
            cmd.Parameters.Add("@logic", SqlDbType.VarChar, 40).Value = p.logic;
            cmd.Parameters.Add("@prm_1", SqlDbType.VarChar, 50).Value = p.prm_1;
            cmd.Parameters.Add("@prm_2", SqlDbType.Int, 50).Value = p.prm_2;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                processInfo = "No Error";
            }
            catch (SqlException sqlEx)
            {
                processInfo = sqlEx.ToString();
            }
            finally { con.Close(); }
            return ds;
        }
        public void InsertSaltInfo(out string processInfo, pm_salts p)
        {
            SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy);
            SqlCommand cmd = new SqlCommand("pInsSaltInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@salt_code", SqlDbType.VarChar, 5).Value = p.salt_code;
            cmd.Parameters.Add("@Salt_Name", SqlDbType.VarChar, 50).Value = p.Salt_Name;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 30).Value = "-";
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
        public void InsertStrengthInfo(out string processInfo, pm_salts p)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy);
            SqlCommand cmd = new SqlCommand("pInsStrengthInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@str_code", SqlDbType.VarChar, 5).Value = p.str_code;
            cmd.Parameters.Add("@str_type", SqlDbType.VarChar, 50).Value = p.str_type;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 30).Value = "-";
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
        public void InsertSubstitute(out string processInfo, pm_salts p)
        {
            SqlConnection con = new SqlConnection(GlobalConfig.strConnPharmacy);
            SqlCommand cmd = new SqlCommand("pInsSubstitute", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 2500;
            cmd.Parameters.Add("@item_id", SqlDbType.VarChar, 7).Value = p.item_id;
            cmd.Parameters.Add("@salt_code", SqlDbType.VarChar, 5).Value = p.salt_code;
            cmd.Parameters.Add("@str_code", SqlDbType.VarChar, 5).Value = p.str_code;
            cmd.Parameters.Add("@Strength", SqlDbType.Decimal).Value = p.Strength;
            cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 30).Value = p.login_id;
            cmd.Parameters.Add("@result", SqlDbType.VarChar, 30).Value = "";

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
    }
}