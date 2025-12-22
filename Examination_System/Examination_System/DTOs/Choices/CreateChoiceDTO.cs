using Examination_System.ViewModels;

namespace Examination_System.DTOs.Choices
{
    public class CreateChoiceDTO
    {
        public string ChoiceBody { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public CreateChoiceDTO ToDTO(CreateChoiceViewModel vm)
        {
            if (vm == null) return null;
            return new CreateChoiceDTO
            {
                ChoiceBody = vm.ChoiceBody,
                IsCorrect = vm.IsCorrect,
                QuestionId = vm.QuestionId
            };
        }
    }
}