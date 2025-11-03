namespace Examination_System.Models
{
    public class Result : BaseModel
    {
        public double Score { get; set; }
        public StudentExam StudentExam { get; set; }
    }
}
