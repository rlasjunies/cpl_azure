using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Http.Filters;
using System.Web.Mvc;

namespace cpl_azure.App_Start
{
    public class SSLRedirector : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsSecureConnection)
            {
                var url = filterContext.HttpContext.Request.Url.ToString().Replace("http:", "https:");
                filterContext.Result = new RedirectResult(url);
            }
        }
    }

}