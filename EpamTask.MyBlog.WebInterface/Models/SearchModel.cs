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

    public class SearchModel
    {
        public string SearchText { get; set; }

        public SearchModel()
        { 
        }

        public SearchModel(string searchText)
        {
            this.SearchText = searchText;
        }
        public static IEnumerable<BlogPostModel> SearchPostsByTag(string tag)
        {
            var posts = BusinessLogicHelper._logic.GetPostsByTag(tag).ToList();
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

        public static IEnumerable<BlogPostModel> SearchPostsByText(string text)
        {
            var posts = BusinessLogicHelper._logic.GetPostsByText(text).ToList();
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