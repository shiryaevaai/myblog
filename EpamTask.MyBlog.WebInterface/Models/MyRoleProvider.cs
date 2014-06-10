namespace EpamTask.MyBlog.WebInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;

    using EpamTask.MyBlog.Entities;
    using EpamTask.MyBlog.Logic;

    public class MyRoleProvider : RoleProvider
    {
        public override string[] GetAllRoles()
        {
            var result = new string[] { };
            int index = 0;
            var roleList = BusinessLogicHelper._logic.GetAllRoles();
            foreach (var role in roleList)
            {
                result[index] = role.RoleName;
                index++;
            }

            return result;
        }

        public override string[] GetRolesForUser(string username)
        {
            var result = new List<string>();
            var account = BusinessLogicHelper._logic.GetUserByLogin(username);
            var roleList = BusinessLogicHelper._logic.GetAccountRoles(account.ID);
            foreach (var role in roleList)
            {
                result.Add(role.RoleName);
            }

            return result.ToArray();
        }

        public static IEnumerable<Role> GetRolesForUser(Guid ID)
        {
            var result = new List<string>();
            var account = BusinessLogicHelper._logic.GetUserByID(ID);
            return BusinessLogicHelper._logic.GetAccountRoles(account.ID).ToList();
        }

        public static IEnumerable<Role> GetNoRolesForUser(Guid ID)
        {
            var result = new List<string>();
            var account = BusinessLogicHelper._logic.GetUserByID(ID);
            return BusinessLogicHelper._logic.GetNoAccountRoles(account.ID).ToList();
        }

        public static bool DeleteRoleFromAccount(Guid AccountID, Guid RoleID)
        {
            return BusinessLogicHelper._logic.DeleteRoleFromAccount(AccountID, RoleID);
        }

        public static Role GetRole(Guid id)
        {
            return BusinessLogicHelper._logic.GetRole(id);
        }

        public static bool AddRoleToAccount(Guid AccountID, Guid RoleID)
        {
            return BusinessLogicHelper._logic.AddRoleToAccount(AccountID, RoleID);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}