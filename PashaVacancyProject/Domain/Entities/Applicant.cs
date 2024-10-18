using PashaVacancyProject.Domain.Entities.Base;

namespace PashaVacancyProject.Domain.Entities
{
    public class Applicant : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public ICollection<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
    }
}
