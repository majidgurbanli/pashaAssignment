using PashaVacancyProject.Domain.Entities.Base;

namespace PashaVacancyProject.Domain.Entities
{
    public class VacancyQuestion : BaseEntity
    {
        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }  
    }
}