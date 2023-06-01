using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChandanCRM.Areas.Pharmacy.Controllers
{
    public class AnalysisController : Controller
    {
        // GET: Pharmacy/Analysis       
        public ActionResult SalesByStaff()
        {
            return View();
        }
        public ActionResult SalesByUnit()
        {
            return View();
        }
        public ActionResult NewOrder()
        {
            return View();
        }
        public ActionResult RepeatOrder()
        {
            return View();
        }
        public ActionResult NotRepeatedOrder()
        {
            return View();
        }
        public ActionResult CommissionReport()
        {
            return View();
        }
    }
}