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
        //[Authorize(Roles = "Admin")]
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
        [ValidateAntiForgeryToken]
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

        public ActionResult EditComment(Guid commentID)
        {
            var model = CommentModel.GetComment(commentID);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(CommentModel model)
        {
            try
            {
                if (CommentModel.UpdateComment(model))
                {
                    return RedirectToAction("PostAndComments", "Blog", new { id = model.PostID });
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }         
        }

        public ActionResult DeleteComment(Guid commentID)
        {
            var model = CommentModel.GetComment(commentID);
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
                return RedirectToAction("PostAndComments", "Blog", new { id = model.PostID});
            }

            return RedirectToAction("PostAndComments", "Blog", new { id = model.PostID});
        }

        [ChildActionOnly]
        public ActionResult GetCommentsNumber(Guid postID)
        {
            int model = BlogPostModel.GetPostComments(postID).Count();

            return PartialView(model);
        }

        public ActionResult EditPost(Guid postID)
        {
            var model = new EditPostModel(postID);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(EditPostModel model)
        {
            try
            {
                if (EditPostModel.UpdatePost(model))
                {
                    return RedirectToAction("PostAndComments", "Blog", new { id = model.PostID });
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }           
        }

        public ActionResult DeletePost(Guid postID)
        {
            var model = BlogPostModel.GetPost(postID);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(BlogPostModel model)
        {
            try
            {
                BlogPostModel.DeletePost(model.PostID);
            }
            catch
            {
                return RedirectToAction("PostAndComments", "Blog", new { id = model.PostID });
            }

            return RedirectToAction("Index", "Blog", new { id = model.AuthorID });
        }

        [ChildActionOnly]
        public ActionResult PostTags(Guid postID)
        {
            var model = BlogPostModel.GetPostTags(postID);

            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult UserTags(Guid userID)
        {
            var model = BlogPostModel.GetUserTags(userID);

            return PartialView(model);
        }
              
        public ActionResult GetUserPostsByTag(Guid authorID, Guid postID, string title)
        {
            var tag = new Tag()
            {
                AuthorID = authorID,
                PostID = postID,
                Title = title,
            };

            var model = BlogPostModel.GetUserPostsByTag(tag).ToList();

            if (model.Count != 0)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Blog", new { id = authorID });
            }
        }

        [ChildActionOnly]
        public ActionResult UpdateTags(Guid postID, Guid authorID)
        {
            TagString model = new TagString()
            {
                PostID = postID,
                AuthorID = authorID,
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTags(TagString model)
        {
            try
            {
                BlogPostModel.UpdateTags(model);
            }
            catch
            {
                return RedirectToAction("PostAndComments", "Blog", new { id = model.PostID });
            }

            return RedirectToAction("Index", "Blog", new { id = model.AuthorID });
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