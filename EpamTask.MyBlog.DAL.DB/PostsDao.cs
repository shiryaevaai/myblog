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

    public class PostsDao : IPostsDao
    {
        private static string connectionString;

        public PostsDao()
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

        public bool AddPost(BlogPost post)
        {
            using (var con = new SqlConnection(connectionString))
            {
                //var command = new SqlCommand("dbo.AddPost", con)
                //{
                //    CommandType = System.Data.CommandType.StoredProcedure,
                //};

                var command = new SqlCommand(
                   "INSERT INTO dbo.[BlogPosts] ([ID], [AuthorID], [Text], " +
                   "[CreationTime], [Title], [Privacy]) " +
                   "VALUES (@ID, @AuthorID, @Text, @CreationTime, @Title, @Privacy)", con);

                command.Parameters.Add(new SqlParameter("@ID", post.PostID));
                command.Parameters.Add(new SqlParameter("@AuthorID", post.AuthorID));
                command.Parameters.Add(new SqlParameter("@Title", post.PostTitle));
                command.Parameters.Add(new SqlParameter("@CreationTime", post.PostCreationTime));
                command.Parameters.Add(new SqlParameter("@Text", post.PostContent));
                command.Parameters.Add(new SqlParameter("@Privacy", post.Privacy));                

                con.Open();
                var reader = command.ExecuteNonQuery();
                return reader > 0 ? true : false;
            }
        }

        public bool UpdatePost(BlogPost post)
        {
            using (var con = new SqlConnection(connectionString))
            {
                //var command = new SqlCommand("dbo.UpdatePost", con)
                //{
                //    CommandType = System.Data.CommandType.StoredProcedure,
                //};
                var command = new SqlCommand(
                   "UPDATE dbo.[BlogPosts] " +
                   "SET [AuthorID]=@AuthorID, [Title]=@Title, " +
                   "[CreationTime]=@CreationTime, [Text]=@Text, [Privacy]=@Privacy " +
                   "WHERE [ID]=@ID", con);

                command.Parameters.Add(new SqlParameter("@ID", post.PostID));
                command.Parameters.Add(new SqlParameter("@AuthorID", post.AuthorID));
                command.Parameters.Add(new SqlParameter("@Title", post.PostTitle));
                command.Parameters.Add(new SqlParameter("@CreationTime", post.PostCreationTime));
                command.Parameters.Add(new SqlParameter("@Text", post.PostContent));
                command.Parameters.Add(new SqlParameter("@Privacy", post.Privacy));

                con.Open();
                var reader = command.ExecuteNonQuery();
                return reader > 0 ? true : false;
            }
        }

        public bool DeletePost(Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("DELETE FROM dbo.[BlogPosts] WHERE [ID] = @ID", con);

                command.Parameters.Add(new SqlParameter("@ID", id));

                con.Open();
                var reader = command.ExecuteNonQuery();

                return reader > 0 ? true : false;
            }
        }

        public BlogPost GetPost(Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                //var command = new SqlCommand("dbo.GetPost", con)
                //{
                //    CommandType = System.Data.CommandType.StoredProcedure,
                //};

                var command = new SqlCommand("SELECT TOP 1 [ID], [AuthorID], [Title], " +
                    "[CreationTime], [Text], [Privacy] " +
                    "FROM dbo.[BlogPosts] WHERE [ID] = @id", con);

                command.Parameters.Add(new SqlParameter("@id", id));

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new BlogPost()
                    {
                        PostID = (Guid)reader["ID"],
                        AuthorID = (Guid)reader["AuthorID"],
                        PostTitle = (string)reader["Title"],
                        PostCreationTime = (DateTime)reader["CreationTime"],
                        PostContent = (string)reader["Text"],
                        Privacy = (string)reader["Privacy"],
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<BlogPost> GetAllPosts()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT [ID], [AuthorID], [Title], " +
                    "[CreationTime], [Text], [Privacy] " +
                    "FROM dbo.[BlogPosts]", con);

                //var command = new SqlCommand("dbo.GetAllPosts", con)
                //{
                //    CommandType = System.Data.CommandType.StoredProcedure,
                //};

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new BlogPost()
                    {
                        PostID = (Guid)reader["ID"],
                        AuthorID = (Guid)reader["AuthorID"],
                        PostTitle = (string)reader["Title"],
                        PostCreationTime = (DateTime)reader["CreationTime"],
                        PostContent = (string)reader["Text"],
                        Privacy = (string)reader["Privacy"],
                    };
                }
            }
        }

        public IEnumerable<BlogPost> GetUserPosts(Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT [ID], [AuthorID], [Title], " +
                    "[CreationTime], [Text], [Privacy] " +
                    "FROM dbo.[BlogPosts] WHERE [AuthorID] = @id ORDER BY [CreationTime] DESC", con);

                //var command = new SqlCommand("dbo.GetUserPosts", con)
                //{
                //    CommandType = System.Data.CommandType.StoredProcedure,
                //};
                command.Parameters.Add(new SqlParameter("@ID", id));

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new BlogPost()
                    {
                        PostID = (Guid)reader["ID"],
                        AuthorID = (Guid)reader["AuthorID"],
                        PostTitle = (string)reader["Title"],
                        PostCreationTime = (DateTime)reader["CreationTime"],
                        PostContent = (string)reader["Text"],
                        Privacy = (string)reader["Privacy"],
                    };
                }
            }
        }
    }
}
