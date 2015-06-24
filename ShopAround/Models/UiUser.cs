using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAround.Models
{
    public class UiUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public UiRole Role { get; set; }
    }
}