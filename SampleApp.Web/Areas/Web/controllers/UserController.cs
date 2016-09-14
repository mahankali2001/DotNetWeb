using System;
using System.Linq;
using SampleApp.Services.Client;
using SampleApp.Services.Client.Model;
using MVC = System.Web.Mvc;
using System.Web.UI;
using System.IO;
using System.Web;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Web.Mvc;
using System.Web.Compilation;

namespace AppWeb.Controllers
{
    public class UserController : AngularCommonController
    {

        public MVC.ActionResult Index()
        {
            // to check the seesion timout
            //if (HandleSessionTimeOut())
            //{
            //ViewData["IsMultiTenant"] = ApplicationConfiguration.IsMultiTenant;
            ViewBag.Title = "Manage Users";
            ViewData["isAngular"] = true;

            //ViewData["hdnNoResultsFoundText"] = AppResourceManager.GetResource("Common_NoResultsFound");
            return View();
            //}
            //else
            //{
            //    Response.Redirect(SampleApp.Common.WebHelper.GetApplicationPath() + "/default.aspx", true);
            //    return View();
            //}
        }

        public JsonResult GetUsers()
        {
            // to check the seesion timout
            //if (HandleSessionTimeOut())
            //{

            string response = Rest.Instance.GetUsers();
            return Json(response, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json("SessionTimeout", JsonRequestBehavior.AllowGet);
            //}
        }

        public JsonResult GetUser(int uid)
        {
            // to check the seesion timout
            //if (HandleSessionTimeOut())
            //{

            string response = Rest.Instance.GetUser(uid+"");
            return Json(response, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json("SessionTimeout", JsonRequestBehavior.AllowGet);
            //}
        }

        public JsonResult SaveUser(UserRequest ur)
        {
            // to check the seesion timout
            //if (HandleSessionTimeOut())
            //{

            string response = Rest.Instance.SaveUsers(ur);
            return Json(response, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json("SessionTimeout", JsonRequestBehavior.AllowGet);
            //}
        }

        //public static string GetSetupDivision(string divisionId)
        //{
        //    try
        //    {

        //        RepositoryHelper.SetDealHeader();
        //    }
        //    catch
        //    {
        //        throw new TimeoutException("SESSION");
        //    }

        //    return Rest.Instance.GetSetupDivision(divisionId);
        //}
    }
}
