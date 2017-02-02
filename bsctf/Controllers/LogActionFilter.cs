using NLog;
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
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static Dictionary<string, long> counters = new Dictionary<string, long>();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var name = HttpContext.Current.User.Identity.Name;

            if (string.IsNullOrEmpty(name))
                return;

            if (counters.ContainsKey(name))
                counters[name]++;
            else
                counters.Add(name, 1);

            if (counters[name]%10 == 0)
                logger.Info("{0} - {1}", name, counters[name]);
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