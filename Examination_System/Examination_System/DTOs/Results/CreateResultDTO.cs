using Examination_System.ViewModels.Result;

namespace Examination_System.DTOs.Results
{
    public class CreateResultDTO
    {
        public double Score { get; set; }

        // If you later want to create/associate StudentExam here, you can include these.
        public int? StudentId { get; set; }
        public int? ExamId { get; set; }

        public CreateResultDTO ToDTO(CreateResultViewModel vm)
        {
            if (vm == null) return null;
            return new CreateResultDTO
            {
                Score = vm.Score,
                StudentId = vm.StudentId,
                ExamId = vm.ExamId
            };
        }
    }
}