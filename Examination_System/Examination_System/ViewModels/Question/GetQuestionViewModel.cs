using Examination_System.DTOs.Questions;
using Examination_System.Data.Enums;

namespace Examination_System.ViewModels.Question
{
    public class GetQuestionViewModel
    {
        public int Id { get; set; }
        public QuestionLevel Level { get; set; }
        public string QuestionBody { get; set; }
        public int InstructorId { get; set; }

        public GetQuestionViewModel ToViewModel(GetAllQuestionsDTOs dto)
        {
            if (dto == null) return null;
            return new GetQuestionViewModel
            {
                Id = dto.Id,
                Level = dto.Level,
                QuestionBody = dto.QuestionBody,
                InstructorId = dto.InstructorId
            };
        }
    }
}