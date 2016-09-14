using System.IO;
using System.Web.Mvc;
using System;
using System.Web.Compilation;
using System.Web;

namespace AppWeb.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        protected string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        protected string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
namespace AppWeb.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static void Banner(this HtmlHelper html, string title)
        {
            //string pagePath = "~\\BannerContainer.aspx";
            //Type type = BuildManager.GetCompiledType(pagePath);
            //if (type == null)
            //    throw new ApplicationException("Page " + pagePath + " not found");
            //BannerContainer pageView = (BannerContainer)Activator.CreateInstance(type);

            //pageView.Title = title;
            //HttpContext context = App.Web.Repository.GridConfigRepository.getContext();
            //try
            //{
            //    ((IHttpHandler)pageView).ProcessRequest(context);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Error while rendering header." + ex.Message);
            //}
        }

        public static string GetResourceString(this HtmlHelper html, string text)
        {
            //return AppResourceManager.GetResource(text, GridConfigRepository.CurrentCulture);
            return string.Empty;
        }
    }
    public static class HtmlResource
    {
        public static string GetString(string text)
        {
            //return AppResourceManager.GetResource(text, GridConfigRepository.CurrentCulture);
            return string.Empty;
        }
    }
}