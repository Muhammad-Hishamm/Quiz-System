namespace Examination_System.Models
{
    public class Choice:BaseModel
    {
        public string ChoiceBody { get; set; }
        public bool IsCorrect { get; set; }

        // Foreign key to Instructors
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
