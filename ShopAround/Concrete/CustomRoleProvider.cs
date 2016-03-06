using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Helpers;
using System.Security.Cryptography;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;
using ShopAround.Models;
using ShopAround.Abstract;
using Ninject;

namespace ShopAround.Concrete
{
    public class CustomRoleProvider : RoleProvider
    {
        [Inject]
        public IProductStorage Db { get; set; }
        public CustomRoleProvider()
        {
        }
        public override string[] GetRolesForUser(string email)
        {
            string[] role = new string[] { };

            try
            {
                UiUser user = Db.FindUser(email);
                if (user != null)
                {
                    UiRole userRole = user.Role;
                    if (userRole != null)
                    {
                        role = new string[] { userRole.Name };
                    }
                }
            }
            catch
            {
                role = new string[] { };
            }
            return role;
        }
        public override void CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return;

            UiRole newRole = new UiRole() { Name = roleName };
            Db.AddRole(newRole);
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            try
            {
                UiUser user = Db.FindUser(username);
                if (user != null)
                {
                    UiRole userRole = user.Role;
                    if (userRole != null && userRole.Name == roleName)
                    {
                        outputResult = true;
                    }
                }
            }
            catch
            {
                outputResult = false;
            }
            return outputResult;
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

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
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