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

        //public bool AddUser(string login, string password, DateTime birth, string e_mail)
        //{
        //    var user = new BlogUser(login, password, birth, e_mail);
        //    if (this._users_dao.AddUser(user))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

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

        public bool UpdateUser(BlogUser user)
        {
            if (this._users_dao.UpdateUser(user))
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

        public BlogUser GetUserByLogin(string login)
        {
            return this._users_dao.GetUserByLogin(login);
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

        public bool SetUserAvatar(Guid userID, byte[] img)
        {
            if (this._users_dao.GetUser(userID) == null)
            {
                throw new ArgumentException("Пользователя с данным ID не существует");
            }

            if (this._users_dao.SetUserAvatar(userID, img))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public byte[] GetUserAvatar(Guid userID)
        {
            if (this._users_dao.GetUser(userID) == null)
            {
                throw new ArgumentException("Пользователя с данным ID не существует");
            }

            try
            {
                return this._users_dao.GetUserAvatar(userID);
            }
            catch
            {
                return null;
            }         
        }

        public bool RemoveUserAvatar(Guid userID)
        {
            if (this._users_dao.GetUser(userID) == null)
            {
                throw new ArgumentException("Пользователя с данным ID не существует");
            }

            if (this._users_dao.RemoveUserAvatar(userID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddPost(BlogPost post)
        {
            if (this._posts_dao.AddPost(post))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdatePost(BlogPost post)
        {
            if (this._posts_dao.UpdatePost(post))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeletePost(Guid postID)
        {
            if (this._posts_dao.DeletePost(postID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public BlogPost GetPost(Guid id)
        {
            return this._posts_dao.GetPost(id);
        }

        public IEnumerable<BlogPost> GetAllPosts()
        {
            return this._posts_dao.GetAllPosts().ToList();
        }

        public IEnumerable<BlogPost> GetUserPosts(Guid userID)
        {
            return this._posts_dao.GetUserPosts(userID).ToList();
        }

        public bool AddRoleToAccount(System.Guid accountID, System.Guid roleID)
        {
            if (this._roles_dao.AddRoleToAccount(accountID, roleID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public System.Collections.Generic.IEnumerable<Role> GetAccountRoles(System.Guid accountID)
        {
            return this._roles_dao.GetAccountRoles(accountID).ToList();
        }

        public System.Collections.Generic.IEnumerable<Role> GetAllRoles()
        {
            return this._roles_dao.GetAllRoles().ToList();
        }

        public Role GetRole(System.Guid id)
        {
            return this._roles_dao.GetRole(id);
        }

        public bool DeleteRoleFromAccount(System.Guid accountID, System.Guid roleID)
        {
            if (this._roles_dao.DeleteRoleFromAccount(accountID, roleID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public System.Collections.Generic.IEnumerable<Role> GetNoAccountRoles(System.Guid accountID)
        {
            return this._roles_dao.GetNoAccountRoles(accountID).ToList();
        }

        public bool AddComment(PostComment comment)
        {
            if (this._comments_dao.AddComment(comment))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateComment(PostComment comment)
        {
            if (this._comments_dao.UpdateComment(comment))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteComment(Guid commentID)
        {
            if (this._comments_dao.DeleteComment(commentID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public PostComment GetComment(Guid commentID)
        {
            return this._comments_dao.GetComment(commentID);
        }

        public IEnumerable<PostComment> GetPostComments(Guid postID)
        {
            return this._comments_dao.GetPostComments(postID).ToList();
        }
    }
}
