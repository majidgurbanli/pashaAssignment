using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Domain.EntitiesMapping.Base;

namespace PashaVacancyProject.Domain.EntitiesMapping
{
    public class VacancyQuestionMap : BaseEntityTypeConfiguration<VacancyQuestion>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<VacancyQuestion> builder)
        {
            builder.ToTable("vacancy_question").HasKey(x => new { x.VacancyId, x.QuestionId });

            builder.Property(x => x.VacancyId)
                            .HasColumnName("vacancy_id");
            builder.Property(x => x.QuestionId)
                           .HasColumnName("question_id");
        }
    }
}
