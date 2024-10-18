using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Domain.EntitiesMapping.Base;

namespace PashaVacancyProject.Domain.EntitiesMapping
{
    public class ApplicationAnswerMap : BaseEntityTypeConfiguration<ApplicationAnswer>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<ApplicationAnswer> builder)
        {
            builder.ToTable("answer").HasKey(x => x.ID);
            builder.Property(x => x.ID)
                            .HasColumnName("Id");

            builder.Property(x => x.ApplicantID)
                             .HasColumnName("applicant_id");
            builder.Property(x => x.VacancyID)
                            .HasColumnName("vacancy_id");
            builder.Property(x => x.QuestionID)
                            .HasColumnName("question_id");
            builder.Property(x => x.ChoiceID)
                          .HasColumnName("choice_id");

            builder.Property(x => x.StartTime)
                          .HasColumnName("start_time");
            builder.Property(x => x.AnswerTime)
                         .HasColumnName("answer_time");
            builder.Property(x => x.IsActive)
                       .HasColumnName("is_active");
            builder.HasOne(x => x.Question)
              .WithMany(x => x.ApplicationAnswers).HasForeignKey(z => z.QuestionID);
            builder.HasOne(x => x.Choice)
             .WithMany(x => x.ApplicationAnswers).HasForeignKey(z => z.ChoiceID);

            builder.HasOne(x => x.Application)
            .WithMany(x => x.ApplicationAnswer).HasForeignKey(z => new { z.VacancyID, z.ApplicantID});
        }
    }
}
