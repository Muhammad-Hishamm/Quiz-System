using Examination_System.Data.Enums;
using Examination_System.ViewModels;

namespace Examination_System.DTOs.Questions
{
    public class UpdateQuestionDto
    {
        public QuestionLevel Level { get; set; }
        public string QuestionBody { get; set; }
        public int InstructorId { get; set; }

        public UpdateQuestionDto ToDTO(UpdateQuestionViewModel vm)
        {
            if (vm == null) return null;
            return new UpdateQuestionDto
            {
                Level = vm.Level,
                QuestionBody = vm.QuestionBody,
                InstructorId = vm.InstructorId
            };
        }
    }
}