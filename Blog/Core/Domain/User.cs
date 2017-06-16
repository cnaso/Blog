using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Domain
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public IList<Role> Roles { get; set; }
    }
}