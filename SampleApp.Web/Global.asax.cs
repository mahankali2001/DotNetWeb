using System.Web.Mvc;
using System.Web.Routing;


namespace SampleApp
{
    public class Global : GlobalBase
    {
        protected override void Application_Start(object sender, System.EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            base.Application_Start(sender, e);
            RegisterRoutes(RouteTable.Routes);
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{*allaxd}", new { allaxd = @".*\.axd(/.*)?" });
            routes.IgnoreRoute("{*allico}", new { allico = @".*\.ico(/.*)?" });
            routes.IgnoreRoute("{*allashx}", new { allashx = @".*\.ashx(/.*)?" });
            routes.IgnoreRoute("{*allasmx}", new { allasmx = @".*\.asmx(/.*)?" });
            routes.IgnoreRoute("cas/{*.pathInfo}");
            routes.IgnoreRoute("service/{*.pathInfo}");
            routes.IgnoreRoute("images/{*.pathInfo}");
            routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
            routes.IgnoreRoute("");
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                //new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                  new { controller = "ProductSet", action = "ProductSetPopUp", id = UrlParameter.Optional, commonDealId = UrlParameter.Optional, isMasterDeal =UrlParameter.Optional} // Parameter defaults
            );
        }

    }
}
