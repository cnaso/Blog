using Blog.Core.Domain;
using PagedList;
using System;
using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class PostsIndexViewModel
    {
        public SidebarTagViewModel Tag { get; set; }
        public IPagedList<Post> Posts { get; }

        public PostsIndexViewModel(IList<Post> posts, int page, int pagesize)
        {
            Posts = new PagedList<Post>(posts, page, pagesize);
        }
    }

    public class PostsPostViewModel
    {
        public int? PostId { get; set; }

        public string User { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public IList<SidebarTagViewModel> Tags { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}