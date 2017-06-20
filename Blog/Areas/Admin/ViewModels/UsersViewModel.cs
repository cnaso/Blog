using Blog.Core.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Areas.Admin.ViewModels
{
    public class UserList
    {
        public IEnumerable<User> Users { get; set; }
    }

    public class NewUser
    {
        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(255), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class EditUser
    {
        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, MaxLength(255)]
        public string Email { get; set; }
    }

    public class ResetUserPassword
    {
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}