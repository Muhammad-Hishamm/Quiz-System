using Examination_System.Data.Enums;

namespace Examination_System.ViewModels
{
    public class UpdateExamViewModel
    {
        public string Title { get; set; }
        public ExamType Type { get; set; }
        public int NumberOfQuestions { get; set; }
        public int CourseId { get; set; }
    }
}