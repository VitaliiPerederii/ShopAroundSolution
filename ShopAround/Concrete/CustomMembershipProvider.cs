using System;
using System.Web.Mvc;
using System.Web.Security;
using ShopAround.Models;
using ShopAround.Abstract;
using System.Web.Helpers;

namespace ShopAround.Concrete
{
    public class CustomMembershipProvider : MembershipProvider
    {
        IProductStorage db = null;
        public CustomMembershipProvider()
        {
            db = DependencyResolver.Current.GetService<IProductStorage>();
        }

      
        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false;

            try
            {
                UiUser user = db.FindUser(username);
                if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                {
                    isValid = true;
                }
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }

        public MembershipUser CreateUser(string email, string password, string userName)
        {
            MembershipUser membershipUser = GetUser(email, false);

            if (membershipUser == null)
            {
                try
                {
                    UiUser user = new UiUser();
                    user.Email = email;
                    user.Password = Crypto.HashPassword(password);
                    user.Name = userName;
                    user.RoleId = 1;

                    db.AddUser(user);
                    membershipUser = GetUser(email, false);
                    return membershipUser;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            try
            {

                UiUser user = db.FindUser(email);
                if (user != null)
                {
                    MembershipUser memberUser = new MembershipUser("MyMembershipProvider", user.Email, null, null, null, null,
                        false, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                    return memberUser;
                }
            }
            catch
            {
                return null;
            }
            return null;
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

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
    }
}