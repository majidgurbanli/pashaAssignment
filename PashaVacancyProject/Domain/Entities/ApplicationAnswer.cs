using PashaVacancyProject.Domain.Entities.Base;

namespace PashaVacancyProject.Domain.Entities
{
    public class ApplicationAnswer : BaseEntity
    {
        public int ID { get; set; }
        public int ApplicantID { get; set; }
        public int QuestionID { get; set; }
        public int? ChoiceID { get; set; }
        public int VacancyID { get; set; }
         public bool IsActive { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? AnswerTime { get; set; }
        public Application? Application { get; set; }
        public Question?  Question { get; set; }
        public Choice? Choice { get; set; }


    }
}
