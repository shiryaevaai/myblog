namespace EpamTask.MyBlog.DAL.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EpamTask.MyBlog.Entities;

    public interface ICommentsDao
    {
        bool AddComment(PostComment comment);

        bool UpdateComment(PostComment comment);

        bool DeleteComment(Guid commentID);

        PostComment GetComment(Guid commentID);

        IEnumerable<PostComment> GetPostComments(Guid postID);
    }
}
