using Examination_System.Data.Enums;
using Examination_System.ViewModels;

namespace Examination_System.DTOs.Exams
{
    public class UpdateExamDto
    {
        public string Title { get; set; }
        public ExamType Type { get; set; }
        public int NumberOfQuestions { get; set; }
        public int CourseId { get; set; }

        public UpdateExamDto ToDTO(UpdateExamViewModel vm)
        {
            if (vm == null) return null;
            return new UpdateExamDto
            {
                Title = vm.Title,
                Type = vm.Type,
                NumberOfQuestions = vm.NumberOfQuestions,
                CourseId = vm.CourseId
            };
        }
    }
}