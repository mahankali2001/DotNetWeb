using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SampleApp1.services
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    public class Service : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string key = context.Request.QueryString["act"];
            string args = string.Empty, resp = string.Empty;
            bool ignoreExcep = false;
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    
                    if (context.Request.QueryString["args"] != null)
                        args = context.Request.QueryString["args"];
                    switch (key.ToUpper())
                    {
                        case "CM":
                            //ContextMenuController oCMController = new ContextMenuController();
                            //resp = oCMController.LoadCM(args);
                            resp = "<span class='newCM'>menu</span>";
                            break;
                        case "DOWNLOADATTACHMENT":
                            try
                            {
                                var file = new FileInfo(context.Request.QueryString["path"]);
                                if (file.Exists)
                                {
                                    context.Response.ContentType = context.Request.QueryString["type"];
                                    //  context.Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);
                                    context.Response.AddHeader("Content-Disposition",
                                                               String.Format("attachment;filename=\"{0}\"",
                                                                             file.Name.ToString()));
                                    context.Response.WriteFile(file.FullName, false);
                                    context.Response.Flush();
                                    context.Response.End();
                                    context.Response.Close();
                                }
                                else
                                {
                                    resp = "File does not exist";
                                }
                            }
                            catch
                            {
                                ignoreExcep = false;
                                resp = "";
                            }
                            break;
                        default:
                            break;
                    }
                }
                //context.Response.ContentType = "text/plain";
                //context.Response.Write("Hello World");
            }
            catch(Exception exc)
            {
                if (ignoreExcep)
                {
                   // _logger.Log(exc);
                    Exception[] e = GetInnerExceptions(exc);
                    foreach (var exception in e)
                    {
                        resp = resp + exception.Message + " " + exception.StackTrace;
                    }
                }
            }
            finally
            {
                context.Response.Clear();
                context.Response.Write(resp);
            }
        }

        public Exception[] GetInnerExceptions(Exception ex)
        {
            List<Exception> exceptions = new List<Exception>();
            exceptions.Add(ex);
            Exception currentEx = ex;
            while (currentEx.InnerException != null)
            {
                currentEx = currentEx.InnerException;
                exceptions.Add(currentEx);
            }
            // Reverse the order to the innermost is first
            exceptions.Reverse();
            return exceptions.ToArray();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}