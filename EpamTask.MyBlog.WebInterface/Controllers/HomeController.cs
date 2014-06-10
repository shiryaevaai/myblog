namespace EpamTask.MyBlog.WebInterface.Controllers
{
    using EpamTask.MyBlog.WebInterface.Models;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {  
            ILog logger = LogManager.GetLogger(typeof(HomeController));
            
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return View("Error.chtml");
            }            
        }
    }
}