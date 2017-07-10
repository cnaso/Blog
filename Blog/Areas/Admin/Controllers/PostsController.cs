using Blog.Areas.Admin.ViewModels;
using Blog.Core.Domain;
using Blog.Infrastructure;
using Blog.Persistence;
using PagedList;
using System;
using System.Collections.Generic;
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
            List<Post> posts = _context.Posts.ToList();
            PagedList<Post> pagedList = new PagedList<Post>(posts, page, pagesize);

            return View(pagedList);
        }

        public ActionResult New()
        {
            return View("Form", new PostsForm
            {
                IsNew = true,
                Tags = _context.Tags.Select(tag => new TagCheckbox
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    IsChecked = false
                }).ToList()
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
                    User = _context.Users.FirstOrDefault(u => u.Username == User.Identity.Name)
                };

                foreach (var tag in selectedTags)
                {
                    post.Tags.Add(tag);
                }
            }
            else
            {
                post = _context.Posts.Find(form.PostId);

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
            var post = _context.Posts.Find(id);

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
                Tags = _context.Tags.Select(tag => new TagCheckbox
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    IsChecked = post.Tags.Contains(tag)
                }).ToList()
            });
        }

        private IEnumerable<Tag> ReconsileTags(IEnumerable<TagCheckbox> tags)
        {
            foreach (var tag in tags.Where(t => t.IsChecked))
            {
                if (tag.Id != null)
                {
                    yield return _context.Tags.Find(tag.Id);
                    continue;
                }

                var existingTag = _context.Tags.FirstOrDefault(t => t.Name == tag.Name);

                if (existingTag != null)
                {
                    yield return existingTag;
                    continue;
                }

                var newTag = new Tag
                {
                    Name = tag.Name,
                    Slug = tag.Name.Replace(' ', '-')
                };

                _context.SaveChanges();

                yield return newTag;
            }
        }
    }
}