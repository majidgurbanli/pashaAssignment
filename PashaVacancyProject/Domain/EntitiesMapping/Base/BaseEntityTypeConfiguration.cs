using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PashaVacancyProject.Domain.Entities.Base;

namespace PashaVacancyProject.Domain.EntitiesMapping.Base
{
    public abstract class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {

            builder.Property(x => x.ReguID)
                             .HasColumnName("reguid");

            builder.Property(x => x.RegDate)
                             .HasColumnName("reg_date")
                             .HasDefaultValue();

            builder.Property(x => x.EdituID)
                             .HasColumnName("edituid");

            builder.Property(x => x.EditDate)
                             .HasColumnName("edit_date");

            builder.Property(x => x.ArchivedUserID)
                             .HasColumnName("archived_user_id");

            ConfigureOtherProperties(builder);

        }
        public abstract void ConfigureOtherProperties(EntityTypeBuilder<TEntity> builder);

    }
}
