using System.Web.Mvc;
using SearchEngine;

namespace bsctf.Controllers
{
    [LogActionFilter, Authorize]
    public class SearchController : Controller
    {
        private readonly ISearch searchService;

        public SearchController(ISearch searchService)
        {
            this.searchService = searchService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string query)
        {
            var model = searchService.Search(query);
            return View(model);
        }
    }
}