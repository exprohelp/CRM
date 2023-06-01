using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChandanCRM.Areas.Pharmacy.Controllers
{
    public class PharmacyController : Controller
    {
        // GET: Pharmacy/Pharmacy
        public ActionResult ProductHelp()
        {
            return View();
        }
        public ActionResult OnlineOrderRequest()
        {
            return View();
        }
        public ActionResult justdial()
        {
            return View();
        }
        public ActionResult OrderReport()
        {
            return View();
        }

        public ActionResult TeleCaller()
        {
            return View();
        }
        public ActionResult ProductInfo()
        {
            return View();
        }
        public ActionResult BySaltName()
        {
            return View();
        }
        
    }
}