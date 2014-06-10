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


       //@Html.ActionLink("Удалить пользователя", "DeleteUser", "Admin", new { accountID = item.ID }, null) 

        // GET: /Account/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRoleFromUser(Guid accountID)
        {
            var model = BlogUserModel.GetUser(accountID);
            model.roleList = MyRoleProvider.GetRolesForUser(accountID).ToList();
            return View(model);
        }

        // POST: /Account/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleFromUser(Guid accountID, Guid roleID)
        {
            try
            {
                MyRoleProvider.DeleteRoleFromAccount(accountID, roleID);
                return RedirectToAction("Index", "Admin");
            }
            catch
            {
                //var model = MyRoleProvider.GetAccount(AccountID);
                return RedirectToAction("Index","Admin");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddRoleToUser(Guid accountID)
        {
            var model = BlogUserModel.GetUser(accountID);
            model.hasNotRoleList = MyRoleProvider.GetNoRolesForUser(accountID).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AddRoleToUser(Guid accountID, Guid roleID)
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

        [Authorize(Roles = "Admin")]
        [ChildActionOnly]
        public ActionResult UserRoleList(Guid accountID)
        {
            var model = MyRoleProvider.GetRolesForUser(accountID);
            return PartialView(model);
        }
	}
}