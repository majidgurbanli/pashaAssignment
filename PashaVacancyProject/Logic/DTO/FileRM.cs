using PashaVacancyProject.Logic.Validation;

namespace PashaVacancyProject.Logic.DTO
{
    public class FileRM
    {


        [FileValidation(5 * 1024 * 1024, new string[] { ".pdf", ".docx" })]
        public IFormFile? File { get; set; }
        public int ApplicantID { get; set; }
        public int VacancyID { get; set; }
    }
}
