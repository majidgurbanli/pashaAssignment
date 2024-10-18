using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Domain.EntitiesMapping.Base;

namespace PashaVacancyProject.Domain.EntitiesMapping
{
    public class ChoiceMap : BaseEntityTypeConfiguration<Choice>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<Choice> builder)
        {
            builder.ToTable("choices").HasKey(x => new { x.ID });
            builder.Property(x => x.ID)
                            .HasColumnName("Id");

            builder.Property(x => x.ChoiceText)
                             .HasColumnName("choice_text");
            builder.Property(x => x.QuestionID)
                            .HasColumnName("question_id");
            builder.Property(x => x.IsCorrect)
                           .HasColumnName("is_correct");
            builder.HasOne(x => x.Question)
              .WithMany(x => x.Choices).HasForeignKey(z => z.QuestionID);
        }
    }
}
