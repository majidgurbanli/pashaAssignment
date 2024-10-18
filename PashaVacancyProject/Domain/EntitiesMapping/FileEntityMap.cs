using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Domain.EntitiesMapping.Base;

namespace PashaVacancyProject.Domain.EntitiesMapping
{
    public class FileEntityMap : BaseEntityTypeConfiguration<FileEntity>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<FileEntity> builder)
        {
            builder.ToTable("FileEntity").HasKey(x => x.ID);
            builder.Property(x => x.ID)
                            .HasColumnName("ID");

            builder.Property(x => x.ApplicantID)
                             .HasColumnName("applicant_id");
            builder.Property(x => x.VacancyID)
                            .HasColumnName("vacancy_id");
            builder.Property(x => x.FileName)
                            .HasColumnName("FileName");
           
            builder.HasOne(x => x.Application)
              .WithOne(x => x.FileEntity).HasForeignKey<FileEntity>(z => new { z.VacancyID, z.ApplicantID });
        }
    }
}
