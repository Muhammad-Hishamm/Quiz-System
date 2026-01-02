using Examination_System.ViewModels.Feedback;

namespace Examination_System.DTOs.Feedbacks
{
    public class CreateFeedbackDTO
    {
        public int Rating { get; set; }
        public string Comments { get; set; }
        public int ResultId { get; set; }

        // Mirrors your Course pattern: used as new CreateXDTO().ToDTO(viewModel)
        public CreateFeedbackDTO ToDTO(CreateFeedbackViewModel vm)
        {
            if (vm == null) return null!;
            return new CreateFeedbackDTO
            {
                Rating = vm.Rating,
                Comments = vm.Comments,
                ResultId = vm.ResultId
            };
        }
    }
}