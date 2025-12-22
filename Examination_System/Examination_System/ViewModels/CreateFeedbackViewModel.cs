using System.ComponentModel.DataAnnotations;

namespace Examination_System.ViewModels
{
    public class CreateFeedbackViewModel
    {
        [Required]
        public int Rating { get; set; }

        [Required]
        public string Comments { get; set; }

        [Required]
        public int ResultId { get; set; }
    }
}