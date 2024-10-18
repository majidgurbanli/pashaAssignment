using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Domain.EntitiesMapping.Base;

namespace PashaVacancyProject.Domain.EntitiesMapping
{
    public class VacancyMap : BaseEntityTypeConfiguration<Vacancy>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<Vacancy> builder)
        {
            builder.ToTable("vacancy").HasKey(x => new { x.Id });

            builder.Property(x => x.Title)
                             .HasColumnName("title");

            builder.Property(x => x.Description)
                             .HasColumnName("description");
            builder.Property(x => x.QuestionCount)
                            .HasColumnName("question_count");

            builder.HasMany(x => x.Questions)
              .WithMany(x => x.Vacancies).UsingEntity<VacancyQuestionMap>();
            builder.HasMany(x => x.Applications)
           .WithMany(x => x.Vacancies).UsingEntity<Application>();


        }
    }
}
