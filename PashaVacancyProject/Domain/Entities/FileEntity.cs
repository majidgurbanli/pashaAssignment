using PashaVacancyProject.Domain.Entities.Base;

namespace PashaVacancyProject.Domain.Entities
{
    public class FileEntity : BaseEntity
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public Application Application { get; set; }
        public int VacancyID { get; set; }
        public int ApplicantID { get; set; }



    }
}
