using PashaVacancyProject.Domain.Entities.Base;

namespace PashaVacancyProject.Domain.Entities
{
    public class Choice : BaseEntity
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public string? ChoiceText { get; set; }
        public bool IsCorrect { get; set; }
        public Question Question { get; set; }
        public ICollection<ApplicationAnswer> ApplicationAnswers { get; set; }


    }
}
