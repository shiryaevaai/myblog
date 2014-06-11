﻿namespace EpamTask.MyBlog.WebInterface.Controllers
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

    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(AccountController));
                logger.Error(ex.Message, ex);
                return View("Error.chtml");
            }
        }

        public ActionResult Login(string returnUrl)
        {
            try
            {
                ViewBag.returnUrl = returnUrl;
                return View();
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
        public ActionResult Login(BlogUserModel model, string returnUrl)
        {
            try
            {
                if (model.TryToLogin(model.BlogUserLogin, model.BlogUserPassword))
                {
                    if (!String.IsNullOrWhiteSpace(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
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

        [ChildActionOnly]
        public ActionResult UserInformation()
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

        [ChildActionOnly]
        public ActionResult UserNavBar(string username)
        {
            try
            {
                var model = BlogUserModel.GetUser(username);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(AccountController));
                logger.Error(ex.Message, ex);
                return View("Error.chtml");
            }
        }

        public ActionResult Logout()
        {
            try
            {
                return View();
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
        public ActionResult Logout(ConfirmationModel model)
        {
            try
            {
                if (model.Confirm)
                {
                    BlogUserModel.Logout();
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(AccountController));
                logger.Error(ex.Message, ex);
                return View("Error.chtml");
            }
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            try
            {
                return View();
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
        public ActionResult CreateAccount(RegistrationModel model)
        {
            try
            {
                BlogUserModel.CreateAccount(model);
                var user = BlogUserModel.GetUser(model.Login);
                if (user.TryToLogin(user.BlogUserLogin, user.BlogUserPassword))
                {
                    return RedirectToAction("UserInfo", "User", new { userID = user.ID });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ILog logger = LogManager.GetLogger(typeof(AccountController));
                logger.Error(ex.Message, ex);
                return View("Error.chtml");
            }
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult CheckAccountName(string login)
        {
            var result = RegistrationModel.CheckAccountName(login);

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
        public JsonResult CheckRegEmail(string email)
        {
            var result = RegistrationModel.CheckEmail(email);

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
        public JsonResult CheckEmail(string email, Guid ID)
        {
            var result = EditUserModel.CheckEmail(email, ID);

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
    }
}
