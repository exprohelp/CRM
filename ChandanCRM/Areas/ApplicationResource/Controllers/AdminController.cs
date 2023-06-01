using ChandanCRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChandanCRM.Areas.ApplicationResource.Controllers
{
    public class AdminController : Controller
    {
        // GET: ApplicationResource/Admin
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            HospitalInfo.HospitalName = "CHANDAN HOSPITAL";
            HospitalInfo.HospitalPhone = "0522-666666";
            HospitalInfo.HospitalEmail = "care@chandanhospital.in";
            HospitalInfo.HospitalAddress = "Faizabad Road,Near Chinhat Flyover, Vijayant Khand, Lucknow,226010";

            return View();
        }
        public ActionResult Employee()
        {
            return View();
        }
        public ActionResult EmployeeComponent()
        {
            return View();
        }
        public ActionResult Role()
        {
            return View();
        }
        public ActionResult SubMenu()
        {
            return View();
        }
        public ActionResult MenuAllotment()
        {
            return View();
        }
    }
}