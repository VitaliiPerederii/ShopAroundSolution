using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopAround.Abstract;
using ShopAround.Models;
using System.Web.Helpers;
using System.Web.Security;

namespace ShopAround.Concrete.Tests
{
    [TestClass()]
    public class CustomMembershipProviderTests
    {
        [TestMethod()]
        public void ValidateUser_ExistingUserCorrectPassword_ReturnsTrue()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            string password = "test";
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => new UiUser()
            {
                Name = s,
                Password = Crypto.HashPassword(password)
            });

            CustomMembershipProvider provider = new CustomMembershipProvider();
            provider.Db = mock.Object;
            Assert.IsTrue(provider.ValidateUser("test", password));
        }

        [TestMethod()]
        public void ValidateUser_ExistingUserIncorrectPassword_ReturnsFalse()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            string password = "test";
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => new UiUser()
            {
                Name = s,
                Password = Crypto.HashPassword(password)
            });

            CustomMembershipProvider provider = new CustomMembershipProvider();
            provider.Db = mock.Object;
            Assert.IsFalse(provider.ValidateUser("test", "test1"));
        }

        [TestMethod()]
        public void ValidateUser_NonExistingUser_ReturnsFalse()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => null);

            CustomMembershipProvider provider = new CustomMembershipProvider();
            provider.Db = mock.Object;
            Assert.IsFalse(provider.ValidateUser("test", "test"));
        }

        [TestMethod()]
        public void ValidateUser_EmptyUserEmptyPassword_ReturnsFalse()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => null);

            CustomMembershipProvider provider = new CustomMembershipProvider();
            provider.Db = mock.Object;
            Assert.IsFalse(provider.ValidateUser(null, null));
        }

        [TestMethod()]
        public void CreateUser_EmptyUserEmptyPasswordEmptyEmail_ReturnsNull()
        {
            CustomMembershipProvider provider = new CustomMembershipProvider();
            Assert.IsNull(provider.CreateUser(null, null, null));
        }

        [TestMethod()]
        public void CreateUser_EmptyUserEmptyPassword_ReturnsNull()
        {
            CustomMembershipProvider provider = new CustomMembershipProvider();
            Assert.IsNull(provider.CreateUser("test@test", null, null));
        }

        [TestMethod()]
        public void CreateUser_UserExists_ReturnsNull()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            string password = "test";
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => new UiUser()
            {
                Name = s,
                Password = Crypto.HashPassword(password)
            });

            CustomMembershipProvider provider = new CustomMembershipProvider();
            provider.Db = mock.Object;
            
            Assert.IsNull(provider.CreateUser("test@test", password, "test"));
        }

        [TestMethod()]
        public void CreateUser_UserNotExist_ReturnsNewUser()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            string userName = "test@test";
            string Email = "test@test";
            string password = "test";
            mock.SetupSequence(m => m.FindUser(It.IsAny<string>()))
                .Returns(null)
                .Returns(new UiUser()
                    {
                        Name = userName,
                        Password = Crypto.HashPassword(password),
                        Email = Email
                    });
            CustomMembershipProvider provider = new CustomMembershipProvider();
            provider.Db = mock.Object;
            MembershipUser user = provider.CreateUser(Email, password, userName);

            Assert.IsNotNull(user);
            Assert.AreEqual(user.UserName, Email);
        }

        [TestMethod()]
        public void CreateUser_UserNotExist_CallsAddUser()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            string userName = "test@test";
            string Email = "test@test";
            string password = "test";
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => null);
            
            CustomMembershipProvider provider = new CustomMembershipProvider();
            provider.Db = mock.Object;
            MembershipUser user = provider.CreateUser(Email, password, userName);

            mock.Verify(f => f.AddUser(It.Is<UiUser>(u => helpfunc(u))), Times.Once());
        }

        [TestMethod()]
        public void CreateUser_UserExists_NotCallsAddUser()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            string userName = "test@test";
            string Email = "test@test";
            string password = "test";
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => new UiUser()
            {
                Name = userName,
                Password = Crypto.HashPassword(password),
                Email = Email
            });

            CustomMembershipProvider provider = new CustomMembershipProvider();
            provider.Db = mock.Object;
            MembershipUser user = provider.CreateUser(Email, password, userName);

            mock.Verify(f => f.AddUser(It.Is<UiUser>(u => helpfunc(u))), Times.Never());
        }

        bool helpfunc(UiUser u)
        {
            return u.Email == "test@test" && u.Name == "test@test" && Crypto.VerifyHashedPassword(u.Password, "test");
        }

        [TestMethod()]
        public void GetUser_UserExists_ReturnsUser()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            string Email = "test@test";
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => new UiUser()
            {
                Email = Email
            });

            CustomMembershipProvider provider = new CustomMembershipProvider();
            provider.Db = mock.Object;

            MembershipUser user = provider.GetUser(Email, false);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.UserName, Email);
        }

        [TestMethod()]
        public void GetUser_UserNotExists_ReternsNull()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            string Email = "test@test";
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => null);

            CustomMembershipProvider provider = new CustomMembershipProvider();
            provider.Db = mock.Object;

            MembershipUser user = provider.GetUser(Email, false);
            Assert.IsNull(user);
        }
    }
}