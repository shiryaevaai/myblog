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

    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Details/5
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(BlogUserModel model, string returnUrl)
        {
            // returnUrl
            if (ModelState.IsValid)
            {
                if (model.TryToLogin(model.BlogUserLogin, model.BlogUserPassword))
                {
                    try
                    {
                        return Redirect(returnUrl);
                    }
                    catch
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult UserInfo()
        {
            return PartialView();
        }

        public ActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(ConfirmationModel model)
        {
            if (model.Confirm)
            {
                BlogUserModel.Logout();
            }

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Account/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
