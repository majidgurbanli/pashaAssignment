using PashaVacancyProject.Domain.Entities.Base;

namespace PashaVacancyProject.Domain.Entities
{
    public class Vacancy : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? QuestionCount { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Applicant> Applications { get; set; }
    }
}
