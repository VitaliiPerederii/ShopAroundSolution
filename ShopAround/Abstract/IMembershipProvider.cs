using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace ShopAround.Abstract
{
    public interface IMembershipMngr
    {
        bool ValidateUser(string Email, string Password);
        MembershipUser CreateUser(string Email, string Password, string Name);
        MembershipUser GetUser();
    }
}
