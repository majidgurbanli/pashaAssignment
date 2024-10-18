using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Domain.EntitiesMapping.Base;

namespace PashaVacancyProject.Domain.EntitiesMapping
{
    public class QuestionMap : BaseEntityTypeConfiguration<Question>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("question").HasKey(x => new { x.Id });

            builder.Property(x => x.QuestionText)
                             .HasColumnName("question_text");



            
        }
    }
}
