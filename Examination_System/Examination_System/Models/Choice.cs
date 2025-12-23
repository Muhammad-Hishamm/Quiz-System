namespace Examination_System.Models
{
    public class Choice:BaseModel
    {
        public string ChoiceBody { get; set; }
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<QuestionChoice>? QuestionChoices { get; set; }
    }
}
