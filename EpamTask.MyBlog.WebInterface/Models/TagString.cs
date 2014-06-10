namespace EpamTask.MyBlog.WebInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    using EpamTask.MyBlog.Entities;
    using EpamTask.MyBlog.Logic;

    public class TagString
    {
        public TagString()
        {
        }

        [Display(Name = "Введите тэги, разделенные пробелами:")]
        public string Tags { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid AuthorID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid PostID { get; set; }
    }
}