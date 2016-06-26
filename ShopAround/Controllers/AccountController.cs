using System.Web.Mvc;
using ShopAround.Models;
using ShopAround.Abstract;
using ShopAround.Concrete;
using System.Web.Security;
using Ninject;

namespace ShopAround.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        IProductStorage db;
        IMembershipMngr membMngr;

        [Inject]
        public IPlatformProvider PlatformProvider { get; set; }

        public AccountController(IProductStorage storage, IMembershipMngr membMngr)
        {
            db = storage;
            this.membMngr = membMngr;
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (model != null && ModelState.IsValid)
            {
                if (membMngr.ValidateUser(model.Email, model.Password))
                {
                    membMngr.SetAuthCookie(model.Email, model.RememberMe);
                    if (PlatformProvider.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipUser membershipUser = membMngr.CreateUser(model.Email, model.Password, model.Name);

                if (membershipUser != null)
                {
                    membMngr.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка при регистрации");
                }
            }
            return View(model);
        }

        public ActionResult ViewProfile()
        {
            UiUser uiUser = null;
            MembershipUser user = membMngr.GetUser();
            if (user != null)
            {
                uiUser = db.FindUser(user.UserName);
                ViewBag.RoleName = uiUser.Role.Name;
            }

            return View(uiUser);
        }
    }
}