using Blog.Infrastructure;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Selected("posts")]
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}