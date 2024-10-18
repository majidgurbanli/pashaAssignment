using PashaVacancyProject.Domain.Entities.Base;

namespace PashaVacancyProject.Domain.Entities
{
    public class Application :BaseEntity
    {
        public int ApplicantID { get; set; }
        public int VacancyID { get; set; }
        public Vacancy? Vacancy { get; set; }
        public Applicant? Applicant { get; set; }
        public FileEntity? FileEntity { get; set; }
        public ICollection<ApplicationAnswer>? ApplicationAnswer { get; set; }





    }
}
