namespace EpamTask.MyBlog.DAL.Abstract
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

        IEnumerable<BlogPost> GetUserPosts(Guid id);
    }
}
