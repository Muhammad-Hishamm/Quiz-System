namespace Examination_System.DTOs.Results
{
    public class GetAllResultsDTOs
    {
        public int Id { get; set; }
        public double Score { get; set; }

        // Optional FK info exposed by DTO (nullable because navigation may be missing)
        public int? StudentId { get; set; }
        public int? ExamId { get; set; }
    }
}