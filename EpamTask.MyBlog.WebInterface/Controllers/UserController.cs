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
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserInfo(Guid id)
        {
            var model = BlogUserModel.GetUser(id);
            return View(model);
        }
	}
}