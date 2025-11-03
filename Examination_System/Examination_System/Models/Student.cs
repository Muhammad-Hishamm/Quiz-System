namespace Examination_System.Models
{
    public class Student : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }

    }
}
