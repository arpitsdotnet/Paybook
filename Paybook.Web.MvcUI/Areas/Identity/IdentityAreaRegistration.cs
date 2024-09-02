﻿using System.Web.Mvc;

namespace Paybook.Web.MvcUI.Areas.Identity
{
    public class IdentityAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Identity";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Identity_default",
                "Identity/{controller}/{action}/{id}",
                new { controller = "User", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}