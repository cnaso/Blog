using Blog.Core.Domain;
using Blog.Persistence.EntityConfigurations;
using System.Data.Entity;
using System.Linq;

namespace Blog.Persistence
{
    public class BlogContext : DbContext
    {
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        public BlogContext() : base("name=BlogContext")
        {
            Configuration.LazyLoadingEnabled = false;

            Users.Include(r => Roles).ToList();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PostConfiguration());
            modelBuilder.Configurations.Add(new TagConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
        }
    }
}