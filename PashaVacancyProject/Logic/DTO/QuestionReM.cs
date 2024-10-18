namespace PashaVacancyProject.Logic.DTO
{
    public class QuestionReM
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public List<ChoiceReM>? Choices { get; set; }
    }
}
