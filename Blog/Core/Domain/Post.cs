using System;
using System.Collections.Generic;

namespace Blog.Core.Domain
{
    public class Post
    {
        public int Id { get; set; }

        public User User { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public IList<Tag> Tags { get; set; }

        public bool IsDeleted { get { return DeletedAt != null; } }
    }
}