using System.Linq;
using System.Web.Mvc;

namespace bsctf.Controllers
{
    [LogActionFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Log()
        {
            if (User.Identity.Name == "iruslansafin@gmail.com")
                return Content(string.Join("<br/>", LogActionFilter.counters.Select(x => $"{x.Key} - {x.Value}")));
            return Content("");
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}