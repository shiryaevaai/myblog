namespace EpamTask.MyBlog.WebInterface.Controllers
{    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;

    using EpamTask.MyBlog.Entities;
    using EpamTask.MyBlog.Logic;
    using log4net;
    using WebInterface.Models;

    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(SearchController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        public ActionResult SearchResults(List<BlogPostModel> posts)
        {
            try
            {
                return View(posts);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(SearchController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [ChildActionOnly]
        public ActionResult SearchPostsByTag()
        {
            try
            {
                SearchModel model = new SearchModel("Введите текст для поиска");
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(SearchController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchPostsByTagResults(SearchModel tag)
        {
            try
            {
                var model = SearchModel.SearchPostsByTag(tag.SearchText).ToList();

                if (model.Count != 0)
                {
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(SearchController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [ChildActionOnly]
        public ActionResult SearchPostsByText()
        {
            try
            {
                SearchModel model = new SearchModel("Введите текст для поиска");
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(SearchController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchPostsByTextResults(SearchModel text)
        {
            try
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
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(SearchController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }
    }
}