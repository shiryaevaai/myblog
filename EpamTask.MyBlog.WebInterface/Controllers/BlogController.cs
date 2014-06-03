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

    public class BlogController : Controller
    {
        //
        // GET: /Blog/
        public ActionResult Index(Guid id)
        {
            return View(id);
        }

        [ChildActionOnly]
        public ActionResult GetUserPosts(Guid id)
        {
            var model = BlogPostModel.GetUserPosts(id).ToList();
            return PartialView(model);
        }

        public ActionResult CreatePost(Guid id)
        {
            BlogPostModel model = new BlogPostModel()
            {
                AuthorID = id,
            };

            return View(model);
        }

        //
        // POST: /Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(BlogPostModel model)
        {
            if (ModelState.IsValid)
            {
                if (BlogPostModel.CreatePost(model))
                {
                    try
                    {
                        return RedirectToAction("Index", "Blog", new { id = model.AuthorID });
                    }
                    catch
                    {
                        return View(model);
                    }
                }               
            }

            return View(model);
        }

        public ActionResult Feeds(Guid id)
        {
            return View();
        }

        public ActionResult Favourite(Guid id)
        {
            return View();
        }
	}
}