using Examination_System.DTOs.Results;

namespace Examination_System.ViewModels
{
    public class GetResultViewModel
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public int? StudentId { get; set; }
        public int? ExamId { get; set; }

        public GetResultViewModel ToViewModel(GetAllResultsDTOs dto)
        {
            if (dto == null) return null;
            return new GetResultViewModel
            {
                Id = dto.Id,
                Score = dto.Score,
                StudentId = dto.StudentId,
                ExamId = dto.ExamId
            };
        }
    }
}