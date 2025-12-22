using Examination_System.Data.Enums;

namespace Examination_System.DTOs.Questions
{
    public class GetAllQuestionsDTOs
    {
        public int Id { get; set; }
        public QuestionLevel Level { get; set; }
        public string QuestionBody { get; set; }
        public int InstructorId { get; set; }
    }
}