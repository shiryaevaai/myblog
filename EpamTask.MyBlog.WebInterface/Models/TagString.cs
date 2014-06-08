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

    using EpamTask.MyBlog.Logic;
    using EpamTask.MyBlog.Entities;

    public class TagString
    {
        //[Required(ErrorMessage = "Необходимо ввести текст тэга!")]
        [Display(Name = "Введите тэги, разделенные пробелами:")]
        public string Tags { get; set; }

        //=============================================
        [HiddenInput(DisplayValue = false)]
        public Guid AuthorID { get; set; }

        //=============================================
        [HiddenInput(DisplayValue = false)]
        public Guid PostID { get; set; }

        public TagString()
        {

        }

        //internal static void AddTags(TagString model)
        //{
        //    throw new NotImplementedException();
        //}
    }
}