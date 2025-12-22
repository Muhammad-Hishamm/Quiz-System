namespace Examination_System.DTOs.Choices
{
    public class GetAllChoicesDTOs
    {
        public int Id { get; set; }
        public string ChoiceBody { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}