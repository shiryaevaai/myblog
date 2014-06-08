namespace EpamTask.MyBlog.WebInterface.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;
    using WebInterface.Models;

    using EpamTask.MyBlog.Logic;
    using EpamTask.MyBlog.Entities;

    public class SearchController : Controller
    {
        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchResults(List<BlogPostModel> posts)
        {
            return View(posts);
        }

        [ChildActionOnly]
        public ActionResult SearchPostsByTag()
        {
            SearchModel model = new SearchModel("Введите текст для поиска");
            return PartialView(model);
        }       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchPostsByTagResults(string tag)
        {
            var model = SearchModel.SearchPostsByTag(tag).ToList();

            if (model.Count != 0)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ChildActionOnly]
        public ActionResult SearchPostsByText()
        {
            SearchModel model = new SearchModel("Введите текст для поиска");
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchPostsByTextResults(SearchModel text)
        {
            var model = SearchModel.SearchPostsByText(text.SearchText).ToList();

            if (model.Count != 0)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
	}
}