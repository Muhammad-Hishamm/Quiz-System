using Examination_System.Data.Enums;

namespace Examination_System.Models
{
    public class Question
    {
        public int Id { get; set; }
        public QuestionLevel Level { get; set; }
        public string QuestionBody { get; set; }

        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public ICollection<Choice> Choices { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }

    }
}
