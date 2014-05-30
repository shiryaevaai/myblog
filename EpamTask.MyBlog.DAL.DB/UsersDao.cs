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

    public class UsersDao : IUsersDao
    {
        private static string connectionString;

        public UsersDao()
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

        public bool AddUser(BlogUser user)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("dbo.AddUser", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.Add(new SqlParameter("@ID", user.ID));
                command.Parameters.Add(new SqlParameter("@BlogUserLogin", user.BlogUserLogin));
                command.Parameters.Add(new SqlParameter("@BlogUserPassword", user.BlogUserPassword));
                command.Parameters.Add(new SqlParameter("@BirthDate", user.BirthDate));
                command.Parameters.Add(new SqlParameter("@Email", user.Email));
                command.Parameters.Add(new SqlParameter("@RegistrationTime", user.RegistrationTime));
                
                con.Open();
                var reader = command.ExecuteNonQuery();
                return reader > 0 ? true : false;
            }
        }

        public bool UpdateUser(BlogUser user)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("dbo.UpdateUser", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.Add(new SqlParameter("@ID", user.ID));
                command.Parameters.Add(new SqlParameter("@BlogUserLogin", user.BlogUserLogin));
                command.Parameters.Add(new SqlParameter("@BlogUserPassword", user.BlogUserPassword));
                command.Parameters.Add(new SqlParameter("@BirthDate", user.BirthDate));
                command.Parameters.Add(new SqlParameter("@Email", user.Email));
                command.Parameters.Add(new SqlParameter("@RegistrationTime", user.RegistrationTime));

                command.Parameters.Add(new SqlParameter("@BlogUserEpigraph", user.BlogUserEpigraph));
                command.Parameters.Add(new SqlParameter("@About", user.About));
                command.Parameters.Add(new SqlParameter("@Sex", user.Sex));
                command.Parameters.Add(new SqlParameter("@Skype", user.Skype));
                command.Parameters.Add(new SqlParameter("@HasAvatar", user.HasAvatar));

                con.Open();
                var reader = command.ExecuteNonQuery();
                return reader > 0 ? true : false;
            }
        }

        public bool DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public BlogUser GetUser(Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                // var command = new SqlCommand("SELECT TOP 1 [ID], [Name], [DateOfBirth] FROM dbo.[Users] WHERE [ID] = @id", con);
                var command = new SqlCommand("dbo.GetUser", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                }; 

                command.Parameters.Add(new SqlParameter("@id", id));

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new BlogUser()
                    {
                        ID = (Guid)reader["ID"],
                        BlogUserLogin = (string)reader["Login"],
                        BlogUserPassword = (string)reader["Password"],
                        BirthDate = (DateTime)reader["BirthDate"],
                        Email = (string)reader["Email"],
                        RegistrationTime = (DateTime)reader["RegistrationTime"],
                        BlogUserEpigraph = (string)reader["Epigraph"],
                        About = (string)reader["About"],
                        Sex = (bool)reader["Sex"],
                        Skype = (string)reader["Skype"],
                        HasAvatar = (bool)reader["HasAvatar"],
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<BlogUser> GetAllUsers()
        {
            using (var con = new SqlConnection(connectionString))
            {
                // var command = new SqlCommand("SELECT [ID], [Name], [DateOfBirth] FROM dbo.[Users]", con);
                var command = new SqlCommand("dbo.GetAllUsers", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                }; 

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new BlogUser()
                    {
                        ID = (Guid)reader["ID"],
                        BlogUserLogin = (string)reader["Login"],
                        BlogUserPassword = (string)reader["Password"],
                        BirthDate = (DateTime)reader["BirthDate"],
                        Email = (string)reader["Email"],
                        RegistrationTime = (DateTime)reader["RegistrationTime"],
                        BlogUserEpigraph = (string)reader["Epigraph"],
                        About = (string)reader["About"],
                        Sex = (bool)reader["Sex"],
                        Skype = (string)reader["Skype"],
                        HasAvatar = (bool)reader["HasAvatar"],
                    };
                }
            } 
        }

        public bool SetUserAvatar(Guid id, byte[] img)
        {
            try
            {
                BlogUser user = this.GetUser(id);
                if (user.HasAvatar)
                {
                    using (var con = new SqlConnection(connectionString))
                    {
                        // ! update
                        var command = new SqlCommand("UPDATE INTO dbo.UserAvatars (ID, Image) VALUES (@ID, @Image)", con);
                        command.Parameters.Add(new SqlParameter("@ID", id));
                        command.Parameters.Add(new SqlParameter("@Image", img));

                        con.Open();
                        var reader = command.ExecuteNonQuery();

                        return reader > 0 ? true : false;
                    }
                }
                else
                { 
                    using (var con = new SqlConnection(connectionString))
                    {
                        var command = new SqlCommand("INSERT INTO dbo.UserAvatars (ID, Image) VALUES (@ID, @Image)", con);
                        command.Parameters.Add(new SqlParameter("@ID", id));
                        command.Parameters.Add(new SqlParameter("@Image", img));

                        con.Open();
                        var reader = command.ExecuteNonQuery();

                        return reader > 0 ? true : false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public byte[] GetUserAvatar(Guid id)
        {
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("SELECT TOP 1 [ID], [Image] FROM dbo.UserAvatars WHERE [ID] = @id", con);
                    command.Parameters.Add(new SqlParameter("@id", id));

                    con.Open();
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return (byte[])reader["Image"];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveUserAvatar(Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                // var command = new SqlCommand("DELETE FROM dbo.[Users] WHERE [ID] = @ID", con);
                var command = new SqlCommand("dbo.RemoveUserAvatar", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.Add(new SqlParameter("@ID", id));

                con.Open();
                var reader = command.ExecuteNonQuery();

                return reader > 0 ? true : false;
            }
        }
    }
}
