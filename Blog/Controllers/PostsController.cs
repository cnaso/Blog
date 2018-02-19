using Blog.Core.Domain;
using Blog.Persistence;
using PagedList;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using Blog.Infrastructure;
using System.Text.RegularExpressions;
using System;
using Blog.ViewModels;

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

            PostsIndexViewModel postModel = new PostsIndexViewModel(posts, page, pagesize);

            return View(postModel);
        }

        [AjaxActionOnly]
        public PartialViewResult TagPosts(string idAndSlug, int page = 1)
        {
            var tagSlugParts = SeparateIdAndSlug(idAndSlug);

            if (tagSlugParts == null)
            {
                //return HttpNotFound(); show error
            }

            Tag tag = _context.Tags.Where(t => t.Id == tagSlugParts.Item1).Include("Posts.User").Include("Posts.Tags").FirstOrDefault();

            //if (tag == null)
            //{
            //    return HttpNotFound();
            //}
            //TODO return all posts?

            IList<Post> posts = tag.Posts;

            PostsIndexViewModel postModel = new PostsIndexViewModel(posts, page, 5);
            postModel.Tag = new SidebarTagViewModel
            {
                Id = tag.Id,
                Name = tag.Name,
                Slug = tag.Slug,
                PostCount = tag.Posts.Count
            };

            return PartialView("_Posts", postModel);
        }

        [AjaxActionOnly]
        public PartialViewResult Post(string idAndSlug, int page = 1)
        {
            var postSlugParts = SeparateIdAndSlug(idAndSlug);

            if (postSlugParts == null)
            {
                //return HttpNotFound(); show error
            }

            Post post = _context.Posts.Include(p => p.User).FirstOrDefault(p => p.Id == postSlugParts.Item1);

            //if (post == null)
            //{
            //    return HttpNotFound();
            //}
            //TODO return all posts?

            PostsPostViewModel postModel = new PostsPostViewModel()
            {
                PostId = post.Id,
                User = post.User.Username,
                Title = post.Title,
                Content = post.Content,
                Tags = getTagViewModelList(post.Tags),
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            };

            return PartialView("_Post", postModel);
        }

        private Tuple<int, string> SeparateIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");

            if (!matches.Success)
            {
                return null;
            }

            var id = int.Parse(matches.Result("$1"));
            var slug = matches.Result("$2");

            return Tuple.Create(id, slug);
        }

        private IList<SidebarTagViewModel> getTagViewModelList(IList<Tag> tags)
        {
            IList<SidebarTagViewModel> tagViewModels = new List<SidebarTagViewModel>();

            foreach (var tag in tags)
            {
                tagViewModels.Add(new SidebarTagViewModel
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Slug = tag.Slug,
                    PostCount = tag.Posts.Count
                });
            }

            return tagViewModels;
        }
    }
}