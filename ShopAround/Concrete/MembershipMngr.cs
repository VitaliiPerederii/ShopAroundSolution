using ShopAround.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ShopAround.Concrete
{
    public class MembershipMngr: IMembershipMngr
    {
        public bool ValidateUser(string email, string password)
        {
            return Membership.ValidateUser(email, password);
        }
        public MembershipUser CreateUser(string email, string password, string name)
        {
            return ((CustomMembershipProvider)Membership.Provider).CreateUser(email, password, name);
        }
        public MembershipUser GetUser()
        {
            return Membership.GetUser();
        }
    }
}