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

    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            try
            {
                var model = BusinessLogicHelper._logic.GetAllUsers();
                return View(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(AdminController));
                logger.Error(ex.Message.ToString(), ex);
                return View("Error.chtml");
            }
        }
        
        [ChildActionOnly]
        public ActionResult UserInfo()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(AccountController));
                logger.Error(ex.Message, ex);
                return View("Error.chtml");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRoleFromUser(Guid accountID)
        {
            try
            {
                var model = BlogUserModel.GetUser(accountID);
                model.roleList = MyRoleProvider.GetRolesForUser(accountID).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(AccountController));
                logger.Error(ex.Message, ex);
                return View("Error.chtml");
            }
        }

        // POST: /Account/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleFromUser(Guid accountID, Guid roleID)
        {
            try
            {
                if (MyRoleProvider.DeleteRoleFromAccount(accountID, roleID))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("DeleteRoleFromUser", new { accountID = accountID });
                }
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(AccountController));
                logger.Error(ex.Message, ex);
                return View("Error.chtml");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddRoleToUser(Guid accountID)
        {
            try
            {
                var model = BlogUserModel.GetUser(accountID);
                model.hasNotRoleList = MyRoleProvider.GetNoRolesForUser(accountID).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(AccountController));
                logger.Error(ex.Message, ex);
                return View("Error.chtml");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AddRoleToUser(Guid accountID, Guid roleID)
        {
            try
            {
                var model = BlogUserModel.GetUser(accountID);

                if (MyRoleProvider.AddRoleToAccount(accountID, roleID))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(AccountController));
                logger.Error(ex.Message, ex);
                return View("Error.chtml");
            }
        }

        [Authorize(Roles = "Admin")]
        [ChildActionOnly]
        public ActionResult UserRoleList(Guid accountID)
        {
            try
            {
                var model = MyRoleProvider.GetRolesForUser(accountID);
                return PartialView(model);
            }        
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(AccountController));
                logger.Error(ex.Message, ex);
                return View("Error.chtml");
            }
        }
    }
}