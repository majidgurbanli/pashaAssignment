using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Domain.EntitiesMapping.Base;

namespace PashaVacancyProject.Domain.EntitiesMapping
{
    public class ApplicationMap : BaseEntityTypeConfiguration<Application>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<Application> builder)
        {
            builder.ToTable("application").HasKey(x => new { x.ApplicantID, x.VacancyID });

            builder.Property(x => x.ApplicantID)
                             .HasColumnName("applicant_id");
            builder.Property(x => x.VacancyID)
                            .HasColumnName("vacancy_id");
            
        }
    }
}
