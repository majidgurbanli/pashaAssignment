using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Domain.EntitiesMapping.Base;

namespace PashaVacancyProject.Domain.EntitiesMapping
{
    public class AdminUserMap : BaseEntityTypeConfiguration<AdminUser>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<AdminUser> builder)
        {
            builder.ToTable("admin_user").HasKey(x => x.ID);
            builder.Property(x => x.ID)
                            .HasColumnName("Id");

            builder.Property(x => x.Username)
                             .HasColumnName("username");
            builder.Property(x => x.Password)
                            .HasColumnName("password");
        }
    }
}
