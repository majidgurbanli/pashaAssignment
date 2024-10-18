using PashaVacancyProject.Domain.Entities.Base;

namespace PashaVacancyProject.Domain.Entities
{
    public class Question : BaseEntity
    {
        public int Id { get; set; }
        public string? QuestionText { get; set; }

        public ICollection<Vacancy>? Vacancies { get; set; }
        public ICollection<Choice>? Choices { get; set; }
        public ICollection<ApplicationAnswer> ApplicationAnswers { get; set; }
    }
}