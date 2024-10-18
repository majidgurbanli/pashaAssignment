using PashaVacancyProject.Domain.Entities.Base;

namespace PashaVacancyProject.Domain.Entities
{
    public class AdminUser : BaseEntity
    {
        public int ID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
