namespace EpamTask.MyBlog.Logic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EpamTask.MyBlog.DAL.Abstract;
    using EpamTask.MyBlog.DAL.DB;
    using EpamTask.MyBlog.Entities;

    public class MyBlogLogic
    {
        private ICommentsDao _comments_dao;

        private IUsersDao _users_dao;

        private IPostsDao _posts_dao;

        private IRolesDao _roles_dao;

        // Add config parameter to check dal
        public MyBlogLogic()
        {
            this._comments_dao = new DAL.DB.CommentsDao();
            this._users_dao = new DAL.DB.UsersDao();
            this._posts_dao = new DAL.DB.PostsDao();
            this._roles_dao = new DAL.DB.RolesDao();
        }

        public bool AddUser(string login, string password, DateTime birth, string e_mail)
        {
            var user = new BlogUser(login, password, birth, e_mail);
            if (this._users_dao.AddUser(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddUser(BlogUser user)
        {           
            if (this._users_dao.AddUser(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public BlogUser GetUserByID(Guid id)
        {
            return this._users_dao.GetUser(id);
        }

        public IEnumerable<BlogUser> GetAllUsers()
        {
            return this._users_dao.GetAllUsers().ToList();
        }

        public bool DeleteUser(Guid userID)
        {
            if (this._users_dao.DeleteUser(userID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
