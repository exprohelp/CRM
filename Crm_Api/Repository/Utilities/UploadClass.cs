using Crm_Api.Models;
using Crm_Api.Repository.ApplicationResource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace Crm_Api.Repository.Utilities
{
    public class UploadClass
    {
        public static string UploadDocumentInfo(out string outFileName, out string vertual_path, string Appointmentid, byte[] imageByte, string ImageName, string imageType)
        {
            outFileName = "Error";
            string result = "Not Saved";
            vertual_path = "http://exprohelp.com/HospDoc/GeneralStore/Purchase/" + ImageName + "." + imageType;
            string directory = "I:\\Hospital\\GeneralStore\\Purchase\\";
            try
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                outFileName = directory + ImageName + "." + imageType;
                File.WriteAllBytes(outFileName, imageByte);
                result = "Success";
            }
            catch (Exception ex) { result = ex.Message; }
            return result;
        }
        public SubmitStatus UploadDocument(string TrnType, string TranId, string ImageType, byte[] ImageByte, string login_id)
        {
            string processInfo = string.Empty;
            SubmitStatus ss = new SubmitStatus();
            string virtual_path = string.Empty; string physical_path = string.Empty;
            string ImageName = TranId.Replace("/", "").Replace("-", "");
            processInfo = UploadDocumentInfo(out physical_path, out virtual_path, TranId, ImageByte, ImageName, ImageType);
            if (processInfo.Contains("Success"))
            {
                using (SqlConnection con = new SqlConnection(GlobalConfig.ConStr_Hospital))
                {
                    using (SqlCommand cmd = new SqlCommand("pInsert_UploadedDocumentInfo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 2500;
                        cmd.Parameters.Add("@TrnType", SqlDbType.VarChar, 50).Value = TrnType;
                        cmd.Parameters.Add("@TranId ", SqlDbType.VarChar, 20).Value = TranId;
                        cmd.Parameters.Add("@virtual_path", SqlDbType.VarChar, 200).Value = virtual_path;
                        cmd.Parameters.Add("@physical_path", SqlDbType.VarChar, 200).Value = physical_path;
                        cmd.Parameters.Add("@login_id", SqlDbType.VarChar, 15).Value = login_id;
                        cmd.Parameters.Add("@Logic", SqlDbType.VarChar, 50).Value = "Upload";
                        cmd.Parameters.Add("@result", SqlDbType.VarChar, 200).Value = "";
                        cmd.Parameters["@result"].Direction = ParameterDirection.InputOutput;
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            processInfo = (string)cmd.Parameters["@result"].Value.ToString();
                            if (processInfo.Contains("Success"))
                            {
                                ss.Status = 1;
                                ss.Message = processInfo;
                                ss.purchId = TranId;
                                ss.virtual_path = virtual_path;
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            ss.Status = 0;
                            ss.purchId = TranId;
                            ss.Message = sqlEx.Message;
                        }
                        finally { con.Close(); }
                    }
                    return ss;
                }
            }
            else
            {
                ss.Status = 0;
                ss.Message = processInfo;
            }
            return ss;
        }
    }
}