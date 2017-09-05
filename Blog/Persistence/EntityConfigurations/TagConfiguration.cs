using Blog.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Blog.Persistence.EntityConfigurations
{
    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
            Property(t => t.Slug)
                .HasMaxLength(128)
                .HasColumnType("varchar")
                .IsRequired();

            Property(t => t.Name)
                .HasMaxLength(128)
                .HasColumnType("varchar")
                .IsRequired();

            HasMany(t => t.Posts)
                .WithMany(p => p.Tags)
                .Map(m =>
                {
                    m.ToTable("TagPosts");
                    m.MapLeftKey("TagId");
                    m.MapRightKey("PostId");
                });
        }
    }
}