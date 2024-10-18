namespace PashaVacancyProject.Logic.DTO
{
    public class QuestionDTO
    {
        public int ID { get; set; }
        public string QuestionText { get; set; }
        public List<ChoiceDTO> Choices { get; set; }
        public bool isLast { get; set; } = false;
    }
}
