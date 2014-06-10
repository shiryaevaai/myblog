namespace EpamTask.MyBlog.WebInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using EpamTask.MyBlog.Entities;
    using EpamTask.MyBlog.Logic;

    public class BusinessLogicHelper
    {
        public static MyBlogLogic _logic = new MyBlogLogic();
    }
}