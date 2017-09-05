using Blog.Persistence;
using Blog.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Blog.Controllers
{
    public class AuthController : Controller
    {
        private BlogContext _context;

        public AuthController(BlogContext context)
        {
            _context = context;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            var user = _context.Users.Single(u => u.Username == form.Username);

            if (user == null || !user.CheckPassword(form.Password))
            {
                Core.Domain.User.FakeHash();
                ModelState.AddModelError("Username", "Username or password is not correct.");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            FormsAuthentication.SetAuthCookie(user.Username, true);

            if (!string.IsNullOrWhiteSpace(returnUrl) & Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToRoute("Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToRoute("Home");
        }
    }
}