namespace Examination_System.Models
{
    public class Result : BaseModel
    {
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

        public double Score { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int StudentExamId { get; set; }
        public StudentExam? StudentExam { get; set; }
        public Feedback? Feedback { get; set; }
    }
}
