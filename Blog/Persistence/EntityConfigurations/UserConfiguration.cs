using Blog.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Blog.Persistence.EntityConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Username)
                .HasMaxLength(128)
                .HasColumnType("varchar")
                .IsRequired();

            Property(u => u.Email)
                .HasMaxLength(255)
                .HasColumnType("varchar")
                .IsRequired();

            Property(u => u.PasswordHash)
                .HasMaxLength(128)
                .HasColumnType("varchar")
                .IsRequired();

            HasMany(u => u.Roles)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("UserRoles");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });
        }
    }
}