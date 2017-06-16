using Blog.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Blog.Persistence.EntityConfigurations
{
    public class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            Property(p => p.Title)
                .HasMaxLength(128)
                .HasColumnType("varchar")
                .IsRequired();

            Property(p => p.Slug)
                .HasMaxLength(128)
                .HasColumnType("varchar")
                .IsRequired();

            Property(p => p.Content)
                .HasMaxLength(5000)
                .HasColumnType("text")
                .IsRequired();

            Property(p => p.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            Property(p => p.UpdatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            Property(p => p.DeletedAt)
                .HasColumnType("datetime")
                .IsRequired();

            HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .Map(m =>
                {
                    m.ToTable("TagPosts");
                    m.MapLeftKey("PostId");
                    m.MapRightKey("TagId");
                });
        }
    }
}