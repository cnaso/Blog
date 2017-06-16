using Blog.Infrastructure;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Selected("users")]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}