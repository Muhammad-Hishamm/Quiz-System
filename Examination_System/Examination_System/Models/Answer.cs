namespace Examination_System.Models
{
    public class Answer: BaseModel
    {
        public string AnswerBody { get; set; }
        public int StudentExamId { get; set; }
        public StudentExam? StudentExam { get; set; }
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
