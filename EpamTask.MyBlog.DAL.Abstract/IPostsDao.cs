﻿namespace EpamTask.MyBlog.DAL.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EpamTask.MyBlog.Entities;

    public interface IPostsDao
    {
        bool AddPost(BlogPost post);

        bool UpdatePost(BlogPost post);

        bool DeletePost(Guid id);

        BlogPost GetPost(Guid id);

        IEnumerable<BlogPost> GetAllPosts();

        IEnumerable<BlogPost> GetUserPosts(Guid userID);

        bool AddTagToPost(Tag tag);

        bool DeletePostTags(Guid postID);

        IEnumerable<Tag> GetPostTags(Guid postID);

        IEnumerable<Tag> GetUserTags(Guid userID);

        IEnumerable<BlogPost> GetPostsByTag(string tag);

        IEnumerable<BlogPost> GetUserPostsByTag(Tag tag);

        IEnumerable<BlogPost> GetPostsByText(string text);
    }
}
