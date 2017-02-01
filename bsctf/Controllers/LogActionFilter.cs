using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bsctf.Controllers
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public static Dictionary<string, long> log = new Dictionary<string, long>();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var name = HttpContext.Current.User.Identity.Name;
            if (log.ContainsKey(name))
                log[name]++;
            else
                log.Add(name, 1);

            if (log[name]%10 == 0)
            {
                
            }
        }


        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            Debug.WriteLine(message, "Action Filter Log");
        }

    }
}