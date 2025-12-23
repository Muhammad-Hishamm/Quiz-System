namespace Examination_System.Models
{
    public class StudentExam: BaseModel
    {
        public double Score { get; set; }
        public bool IsSubmitted { get; set; }
        public DateTime? SubmissionTime { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public ICollection<Answer> Answers { get; set; }



        public int ExamId { get; set; }
        public Exam? Exam { get; set; }

    }
}
