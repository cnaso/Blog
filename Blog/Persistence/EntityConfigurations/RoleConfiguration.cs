using Blog.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Blog.Persistence.EntityConfigurations
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(r => r.Name)
                .HasMaxLength(128)
                .HasColumnType("varchar")
                .IsRequired();
        }
    }
}