using Blog.Areas.Admin.ViewModels;
using Blog.Core.Domain;
using Blog.Infrastructure;
using Blog.Persistence;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Selected("posts")]
    public class PostsController : Controller
    {
        private BlogContext _context;

        public PostsController(BlogContext context)
        {
            _context = context;
        }

        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            List<Post> posts = _context.Posts.Include(p => p.User).ToList();
            PagedList<Post> pagedList = new PagedList<Post>(posts, page, pagesize);

            return View(pagedList);
        }

        public ActionResult New()
        {
            return View("Form", new PostsForm
            {
                IsNew = true,
            });
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var selectedTags = ReconsileTags(form.Tags).ToList();

            Post post;
            if (form.IsNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.UtcNow,
                    User = _context.Users.Single(u => u.Username == User.Identity.Name)
                };

                foreach (var tag in selectedTags)
                {
                    post.Tags.Add(tag);
                }
            }
            else
            {
                post = _context.Posts.Include(p => p.Tags).Single(p => p.Id == form.PostId);

                if (post == null)
                {
                    return HttpNotFound();
                }

                post.UpdatedAt = DateTime.UtcNow;

                foreach (var toAdd in selectedTags.Where(t => !post.Tags.Contains(t)))
                {
                    post.Tags.Add(toAdd);
                }

                foreach (var toRemove in post.Tags.Where(t => !selectedTags.Contains(t)).ToList())
                {
                    post.Tags.Remove(toRemove);
                }
            }

            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var post = _context.Posts.Include(p => p.Tags).Single(p => p.Id == id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View("Form", new PostsForm
            {
                IsNew = false,
                PostId = id,
                Content = post.Content,
                Slug = post.Slug,
                Title = post.Title,
                Tags = String.Join(", ", post.Tags.Select(t => t.Name))
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Trash(int id)
        {
            var post = _context.Posts.Single(u => u.Id == id);

            if (post == null)
            {
                return HttpNotFound();
            }

            post.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private void DeletePost(string postId, bool permanent)
        {
            
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = _context.Posts.Single(u => u.Id == id);

            if (post == null)
            {
                return HttpNotFound();
            }

            _context.Posts.Remove(post);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Restore(int id)
        {
            var post = _context.Posts.Single(u => u.Id == id);

            if (post == null)
            {
                return HttpNotFound();
            }

            post.DeletedAt = null;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private IEnumerable<Tag> ReconsileTags(string tags)
        {
            IList<string> tagList = tags.Split(',').ToList();

            foreach (var tag in tagList)
            {
                string tagName = tag.Trim();
                var existingTag = _context.Tags.Single(t => t.Name == tagName);

                if (existingTag != null)
                {
                    yield return existingTag;
                    continue;
                }

                var newTag = new Tag
                {
                    Name = tagName,
                    Slug = tagName.Replace(' ', '-').ToLower()
                };

                _context.SaveChanges();

                yield return newTag;
            }
        }
    }
}