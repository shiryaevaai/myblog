namespace EpamTask.MyBlog.WebInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Security;

    using EpamTask.MyBlog.Entities;
    using EpamTask.MyBlog.Logic;

    public class EditPostModel
    {
        public EditPostModel() 
        { 
        }

        public EditPostModel(Guid postID) 
        {
            var post = BusinessLogicHelper._logic.GetPost(postID);
            this.PostID = post.PostID;
            this.AuthorID = post.AuthorID;
            this.PostCreationTime = post.PostCreationTime;
            this.PostTitle = post.PostTitle;
            this.Privacy = post.Privacy;
            this.PostContent = post.PostContent;
        }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public Guid PostID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата создания")]
        public DateTime PostCreationTime { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public Guid AuthorID { get; set; }

        [Required(ErrorMessage = "Необходимо ввести название записи!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина названия должна быть от 3 до 100 символов")]
        [Display(Name = "Название")]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "Необходимо ввести содержание записи!")]
        [Display(Name = "Контент")]
        public string PostContent { get; set; }

        public string Privacy { get; set; }

        //================================
        public static bool UpdatePost(EditPostModel model)
        {
            var post = new BlogPost()
            {
                PostID = model.PostID,
                AuthorID = model.AuthorID,
                PostTitle = model.PostTitle,
                PostContent = model.PostContent,
                PostCreationTime = model.PostCreationTime,
                Privacy = "",
            };

            return BusinessLogicHelper._logic.UpdatePost(post);
        }
    }
}