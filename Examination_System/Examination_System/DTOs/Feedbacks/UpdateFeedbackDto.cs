using Examination_System.ViewModels;

namespace Examination_System.DTOs.Feedbacks
{
    public class UpdateFeedbackDto
    {
        public int Rating { get; set; }
        public string Comments { get; set; }
        public int ResultId { get; set; }

        public UpdateFeedbackDto ToDTO(UpdateFeedbackViewModel vm)
        {
            if (vm == null) return null!;
            return new UpdateFeedbackDto
            {
                Rating = vm.Rating,
                Comments = vm.Comments,
                ResultId = vm.ResultId
            };
        }
    }
}