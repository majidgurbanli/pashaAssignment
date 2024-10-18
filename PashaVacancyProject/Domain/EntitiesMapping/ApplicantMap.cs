using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Domain.EntitiesMapping.Base;

namespace PashaVacancyProject.Domain.EntitiesMapping
{
    public class ApplicantMap : BaseEntityTypeConfiguration<Applicant>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<Applicant> builder)
        {
            builder.ToTable("applicants").HasKey(x => new { x.Id });
            builder.Property(x => x.Id)
                       .HasColumnName("id");
            builder.Property(x => x.Name)
                             .HasColumnName("name");
            builder.Property(x => x.Surname)
                            .HasColumnName("surname");
            builder.Property(x => x.Email)
                           .HasColumnName("email");
            builder.Property(x => x.Phone)
                         .HasColumnName("phone_number");

          
        }
    }
}
