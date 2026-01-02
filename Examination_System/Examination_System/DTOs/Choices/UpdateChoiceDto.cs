using Examination_System.ViewModels.Choice;

namespace Examination_System.DTOs.Choices
{
    public class UpdateChoiceDto
    {
        public string ChoiceBody { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public UpdateChoiceDto ToDTO(UpdateChoiceViewModel vm)
        {
            if (vm == null) return null;
            return new UpdateChoiceDto
            {
                ChoiceBody = vm.ChoiceBody,
                IsCorrect = vm.IsCorrect,
                QuestionId = vm.QuestionId
            };
        }
    }
}