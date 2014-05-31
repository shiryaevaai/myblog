namespace EpamTask.MyBlog.DAL.DB
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EpamTask.MyBlog.DAL.Abstract;
    using EpamTask.MyBlog.Entities;

    public class RolesDao : IRolesDao
    {
        private static string connectionString;

        public RolesDao()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyBlogDBConnection"].ConnectionString;
        }        

        public bool AddRoleToAccount(System.Guid accountID, System.Guid roleID)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("INSERT INTO dbo.[UserRoles] ([UserID], [RoleID]) VALUES (@UserID, @RoleID)", con);
                command.Parameters.Add(new SqlParameter("@UserID", accountID));
                command.Parameters.Add(new SqlParameter("@RoleID", roleID));

                con.Open();
                var reader = command.ExecuteNonQuery();

                return reader > 0 ? true : false;
            }
        }
       
        public Role GetRole(System.Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT TOP 1 [ID], [RoleName] FROM dbo.[Roles] WHERE dbo.[Roles].[ID] = @id", con);
                command.Parameters.Add(new SqlParameter("@id", id));

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Role()
                    {
                        ID = (System.Guid)reader["ID"],
                        RoleName = (string)reader["RoleName"],   
                    };
                }
                else
                {
                    return null;
                }
            }
        }
       
        public System.Collections.Generic.IEnumerable<Role> GetAccountRoles(Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                ///!!!!
                var command = new SqlCommand(
                    "SELECT dbo.[Roles].[ID], dbo.[Roles].[RoleName] " +
                    "FROM dbo.[Roles] INNER JOIN dbo.[UserRoles] " +
                    "ON dbo.[Roles].[ID] = dbo.[UserRoles].[RoleID] " +
                    "WHERE dbo.[UserRoles].[UserID] = @UserID", con);
                command.Parameters.Add(new SqlParameter("@UserID", id));
                
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {                    
                    yield return new Role()
                    {
                        ID = (System.Guid)reader["ID"],
                        RoleName = (string)reader["RoleName"],
                    };
                }
            }  
        }        

        public System.Collections.Generic.IEnumerable<Role> GetNoAccountRoles(Guid id)
        {
            var allRoles = this.GetAllRoles().ToList();
            var userRoles = this.GetAccountRoles(id).ToList();

            foreach (var role in allRoles)
            {
                if (!userRoles.Contains(role))
                {
                    yield return role;
                }
            }  
        }

        public System.Collections.Generic.IEnumerable<Role> GetAllRoles()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT [ID], [RoleName] FROM dbo.[Roles]", con);
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Role()
                    {
                        ID = (System.Guid)reader["ID"],
                        RoleName = (string)reader["RoleName"],
                    };
                }
            }  
        }       

        public bool DeleteRoleFromAccount(System.Guid accountID, System.Guid roleID)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("DELETE FROM dbo.[UserRoles] WHERE [UserID]=@UserID AND [RoleID]=@RoleID", con);
                command.Parameters.Add(new SqlParameter("@UserID", accountID));
                command.Parameters.Add(new SqlParameter("@RoleID", roleID));

                con.Open();
                var reader = command.ExecuteNonQuery();

                return reader > 0 ? true : false;
            }
        }
    }
}
