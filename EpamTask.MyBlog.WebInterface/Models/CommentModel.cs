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

    public class CommentModel : IEquatable<CommentModel>
    {
        public CommentModel()
        {
        }

        [HiddenInput(DisplayValue = false)]
        public Guid CommentID { get; set; }

        //=============================================
        [HiddenInput(DisplayValue = false)]
        public Guid AuthorID { get; set; }

        //=============================================
        [HiddenInput(DisplayValue = false)]
        public Guid PostID { get; set; }

        //=============================================
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата создания")]
        public DateTime CommentCreationTime { get; set; }

        //=============================================
        [Required(ErrorMessage = "Необходимо ввести текст комментария!")]
        [Display(Name = "Комментарий:")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Длина текста комментария должна быть от 3 до 250 символов")]
        public string CommentText { get; set; }

         public static bool AddComment(CommentModel model)
        {
            var comment = new PostComment()
            {
                CommentID = Guid.NewGuid(),
                AuthorID = model.AuthorID,
                PostID = model.PostID,
                CommentCreationTime = DateTime.Now,
                CommentText = model.CommentText,
            };

            return BusinessLogicHelper._logic.AddComment(comment);
        }

        public static bool DeleteComment(Guid guid)
        {
            return BusinessLogicHelper._logic.DeleteComment(guid);
        }

        public static CommentModel GetComment(Guid id)
        {
            var item = BusinessLogicHelper._logic.GetComment(id);
            return new CommentModel()
            {
                CommentID = item.CommentID,
                PostID = item.PostID,
                AuthorID = item.AuthorID,
                CommentCreationTime = item.CommentCreationTime,
                CommentText = item.CommentText,
            };
        }

        public static bool UpdateComment(CommentModel model)
        {
            var comment = new PostComment()
            {
                CommentID = model.CommentID,
                PostID = model.PostID,
                AuthorID = model.AuthorID,
                CommentCreationTime = model.CommentCreationTime,
                CommentText = model.CommentText,
            };

            return BusinessLogicHelper._logic.UpdateComment(comment);
        }

        bool IEquatable<CommentModel>.Equals(CommentModel other)
        {
            return this.Equals(other);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            CommentModel comObj = obj as CommentModel;
            if (comObj == null)
            {
                return false;
            }
            else
            {
                if (this.CommentID == comObj.CommentID)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override int GetHashCode()
        {
            int res = 0;
            string g = this.CommentID.ToString();
            for (int i = 0; i < g.Length; i++)
            {
                if (Char.IsDigit(g[i]))
                {
                    res += Int16.Parse(g[i].ToString());
                }
            }

            return res;
        }
    }
}