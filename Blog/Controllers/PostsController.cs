using Blog.Core.Domain;
using Blog.Persistence;
using PagedList;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;

namespace Blog.Controllers
{
    public class PostsController : Controller
    {
        private BlogContext _context;

        public PostsController(BlogContext context)
        {
            _context = context;
        }

        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            List<Post> posts = _context.Posts.Include(u => u.User).Include(t => t.Tags).ToList();
            PagedList<Post> pagedList = new PagedList<Post>(posts, page, pagesize);

            return View(pagedList);
        }
    }
}