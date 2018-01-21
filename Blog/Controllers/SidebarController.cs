using Blog.Core.Domain;
using Blog.Persistence;
using Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class SidebarController : Controller
    {
        private BlogContext _context;

        public SidebarController(BlogContext context)
        {
            _context = context;
        }

        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            IList<Tag> tags = _context.Tags.Include(t => t.Posts).ToList();
            Sidebar sidebar = new Sidebar();

            //TODO: Implement categories

            foreach (var tag in tags)
            {
                TagViewModel viewTag = new TagViewModel {
                    Id = tag.Id,
                    Name = tag.Name,
                    Slug = tag.Slug,
                    PostCount = tag.Posts.Count
                };

                sidebar.Tags.Add(viewTag);
            } 

            return PartialView("_Sidebar", sidebar);
        }
    }
}