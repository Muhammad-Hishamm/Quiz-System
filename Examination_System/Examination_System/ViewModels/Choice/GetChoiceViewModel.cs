using Examination_System.DTOs.Choices;

namespace Examination_System.ViewModels.Choice
{
    public class GetChoiceViewModel
    {
        public int Id { get; set; }
        public string ChoiceBody { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public GetChoiceViewModel ToViewModel(GetAllChoicesDTOs dto)
        {
            if (dto == null) return null;
            return new GetChoiceViewModel
            {
                Id = dto.Id,
                ChoiceBody = dto.ChoiceBody,
                IsCorrect = dto.IsCorrect,
                QuestionId = dto.QuestionId
            };
        }
    }
}