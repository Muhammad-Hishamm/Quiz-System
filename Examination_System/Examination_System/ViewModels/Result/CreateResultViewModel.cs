namespace Examination_System.ViewModels.Result
{
    public class CreateResultViewModel
    {
        public double Score { get; set; }

        // optional association info if you plan to create StudentExam together
        public int? StudentId { get; set; }
        public int? ExamId { get; set; }
    }
}