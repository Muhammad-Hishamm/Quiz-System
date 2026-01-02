using Examination_System.Data.Enums;
using Examination_System.ViewModels.Question;

namespace Examination_System.DTOs.Questions
{
    public class CreateQuestionDTO
    {
        public QuestionLevel Level { get; set; }
        public string QuestionBody { get; set; }
        public int InstructorId { get; set; }

        public CreateQuestionDTO ToDTO(CreateQuestionViewModel vm)
        {
            if (vm == null) return null;
            return new CreateQuestionDTO
            {
                Level = vm.Level,
                QuestionBody = vm.QuestionBody,
                InstructorId = vm.InstructorId
            };
        }
    }
}