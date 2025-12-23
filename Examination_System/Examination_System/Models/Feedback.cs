namespace Examination_System.Models
{
    public class Feedback : BaseModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; }

        public int ResultId { get; set; }
        public Result? Result { get; set; }

        public int ExamId { get; set; }
        public Exam? Exam { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
