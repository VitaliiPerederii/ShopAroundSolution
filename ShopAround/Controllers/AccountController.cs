using System.Web.Mvc;
using ShopAround.Models;
using ShopAround.Abstract;
using ShopAround.Concrete;
using System.Web.Security;


namespace ShopAround.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        IProductStorage db;
        public AccountController(IProductStorage storage)
        {
            db = storage;
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
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
                MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(model.Email, model.Password, model.Name);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
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
            MembershipUser user = Membership.GetUser();
            if (user != null)
            {
                uiUser = db.FindUser(user.UserName);
                ViewBag.RoleName = uiUser.Role.Name;
            }

            return View(uiUser);
        }
    }
}