using Examination_System.DTOs.Exams;
using Examination_System.Data.Enums;

namespace Examination_System.ViewModels.Exam
{
    public class GetExamViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ExamType Type { get; set; }
        public int NumberOfQuestions { get; set; }
        public int CourseId { get; set; }

        public GetExamViewModel ToViewModel(GetAllExamsDTOs dto)
        {
            if (dto == null) return null;
            return new GetExamViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Type = dto.Type,
                NumberOfQuestions = dto.NumberOfQuestions,
                CourseId = dto.CourseId
            };
        }
    }
}