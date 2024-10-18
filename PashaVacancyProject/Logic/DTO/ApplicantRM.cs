using PashaVacancyProject.Logic.Validation;
using System.ComponentModel.DataAnnotations;

namespace PashaVacancyProject.Logic.DTO
{
    public class ApplicantRM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [AzerbaijaniPhoneAttribute]
        public string Phone { get; set; }
        public int VacancyID { get; set; }


    }
}
