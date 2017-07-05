using Blog.Core.Domain;
using Blog.Persistence;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class PostsController : Controller
    {
        private BlogContext _context;

        public PostsController(BlogContext context)
        {
            _context = context;
        }

        public ActionResult Index(int page = 1, int pagesize = 4)
        {
            List<Post> posts = _context.Posts.ToList();
            PagedList<Post> pagedList = new PagedList<Post>(posts, page, pagesize);

            return View(pagedList);
        }
    }
}