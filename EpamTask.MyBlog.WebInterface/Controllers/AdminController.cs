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

    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var model = BusinessLogicHelper._logic.GetAllUsers();
            return View(model);
        }
        
        [ChildActionOnly]
        public ActionResult UserInfo()
        {
            return PartialView();
        }

        // GET: /Account/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRole(Guid AccountID)
        {
            var model = BlogUserModel.GetUser(AccountID);
            model.roleList = MyRoleProvider.GetRolesForUser(AccountID).ToList();
            return View(model);
        }

        // POST: /Account/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleFromUser(Guid AccountID, Guid RoleID)
        {
            try
            {
                MyRoleProvider.DeleteRoleFromAccount(AccountID, RoleID);
                return RedirectToAction("Index", "Account");
            }
            catch
            {
                //var model = MyRoleProvider.GetAccount(AccountID);
                return RedirectToAction("Index","Admin");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddRole(Guid AccountID)
        {
            var model = BlogUserModel.GetUser(AccountID);
            model.hasNotRoleList = MyRoleProvider.GetNoRolesForUser(AccountID).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AddRole(Guid AccountID, Guid RoleID)
        {
            var model = BlogUserModel.GetUser(AccountID);
            if (MyRoleProvider.AddRoleToAccount(AccountID, RoleID))
            {
                return RedirectToAction("Index", "Account");
            }
            else
            {
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(Guid id)
        {
            var model = BlogUserModel.GetUser(id);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetAccountRoles(Guid id)
        {
            var model = MyRoleProvider.GetRolesForUser(id);
            return PartialView(model);
        }
	}
}