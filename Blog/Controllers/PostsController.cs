using Blog.Core.Domain;
using Blog.Persistence;
using PagedList;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using Blog.Infrastructure;
using System.Text.RegularExpressions;
using static Blog.ViewModels.BlogPostViewModel;

namespace Blog.Controllers
{
    public class PostsController : Controller
    {
        private BlogContext _context;

        public PostsController(BlogContext context)
        {
            _context = context;
        }

        [Selected("home")]
        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            IList<Post> posts = _context.Posts.Include(u => u.User).Include(t => t.Tags).ToList();
            PagedList<Post> pagedList = new PagedList<Post>(posts, page, pagesize);

            PostsViewModel postModel = new PostsViewModel(posts, page, pagesize);

            return View(postModel);
        }

        [AjaxActionOnly]
        public PartialViewResult TagPosts(string idAndSlug, int page = 1)
        {
            var tagIdAndSlugFormat = new Regex(@"^(\d+)\-(.*)?$");

            if (!tagIdAndSlugFormat.IsMatch(idAndSlug))
            {
                //return HttpNotFound(); return all posts?
            }

            var tagParts = idAndSlug.Split('-');
            int tagId = int.Parse(tagParts[0]);

            Tag tag = _context.Tags.Include(t => t.Posts.Select(p => p.User)).FirstOrDefault(t => t.Id == tagId);

            //if (tag == null)
            //{
            //    return HttpNotFound();
            //}
            //TODO return all posts?

            IList<Post> posts = tag.Posts;

            PostsViewModel postModel = new PostsViewModel(posts, page, 5);
            postModel.Tag = tag;

            return PartialView("_Posts", postModel);
        }
    }
}