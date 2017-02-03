using NLog;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace bsctf.Controllers
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static readonly Dictionary<string, long> Counters = new Dictionary<string, long>();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var name = HttpContext.Current.User.Identity.Name;

            if (string.IsNullOrEmpty(name))
                return;

            if (Counters.ContainsKey(name))
                Counters[name]++;
            else
                Counters.Add(name, 1);

            if (Counters[name]%100 == 0)
                logger.Info("{0} - {1}", name, Counters[name]);
        }
    }
}