using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Crm_Api.Repository.Utilities
{
    public class ExcelGenerator
    {
        public HttpResponseMessage GetExcelFile(DataSet ds)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var ms = new MemoryStream();
                using (ClosedXML.Excel.XLWorkbook wb = new ClosedXML.Excel.XLWorkbook())
                {
                    if (ds.Tables.Count > 0)
                    {
                        int ct = 1;
                        foreach (DataTable dt in ds.Tables)
                        {
                            wb.Worksheets.Add(ds.Tables[0], "Sheet" + ct.ToString());
                            ct++;
                        }
                    }
                    else { wb.Worksheets.Add(ds.Tables[0], "Sheet1"); }

                    wb.SaveAs(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StreamContent(ms);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = "test.xlsx";
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    response.Content.Headers.ContentLength = ms.Length;
                    ms.Seek(0, SeekOrigin.Begin);
                }
            }
            catch (Exception ex)
            {
            }
            return response;
        }
        public HttpResponseMessage GetPDFFile()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                byte[] data = System.IO.File.ReadAllBytes("m1.pdf");
                var ms = new MemoryStream(data);
                ms.Seek(0, SeekOrigin.Begin);
                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(ms);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = "test.pdf";
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentLength = ms.Length;
                ms.Seek(0, SeekOrigin.Begin);

            }
            catch (Exception ex)
            {
            }
            return response;
        }
    }
}