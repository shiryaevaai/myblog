namespace EpamTask.MyBlog.DAL.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EpamTask.MyBlog.Entities;

    public interface IUsersDao
    {
        bool AddUser(BlogUser user);

        bool UpdateUser(BlogUser user);
        
        bool DeleteUser(Guid id);

        BlogUser GetUser(Guid id);

        BlogUser GetUserByLogin(string login);

        IEnumerable<BlogUser> GetAllUsers();

        bool SetUserAvatar(Guid id, byte[] img);

        byte[] GetUserAvatar(Guid id);

        bool RemoveUserAvatar(Guid id);

        bool UpdateUserAvatar(Guid userID);
    }
}
