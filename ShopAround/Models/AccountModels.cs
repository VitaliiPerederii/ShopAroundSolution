using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ShopAround.Models
{
   public class LogOnModel
   {
       public string Email { get; set; }

       [DataType(DataType.Password)]
       public string Password { get; set; }

       public bool RememberMe { get; set; }
   }

    public class RegisterModel
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [System.Web.Mvc.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        public string Name { get; set; }
    }
}