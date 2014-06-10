namespace EpamTask.MyBlog.WebInterface.Controllers
{
    using EpamTask.MyBlog.WebInterface.Models;
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
            Log4NetLogger logger = new Log4NetLogger();
            logger.Info("Test message for Log4Net");

            try
            {
                throw new Exception("A test exception");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR - An error has occurred");
            }

            return View();
        }
    }
}