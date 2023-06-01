using System.Web.Mvc;

namespace ChandanCRM.Areas.Diagnostic
{
    public class DiagnosticAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Diagnostic";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Diagnostic_default",
                "Diagnostic/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}