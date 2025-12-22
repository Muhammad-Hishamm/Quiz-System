using Examination_System.DTOs.Feedbacks;

namespace Examination_System.ViewModels
{
    public class GetFeedbackViewModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public int ResultId { get; set; }

        public GetFeedbackViewModel ToViewModel(GetAllFeedbacksDTOs dto)
        {
            if (dto == null) return null!;
            return new GetFeedbackViewModel
            {
                Id = dto.Id,
                Rating = dto.Rating,
                Comments = dto.Comments,
                ResultId = dto.ResultId
            };
        }
    }
}