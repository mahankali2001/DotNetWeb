using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb.Areas
{

    public class WebAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Web";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
            "Web_default",
            "Web/{controller}/{action}/{id}",
            new { action = "Index", id = UrlParameter.Optional }, new[] { "AppWeb.Controllers" } 
            );
          
        }
    }
}
