namespace EpamTask.MyBlog.DAL.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EpamTask.MyBlog.DAL.Abstract;
    using EpamTask.MyBlog.Entities;

    public class CommentsDao : ICommentsDao
    {
        private static string connectionString;

        public CommentsDao()
        {
            try
            {
                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyBlogDBConnection"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw new Exception(" ", ex);
            }
        }

        public bool AddComment(PostComment comment)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("dbo.AddComment", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.Add(new SqlParameter("@ID", comment.CommentID));
                command.Parameters.Add(new SqlParameter("@AuthorID", comment.AuthorID));
                command.Parameters.Add(new SqlParameter("@PostID", comment.PostID));
                command.Parameters.Add(new SqlParameter("@Text", comment.CommentText));
                command.Parameters.Add(new SqlParameter("@CreationTime", comment.CommentCreationTime));

                con.Open();
                var reader = command.ExecuteNonQuery();
                return reader > 0 ? true : false;
            }
        }

        public bool UpdateComment(PostComment comment)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("dbo.UpdateComment", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.Add(new SqlParameter("@ID", comment.CommentID));
                command.Parameters.Add(new SqlParameter("@AuthorID", comment.AuthorID));
                command.Parameters.Add(new SqlParameter("@PostID", comment.PostID));
                command.Parameters.Add(new SqlParameter("@Text", comment.CommentText));
                command.Parameters.Add(new SqlParameter("@CreationTime", comment.CommentCreationTime));

                con.Open();
                var reader = command.ExecuteNonQuery();
                return reader > 0 ? true : false;
            }
        }

        public bool DeleteComment(Guid commentID)
        {
            throw new NotImplementedException();
        }

        public PostComment GetComment(Guid commentID)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("dbo.GetComment", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.Add(new SqlParameter("@id", commentID));

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new PostComment()
                    {
                        CommentID = (Guid)reader["ID"],
                        AuthorID = (Guid)reader["AuthorID"],
                        PostID = (Guid)reader["PostID"],
                        CommentText = (string)reader["Text"],
                        CommentCreationTime = (DateTime)reader["CreationTime"],                       
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<PostComment> GetPostComments(Guid postID)
        {
            using (var con = new SqlConnection(connectionString))
            {              
                var command = new SqlCommand("dbo.GetPostComments", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };
                command.Parameters.Add(new SqlParameter("@ID", postID));

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new PostComment()
                    {
                        CommentID = (Guid)reader["ID"],
                        AuthorID = (Guid)reader["AuthorID"],
                        PostID = (Guid)reader["PostID"],
                        CommentText = (string)reader["Text"],
                        CommentCreationTime = (DateTime)reader["CreationTime"],
                    };
                }
            }
        }
    }
}
