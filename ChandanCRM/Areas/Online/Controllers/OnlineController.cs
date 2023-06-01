﻿//using ChandanCRMWebApi.Models;
//using ChandanCRMWebApi.Areas.Online.Models;
using ChandanCRM.Areas.Online.Models;
using Crm_Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ChandanCRM.Areas.Online.Controllers
{
    public class OnlineController : Controller
    {
        // GET: Online/Online
        public ActionResult BookingConfirmation()
        {
            return View();
        }
        public ActionResult PackageConfirmation()
        {
            return View();
        }
		public ActionResult AvailCoupon()
		{
			return View();
		}
		public ActionResult DoctorCovidControl()
        {
            return View();
        }
        public ActionResult CovidPackageItems()
        {
            return View();
        }
        public ActionResult TestChart()
        {
            return View();
        }
        public ActionResult doPaymentPackage()
        {
            string appid = Request.QueryString["ptId"].ToString();
            ipPackageQueries obj = new ipPackageQueries();
            obj.unit_id = "CH01";
            obj.PatientId = appid;
            obj.fromdate = "1900-01-01";
            obj.todate = "1900-01-01";
            obj.prm_1 = "-";
            obj.login_id = "-";
            obj.Logic = "BookingDetail";
            dataSet dsResult = ChandanCRM.App_Start.APIProxy.CallWebApiMethod("Online/PackageQueries", obj);
            ipPayU objpay = new ipPayU();
            if (dsResult.ResultSet.Tables.Count > 0 && dsResult.ResultSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsResult.ResultSet.Tables[0].Rows[0];
                objpay.Key = "E3Ghpv8l";
                objpay.Salt = "y6pP8BDlCf";
                objpay.UDF = "UPI";
                objpay.TnxId = dr["PatientId"].ToString();
                objpay.Amount = dr["doctor_fee"].ToString();
                objpay.firstname = dr["patient_name"].ToString();
                objpay.Mobile = dr["mobile_no"].ToString();
                objpay.Email = dr["email_id"].ToString();
                objpay.DoctorName = dr["doctor_name"].ToString();
                objpay.Remark = "Covid Care Package ";
                objpay.surl = "";
                objpay.furl = "";
                byte[] hash;
                string d = objpay.Key + "|" + objpay.TnxId + "|" + objpay.Amount + "|" + objpay.Remark + "|" + objpay.firstname + "|" + objpay.Email + "|||||" + objpay.UDF + "||||||" + objpay.Salt;
                var datab = Encoding.UTF8.GetBytes(d);
                using (SHA512 shaM = new SHA512Managed())
                {
                    hash = shaM.ComputeHash(datab);
                }
                objpay.Hash = GetStringFromHash(hash);
                objpay.Message = dr["payStatus"].ToString();
            }
            return View(objpay);
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
    }
}