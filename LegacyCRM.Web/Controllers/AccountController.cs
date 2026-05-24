using System;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using LegacyCRM.Core;

namespace LegacyCRM.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly CrmDbContext _context = new CrmDbContext();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            try
            {
                Membership.GetUser(username);
            }
            catch (ProviderException)
            {
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.IsActive);
            if (user != null && user.PasswordHash == password)
            {
                user.LastLoginAt = DateTime.UtcNow;
                _context.SaveChanges();

                FormsAuthentication.SetAuthCookie(username, false);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
