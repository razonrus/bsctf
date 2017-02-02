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
            var model = new SearchModel
            {
                Users = searchService.Search(query),
                Query = query
            };
            return View(model);
        }
    }

    public class SearchModel
    {
        public SearchEngine.User[] Users { get; set; }

        public string Query { get; set; }
    }
}