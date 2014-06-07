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

    using EpamTask.MyBlog.Logic;
    using EpamTask.MyBlog.Entities;

    public class BlogPostModel : IEquatable<BlogPostModel>
    {        
        public BlogPostModel() 
        { 
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
        [Display(Name = "Название")]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "Необходимо ввести содержание записи!")]
        [Display(Name = "Контент")]
        public string PostContent { get; set; }

        public string Privacy { get; set; }

        public void LikePost(Guid userID)
        {
            //this.likesList.Add(userID);
        }

        public void DislikePost(Guid userID)
        {
            //this.dislikesList.Add(userID);
        }

        public void AddTag(string tag)
        {
            //this.postTagList.Add(tag);
        }

        public void AddComment(string comment)
        {
            //this.postCommentList.Add(comment);
        }

        bool IEquatable<BlogPostModel>.Equals(BlogPostModel other)
        {
            return Equals(other);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            BlogPostModel postObj = obj as BlogPostModel;
            if (postObj == null)
            {
                return false;
            }
            else
            {

                if (this.PostID == postObj.PostID)
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
            string g = this.PostID.ToString();
            for (int i = 0; i < g.Length; i++)
            {
                if (Char.IsDigit(g[i]))
                {
                    res += Int16.Parse(g[i].ToString());
                }
            }

            return res;
        }
        //================================

        public static bool CreatePost(BlogPostModel model)
        {
            var post = new BlogPost()
            {
                PostID = Guid.NewGuid(),
                AuthorID = model.AuthorID,
                PostTitle = model.PostTitle,
                PostContent = model.PostContent,
                PostCreationTime = DateTime.Now,
                Privacy = "",
             };

            model.PostID = post.PostID;
            return BusinessLogicHelper._logic.AddPost(post);
        }

        public static IEnumerable<BlogPostModel> GetUserPosts(Guid id)
        {
            var posts = BusinessLogicHelper._logic.GetUserPosts(id).ToList();
            foreach (var item in posts)
            {
                BlogPostModel post = new BlogPostModel()
                {
                    PostID = item.PostID,
                    AuthorID = item.AuthorID,
                    PostTitle = item.PostTitle,
                    PostContent = item.PostContent,
                    PostCreationTime = item.PostCreationTime,
                    Privacy = item.Privacy,
                };

                yield return post;
            }
        }

        public static IEnumerable<BlogPostModel> GetAllPosts()
        {
            var posts = BusinessLogicHelper._logic.GetAllPosts().ToList();
            foreach (var item in posts)
            {
                BlogPostModel post = new BlogPostModel()
                {
                    PostID = item.PostID,
                    AuthorID = item.AuthorID,
                    PostTitle = item.PostTitle,
                    PostContent = item.PostContent,
                    PostCreationTime = item.PostCreationTime,
                    Privacy = item.Privacy,
                };

                yield return post;
            }
        }
    }
}