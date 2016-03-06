using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopAround.Abstract;
using ShopAround.Models;
using System.Web.Helpers;

namespace ShopAround.Concrete.Tests
{
    [TestClass()]
    public class CustomRoleProviderTests
    {
        [TestMethod()]
        public void GetRolesForUser_UserNotExist_ReturnsEmptyArray()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => null);

            CustomRoleProvider provider = new CustomRoleProvider();
            provider.Db = mock.Object;
            string[] roles = provider.GetRolesForUser("test@test");

            Assert.AreEqual(roles.Length, 0);
        }

        [TestMethod()]
        public void GetRolesForUser_UserExists_ReturnsRolesArray()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => new UiUser()
            {
                Role = new UiRole()
                {
                    Name = "admin"
                }
            });

            CustomRoleProvider provider = new CustomRoleProvider();
            provider.Db = mock.Object;
            string[] roles = provider.GetRolesForUser("test@test");

            Assert.AreEqual(roles.Length, 1);
            Assert.AreEqual(roles[0], "admin");
        }

        [TestMethod()]
        public void CreateRole_EmptyRoleName_NotCallAddRole()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();

            CustomRoleProvider provider = new CustomRoleProvider();
            provider.Db = mock.Object;

            string roleName = null;
            provider.CreateRole(roleName);

            mock.Verify(foo => foo.AddRole(It.Is<UiRole>(u => u.Name == roleName)), Times.Never());
        }

        [TestMethod()]
        public void CreateRole_CorrectRoleName_CallAddRole()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();

            CustomRoleProvider provider = new CustomRoleProvider();
            provider.Db = mock.Object;

            string roleName = "admin";
            provider.CreateRole(roleName);

            mock.Verify(foo => foo.AddRole(It.Is<UiRole>(u => u.Name == roleName)), Times.Once());
        }

        [TestMethod()]
        public void IsUserInRole_UserNotExist_ReturnsFalse()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => null);

            CustomRoleProvider provider = new CustomRoleProvider();
            provider.Db = mock.Object;

            string roleName = "admin";
            bool isInRole = provider.IsUserInRole("test", roleName);

            Assert.IsFalse(isInRole);
        }

        [TestMethod()]
        public void IsUserInRole_UserExistsNotInRole_ReturnsFalse()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => new UiUser()
            {
                Role = new UiRole()
                {
                    Name = "admin"
                }
            });

            CustomRoleProvider provider = new CustomRoleProvider();
            provider.Db = mock.Object;

            string roleName = "admin1";
            bool isInRole = provider.IsUserInRole("test", roleName);

            Assert.IsFalse(isInRole);
        }

        [TestMethod()]
        public void IsUserInRole_UserExistsInRole_ReturnsTrue()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            mock.Setup(m => m.FindUser(It.IsAny<string>())).Returns<string>(s => new UiUser()
            {
                Role = new UiRole()
                {
                    Name = "admin"
                }
            });

            CustomRoleProvider provider = new CustomRoleProvider();
            provider.Db = mock.Object;

            string roleName = "admin";
            bool isInRole = provider.IsUserInRole("test", roleName);

            Assert.IsTrue(isInRole);
        }
    }
}