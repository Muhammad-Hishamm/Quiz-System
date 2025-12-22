using Examination_System.ViewModels;

namespace Examination_System.DTOs.Results
{
    public class UpdateResultDto
    {
        public double Score { get; set; }
        public int? StudentId { get; set; }
        public int? ExamId { get; set; }

        public UpdateResultDto ToDTO(UpdateResultViewModel vm)
        {
            if (vm == null) return null;
            return new UpdateResultDto
            {
                Score = vm.Score,
                StudentId = vm.StudentId,
                ExamId = vm.ExamId
            };
        }
    }
}