using Examination_System.Data.Enums;
using Examination_System.Models;


public class Exam : BaseModel
{
    public string Title { get; set; }
    public ExamType Type { get; set; }

    public int InstructorId { get; set; }
    public Instructor? Instructor { get; set; }
    public int NumberOfQuestions { get; set; }
    public int CourseId { get; set; }      
    public Course? Course { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
    public ICollection<Result> Results { get; set; }
    public ICollection<ExamQuestion> ExamQuestions { get; set; }
    public ICollection<StudentExam> StudentExams { get; set; }
}
