﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Crm_Api.Repository.ApplicationResource
{
    public class GlobalConfig
    {
        public static string ConStr_eIMData = ConfigurationManager.ConnectionStrings["ConStr_eIMData"].ToString();
        public static string ConStr_CustomerData = ConfigurationManager.ConnectionStrings["ConStr_CustomerData"].ToString();
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        public static string ConStr_Accounts = ConfigurationManager.ConnectionStrings["ConStr_Accounts"].ToString();
        public static string ConStr_eManagement = ConfigurationManager.ConnectionStrings["ConStr_eManagement"].ToString();
        public static string ConStr_Assets = ConfigurationManager.ConnectionStrings["ConStr_Assets"].ToString();
        public static string ConStr_Hospital = ConfigurationManager.ConnectionStrings["ConStr_Hospital"].ToString();
        public static string ConStr_HISByItDose = ConfigurationManager.ConnectionStrings["ConStr_HISByItDose"].ToString();
        public static string ConStr_ItdoseDataByChandan = ConfigurationManager.ConnectionStrings["ConStr_ItdoseDataByChandan"].ToString();
        public static string ConStr_LISByItDose = ConfigurationManager.ConnectionStrings["ConStr_LISByItDose"].ToString();
        public static string ConStr_ChandanPharmacyLive = ConfigurationManager.ConnectionStrings["ConStr_ChandanPharmacyLive"].ToString();
        public static string ConStr_MobileAppDb = ConfigurationManager.ConnectionStrings["ConStr_MobileAppDb"].ToString();

        public static string strConnPharmacy = ConfigurationManager.ConnectionStrings["strConnPharmacy"].ConnectionString;
        public static string strConnHR = ConfigurationManager.ConnectionStrings["strConnHR"].ConnectionString;
        public static string strConnCSD = ConfigurationManager.ConnectionStrings["strConnCSD"].ConnectionString;
        public static string strConnMGM = ConfigurationManager.ConnectionStrings["strConnMGM"].ConnectionString;
        //public static string strMobileApp = ConfigurationManager.ConnectionStrings["strConnMobileApp"].ConnectionString;
    }
}