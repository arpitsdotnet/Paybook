using System.Web.Mvc;

namespace Paybook.Web.MvcUI.Areas.Chief
{
    public class ChiefAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chief";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Chief_default",
                "Chief/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}