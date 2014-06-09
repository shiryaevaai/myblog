﻿namespace EpamTask.MyBlog.WebInterface.Controllers
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
   // using System.Web.Http.Cors;

    //[EnableCors("*", "*", "POST")]
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

            return View(model);
        }

       // [ChildActionOnly]
        public ActionResult UserInformation()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult UserNavBar(string username)
        {
            var model = BlogUserModel.GetUser(username);
            return PartialView(model);
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

        [HttpGet]
        public ActionResult CreateAccount()
        {
            //RegistrationModel model = new BlogUserModel() { };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(RegistrationModel model)
        {
            BlogUserModel.CreateAccount(model);
            var user = BlogUserModel.GetUser(model.Login);
            if (user.TryToLogin(user.BlogUserLogin, user.BlogUserPassword))
            {
                try
                {
                    return RedirectToAction("UserInfo", "User", new { id = user.ID });
                }
                catch
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult CheckAccountName(string Login)
        {
            var result = RegistrationModel.CheckAccountName(Login);

            if (result)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Этот логин уже занят.", JsonRequestBehavior.AllowGet);
            }
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult CheckRegEmail(string Email)
        {
            var result = RegistrationModel.CheckEmail(Email);

            if (result)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Этот адрес электронной почты уже использовался для регистрации.", JsonRequestBehavior.AllowGet);
            }
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult CheckEmail(string Email, Guid ID)
        {
            var result = EditUserModel.CheckEmail(Email, ID);

            if (result)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Этот адрес электронной почты уже использовался для регистрации.", JsonRequestBehavior.AllowGet);
            }
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult ConfirmPassword(string PasswordConfirmation, string Password)
        {
            var result = RegistrationModel.ConfirmPassword(Password, PasswordConfirmation);

            if (result)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Подтверждение должно быть эквивалентно паролю!", JsonRequestBehavior.AllowGet);
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
