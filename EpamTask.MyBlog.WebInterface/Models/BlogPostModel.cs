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
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина названия должна быть от 3 до 100 символов")]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "Необходимо ввести содержание записи!")]
        [Display(Name = "Контент")]
        public string PostContent { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 20 символов")]
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

        public static BlogPostModel GetPost(Guid postID)
        {
            var item = BusinessLogicHelper._logic.GetPost(postID);

            return new BlogPostModel()
            {
                PostID = item.PostID,
                AuthorID = item.AuthorID,
                PostTitle = item.PostTitle,
                PostContent = item.PostContent,
                PostCreationTime = item.PostCreationTime,
                Privacy = item.Privacy,
            };             
        }

        public static IEnumerable<CommentModel> GetPostComments(Guid postID)
        {
            var comments = BusinessLogicHelper._logic.GetPostComments(postID).ToList();
            foreach (var item in comments)
            {
                yield return new CommentModel()
                {
                    CommentID = item.CommentID,
                    PostID = item.PostID,
                    AuthorID = item.AuthorID,
                    CommentCreationTime = item.CommentCreationTime,
                    CommentText = item.CommentText, 
                };
            }
        }

        public static void DeletePost(Guid guid)
        {
            BusinessLogicHelper._logic.DeletePost(guid);
        }

        public static void UpdateTags(TagString model)
        {
            if (!string.IsNullOrWhiteSpace(model.Tags))
            {
                var tagArray = model.Tags.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                BusinessLogicHelper._logic.DeletePostTags(model.PostID);

                if (tagArray.Count() != 0)
                {
                    foreach (string tag in tagArray)
                    {
                        Tag newTag = new Tag()
                        {
                            AuthorID = model.AuthorID,
                            PostID = model.PostID,
                            Title = tag,
                        };

                        BusinessLogicHelper._logic.AddTagToPost(newTag);
                    }
                }
            }
            else
            {
                BusinessLogicHelper._logic.DeletePostTags(model.PostID);
            }            
        }

        public static IEnumerable<Tag> GetPostTags(Guid postID)
        {
            return BusinessLogicHelper._logic.GetPostTags(postID);
        }

        public static IEnumerable<Tag> GetUserTags(Guid userID)
        {
            return BusinessLogicHelper._logic.GetUserTags(userID);
        }

        public static IEnumerable<BlogPostModel> GetUserPostsByTag(Tag tag)
        {
            var posts = BusinessLogicHelper._logic.GetUserPostsByTag(tag).ToList();
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