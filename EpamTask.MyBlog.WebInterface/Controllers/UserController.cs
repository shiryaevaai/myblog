namespace EpamTask.MyBlog.WebInterface.Controllers
{
    using EpamTask.MyBlog.Entities;
    using EpamTask.MyBlog.Logic;
    using log4net;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;
    using WebInterface.Models;

    public class UserController : Controller
    {
        public ActionResult UserInfo(Guid id)
        {
            try
            {
                var model = BlogUserModel.GetUser(id);
                return View(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(UserController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        public ActionResult EditProfile(Guid id)
        {
            try
            {
                var model = BlogUserModel.GetUser(id);
                EditUserModel m = new EditUserModel()
                {
                    ID = model.ID,
                    BirthDate = model.BirthDate,
                };
                return View(m);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(UserController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditUserModel model)
        {
            try
            {
                if (BlogUserModel.EditProfile(model))
                { 
                     return RedirectToAction("UserInfo", "User", new {id = model.ID});
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(UserController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [ChildActionOnly]
        public ActionResult UploadImage(Guid id)
        {
            try
            {
                Guid UserID = id;
                return PartialView(UserID);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(UserController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(Guid id, HttpPostedFileBase image)
        {
            try
            {
                if (ImageHelper.SetUserAvatar(image, id))
                {
                    return RedirectToAction("UserInfo", "User", new { id = id });
                }
                else
                {
                    return RedirectToAction("EditProfile", new { id = id });
                }
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(UserController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }            
        }

        public ActionResult ShowAvatar(Guid id)
        {
            try
            {
                return File(ImageHelper.GetUserAvatar(id), "image/jpeg");
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(UserController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [ChildActionOnly]
        public ActionResult ShowAvatarOrDefault(Guid id)
        {
            try
            {
                var model = BlogUserModel.GetUser(id);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(UserController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }

        [ChildActionOnly]
        public ActionResult ShowThumbnailAvatarOrDefault(Guid id)
        {
            try
            {
                var model = BlogUserModel.GetUser(id);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(UserController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }
	}
}