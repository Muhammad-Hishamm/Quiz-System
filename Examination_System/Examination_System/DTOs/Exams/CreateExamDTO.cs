using Examination_System.Data.Enums;
using Examination_System.ViewModels.Exam;

namespace Examination_System.DTOs.Exams
{
    public class CreateExamDTO
    {
        public string Title { get; set; }
        public ExamType Type { get; set; }
        public int NumberOfQuestions { get; set; }
        public int CourseId { get; set; }

        public CreateExamDTO ToDTO(CreateExamViewModel vm)
        {
            if (vm == null) return null;
            return new CreateExamDTO
            {
                Title = vm.Title,
                Type = vm.Type,
                NumberOfQuestions = vm.NumberOfQuestions,
                CourseId = vm.CourseId
            };
        }
    }
}