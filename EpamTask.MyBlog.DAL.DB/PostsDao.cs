using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpamTask.MyBlog.DAL.Abstract;
using EpamTask.MyBlog.Entities;

namespace EpamTask.MyBlog.DAL.DB
{    
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

        bool AddPost(BlogPost post)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("dbo.AddPost", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.Add(new SqlParameter("@ID", post.PostID));
                command.Parameters.Add(new SqlParameter("@AuthorID", post.AuthorID));
                command.Parameters.Add(new SqlParameter("@Title", post.PostTitle));
                command.Parameters.Add(new SqlParameter("@CreationTime", post.PostCreationTime));
                command.Parameters.Add(new SqlParameter("@Content", post.PostContent));
                command.Parameters.Add(new SqlParameter("@Privacy", post.Privacy));
                

                con.Open();
                var reader = command.ExecuteNonQuery();
                return reader > 0 ? true : false;
            }
        }

        bool UpdatePost(BlogPost post)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("dbo.UpdatePost", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.Add(new SqlParameter("@ID", post.PostID));
                command.Parameters.Add(new SqlParameter("@AuthorID", post.AuthorID));
                command.Parameters.Add(new SqlParameter("@Title", post.PostTitle));
                command.Parameters.Add(new SqlParameter("@CreationTime", post.PostCreationTime));
                command.Parameters.Add(new SqlParameter("@Content", post.PostContent));
                command.Parameters.Add(new SqlParameter("@Privacy", post.Privacy));


                con.Open();
                var reader = command.ExecuteNonQuery();
                return reader > 0 ? true : false;
            }
        }

        bool DeletePost(Guid id)
        {
            throw new NotImplementedException();
        }

        BlogPost GetPost(Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("dbo.GetPost", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

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
                        PostContent = (string)reader["Content"],
                        Privacy = (string)reader["Privacy"],
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        IEnumerable<BlogPost> GetAllPosts()
        {
            using (var con = new SqlConnection(connectionString))
            {
                // var command = new SqlCommand("SELECT [ID], [Name], [DateOfBirth] FROM dbo.[Users]", con);
                var command = new SqlCommand("dbo.GetAllPosts", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

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
                        PostContent = (string)reader["Content"],
                        Privacy = (string)reader["Privacy"],
                    };
                }
            }
        }

        IEnumerable<BlogPost> GetUserPosts(Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                // var command = new SqlCommand("SELECT [ID], [Name], [DateOfBirth] FROM dbo.[Users]", con);
                var command = new SqlCommand("dbo.GetUserPosts", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };
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
                        PostContent = (string)reader["Content"],
                        Privacy = (string)reader["Privacy"],
                    };
                }
            }
        }
    }
}
