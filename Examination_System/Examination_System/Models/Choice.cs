namespace Examination_System.Models
{
    public class Choice
    {
        public int Id { get; set; }
        public string ChoiceBody { get; set; }
        public bool IsCorrect { get; set; }

        // Foreign key to Question
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
