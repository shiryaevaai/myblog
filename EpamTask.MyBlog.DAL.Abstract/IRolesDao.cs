namespace EpamTask.MyBlog.DAL.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EpamTask.MyBlog.Entities;
    interface IRolesDao
    {
        bool AddRoleToAccount(System.Guid AccountID, System.Guid RoleID);

        System.Collections.Generic.IEnumerable<Role> GetAccountRoles(System.Guid AccountID);

        System.Collections.Generic.IEnumerable<Role> GetAllRoles();

        Role GetRole(System.Guid id);

        bool DeleteRoleFromAccount(System.Guid AccountID, System.Guid RoleID);

        System.Collections.Generic.IEnumerable<Role> GetNoAccountRoles(System.Guid AccountID);
    }
}
