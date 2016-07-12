using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using FullTextSearch.Db;

namespace FullTextSearch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {
            using (var context = new WikiContext())
            {
                return View("Index", context.WikiReferences.ToList());
            }
        }

        [HttpPost]
        public ActionResult Post() {
            var query = Request.Form["SearchQuery"];
            if (string.IsNullOrWhiteSpace(query)) {
                using (var context = new WikiContext())
                {
                    return View("Index", context.WikiReferences.ToList());
                }
            }

            query = Regex.Replace(query, @"\s+", " ").Trim();
            query = query.Replace(" ", " OR ");

            var s = FtsInterceptor.Fts(query);

            using (var context = new WikiContext())
            {
                return View("Index", context.WikiReferences
                    .Where(r => r.Content.Contains(s)).ToList());
            }

        }
    }
}