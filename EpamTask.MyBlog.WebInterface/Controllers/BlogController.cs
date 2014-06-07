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

        [ChildActionOnly]
        public ActionResult GetAllPosts()
        {
            var model = BlogPostModel.GetAllPosts().ToList();
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

        public ActionResult PostAndComments(Guid id)
        {
            BlogPostModel model = BlogPostModel.GetPost(id);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult PostComments(Guid id)
        {
            var model = BlogPostModel.GetPostComments(id);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult AddComment(Guid postID, Guid authorID)
        {
            var model = new CommentModel()
            {
                PostID = postID,
                AuthorID = authorID,                
            };

            return PartialView(model);           
        }

        [HttpPost]
        public ActionResult AddComment(CommentModel model)
        {
            try 
            { 
                CommentModel.AddComment(model);
            }
            catch
            {
                return RedirectToAction("Index", "Blog", new { id = BlogPostModel.GetPost( model.PostID).AuthorID });
            }

            return RedirectToAction("Index", "Blog", new { id = BlogPostModel.GetPost(model.PostID).AuthorID });
        }

        public ActionResult DeleteComment(Guid id)
        {
            var model = CommentModel.GetComment(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(CommentModel model)
        {
            try
            {
                CommentModel.DeleteComment(model.CommentID);
            }
            catch
            {
                return RedirectToAction("PostAndComments", "Blog", new { id = BlogPostModel.GetPost(model.PostID).PostID });
            }

            return RedirectToAction("PostAndComments", "Blog", new { id = BlogPostModel.GetPost(model.PostID).PostID });
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