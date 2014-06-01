namespace EpamTask.MyBlog.WebInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using EpamTask.MyBlog.Logic;
    using EpamTask.MyBlog.Entities;

    public class BusinessLogicHelper
    {
        public static MyBlogLogic _logic = new MyBlogLogic();
    }
}