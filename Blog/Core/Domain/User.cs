using System.Collections.Generic;

namespace Blog.Core.Domain
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public IList<Role> Roles { get; set; }

        public void SetPassword(string password)
        {
            PasswordHash = password;
        }
    }
}