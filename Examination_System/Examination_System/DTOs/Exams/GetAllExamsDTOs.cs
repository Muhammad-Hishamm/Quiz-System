using Examination_System.Data.Enums;

namespace Examination_System.DTOs.Exams
{
    public class GetAllExamsDTOs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ExamType Type { get; set; }
        public int NumberOfQuestions { get; set; }
        public int CourseId { get; set; }
    }
}