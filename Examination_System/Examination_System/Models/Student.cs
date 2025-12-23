namespace Examination_System.Models
{
    public class Student : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Result> Results { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
        public ICollection<InstructorStudent> InstructorStudents { get; set; }

    }
}
