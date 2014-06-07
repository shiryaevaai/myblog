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
                //var command = new SqlCommand("dbo.AddComment", con)
                //{
                //    CommandType = System.Data.CommandType.StoredProcedure,
                //};

                var command = new SqlCommand(
                   "INSERT INTO dbo.[Comments] ([ID], [AuthorID], [PostID], " +
                   "[Text], [CreationDate]) " +
                   "VALUES (@ID, @AuthorID, @PostID, @Text, @CreationDate)", con);

                command.Parameters.Add(new SqlParameter("@ID", comment.CommentID));
                command.Parameters.Add(new SqlParameter("@AuthorID", comment.AuthorID));
                command.Parameters.Add(new SqlParameter("@PostID", comment.PostID));
                command.Parameters.Add(new SqlParameter("@Text", comment.CommentText));
                command.Parameters.Add(new SqlParameter("@CreationDate", comment.CommentCreationTime));

                con.Open();
                var reader = command.ExecuteNonQuery();
                return reader > 0 ? true : false;
            }
        }

        public bool UpdateComment(PostComment comment)
        {
            using (var con = new SqlConnection(connectionString))
            {
                //var command = new SqlCommand("dbo.UpdateComment", con)
                //{
                //    CommandType = System.Data.CommandType.StoredProcedure,
                //};

                var command = new SqlCommand(
                   "UPDATE dbo.[BlogPosts] " +
                   "SET [AuthorID]=@AuthorID, [PostID]=@PostID, " +
                   "[CreationDate]=@CreationDate, [Text]=@Text " +
                   "WHERE [ID]=@ID", con);

                command.Parameters.Add(new SqlParameter("@ID", comment.CommentID));
                command.Parameters.Add(new SqlParameter("@AuthorID", comment.AuthorID));
                command.Parameters.Add(new SqlParameter("@PostID", comment.PostID));
                command.Parameters.Add(new SqlParameter("@Text", comment.CommentText));
                command.Parameters.Add(new SqlParameter("@CreationDate", comment.CommentCreationTime));

                con.Open();
                var reader = command.ExecuteNonQuery();
                return reader > 0 ? true : false;
            }
        }

        public bool DeleteComment(Guid commentID)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("DELETE FROM dbo.[Comments] WHERE [ID] = @ID", con);

                command.Parameters.Add(new SqlParameter("@ID", commentID));

                con.Open();
                var reader = command.ExecuteNonQuery();

                return reader > 0 ? true : false;
            }
        }

        public PostComment GetComment(Guid commentID)
        {
            using (var con = new SqlConnection(connectionString))
            {
                //var command = new SqlCommand("dbo.GetComment", con)
                //{
                //    CommandType = System.Data.CommandType.StoredProcedure,
                //};
                var command = new SqlCommand("SELECT TOP 1 [ID], [AuthorID], [PostID], " +
                    "[CreationDate], [Text] " +
                    "FROM dbo.[Comments] WHERE [ID] = @id", con);

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
                        CommentCreationTime = (DateTime)reader["CreationDate"],                       
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
                //var command = new SqlCommand("dbo.GetPostComments", con)
                //{
                //    CommandType = System.Data.CommandType.StoredProcedure,
                //};
                var command = new SqlCommand("SELECT [ID], [PostID], [AuthorID], " +
                   "[CreationDate], [Text] " +
                   "FROM dbo.[Comments] WHERE [PostID]=@PostID ORDER BY [CreationDate]", con);

                command.Parameters.Add(new SqlParameter("@PostID", postID));

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
                        CommentCreationTime = (DateTime)reader["CreationDate"],
                    };
                }
            }
        }
    }
}
