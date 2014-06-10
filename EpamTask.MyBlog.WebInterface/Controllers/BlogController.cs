namespace EpamTask.MyBlog.WebInterface.Controllers
{
    using EpamTask.MyBlog.Logic;
    using EpamTask.MyBlog.Entities;
    using log4net;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;
    using WebInterface.Models;

    public class BlogController : Controller
    {
        public ActionResult Index(Guid id)
        {
            try
            {
                return View(id);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [ChildActionOnly]
        public ActionResult GetUserPosts(Guid id)
        {
            try
            {
                var model = BlogPostModel.GetUserPosts(id).ToList();
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [ChildActionOnly]
        public ActionResult GetAllPosts()
        {
            try
            {
                var model = BlogPostModel.GetAllPosts().ToList();
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        public ActionResult CreatePost(Guid id)
        {
            try
            {
                BlogPostModel model = new BlogPostModel()
                {
                    AuthorID = id,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(BlogPostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (BlogPostModel.CreatePost(model))
                    {
                        return RedirectToAction("Index", "Blog", new { id = model.AuthorID });
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }            
        }

        public ActionResult PostAndComments(Guid id)
        {
            try
            {
                BlogPostModel model = BlogPostModel.GetPost(id);
                return View(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [ChildActionOnly]
        public ActionResult PostComments(Guid id)
        {
            try
            {
                var model = BlogPostModel.GetPostComments(id);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [ChildActionOnly]
        public ActionResult AddComment(Guid postID, Guid authorID)
        {
            try
            {
                var model = new CommentModel()
                {
                    PostID = postID,
                    AuthorID = authorID,
                };

                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(CommentModel model)
        {
            try
            {
                if (CommentModel.AddComment(model))
                {
                    return RedirectToAction("Index", "Blog", new { id = BlogPostModel.GetPost(model.PostID).AuthorID });
                }
                else
                {
                    return RedirectToAction("PostAndComments", "Blog", new { id = BlogPostModel.GetPost(model.PostID).AuthorID });
                }
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        public ActionResult EditComment(Guid commentID)
        {
            try
            {
                var model = CommentModel.GetComment(commentID);
                return View(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
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
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }        
        }

        public ActionResult DeleteComment(Guid commentID)
        {
            try
            {
                var model = CommentModel.GetComment(commentID);
                return View(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(CommentModel model)
        {
            try
            {
                if (CommentModel.DeleteComment(model.CommentID))
                {
                    return RedirectToAction("PostAndComments", "Blog", new { id = model.PostID });
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            } 
        }

        [ChildActionOnly]
        public ActionResult GetCommentsNumber(Guid postID)
        {
            try
            {
                int model = BlogPostModel.GetPostComments(postID).Count();
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            } 
        }

        public ActionResult EditPost(Guid postID)
        {
            try
            {
                var model = new EditPostModel(postID);
                return View(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            } 
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
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }            
        }

        public ActionResult DeletePost(Guid postID)
        {
            try
            {
                var model = BlogPostModel.GetPost(postID);
                return View(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            } 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(BlogPostModel model)
        {
            try
            {
                if (BlogPostModel.DeletePost(model.PostID))
                {
                    return RedirectToAction("PostAndComments", "Blog", new { id = model.PostID });
                }
                else
                {
                    return RedirectToAction("Index", "Blog", new { id = model.AuthorID });
                }
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            } 
        }

        [ChildActionOnly]
        public ActionResult PostTags(Guid postID)
        {
            try
            {
                var model = BlogPostModel.GetPostTags(postID);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            } 
        }

        [ChildActionOnly]
        public ActionResult UserTags(Guid userID)
        {
            try
            {
                var model = BlogPostModel.GetUserTags(userID);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            } 
        }
              
        public ActionResult GetUserPostsByTag(Guid authorID, Guid postID, string title)
        {
            try
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
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            } 
        }

        [ChildActionOnly]
        public ActionResult UpdateTags(Guid postID, Guid authorID)
        {
            try
            {
                TagString model = new TagString()
                {
                    PostID = postID,
                    AuthorID = authorID,
                };

                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            } 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTags(TagString model)
        {
            try
            {
                if (BlogPostModel.UpdateTags(model))
                {
                    return RedirectToAction("PostAndComments", "Blog", new { id = model.PostID });
                }
                else
                {
                    return RedirectToAction("Index", "Blog", new { id = model.AuthorID });
                }
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(BlogController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            } 
        }
	}
}