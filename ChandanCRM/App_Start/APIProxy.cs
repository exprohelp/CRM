using Crm_Api.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;

namespace ChandanCRM.App_Start
{
    public class APIProxy
    {
        public static string Baseurl = ConfigurationManager.AppSettings["APIHostPath"].ToString();
        public static dataSet CallWebApiMethod(string methodRoute, Object obj)
        {
            dataSet ds = new dataSet();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(Baseurl);
                    HttpResponseMessage response = client.PostAsJsonAsync("api/" + methodRoute + "", obj).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string response_data = response.Content.ReadAsStringAsync().Result;
                        ds = JsonConvert.DeserializeObject<dataSet>(response_data, new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore,
                            Formatting = Formatting.None,
                            DateFormatHandling = DateFormatHandling.IsoDateFormat,
                            FloatParseHandling = FloatParseHandling.Decimal
                        });
                    }
                }
                catch (Exception ex) { ds.ResultSet = null; ds.Msg = ex.Message;}
            }
            return ds;
        }
    }
}