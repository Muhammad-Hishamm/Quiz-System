namespace Examination_System.Models
{
    public class Result
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public StudentExam StudentExam { get; set; }
    }
}
