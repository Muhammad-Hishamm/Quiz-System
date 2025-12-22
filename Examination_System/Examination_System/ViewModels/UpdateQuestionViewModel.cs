using Examination_System.Data.Enums;

namespace Examination_System.ViewModels
{
    public class UpdateQuestionViewModel
    {
        public QuestionLevel Level { get; set; }
        public string QuestionBody { get; set; }
        public int InstructorId { get; set; }
    }
}