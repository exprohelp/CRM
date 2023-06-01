using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Crm_Api.App_Start
{
    public class FindIp
    {
        public static string GetIpAddress()
        {
            string myIP = "";
            string hostName = Dns.GetHostName();
            myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return myIP;
        }
    }
}