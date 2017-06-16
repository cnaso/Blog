using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Core.Domain
{
    public class Tag
    {
        public int Id { get; set; }

        public string Slug { get; set; }

        public string Name { get; set; }

        public IList<Post> Posts { get; set; }
    }
}