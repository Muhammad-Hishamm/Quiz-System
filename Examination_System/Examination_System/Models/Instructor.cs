namespace Examination_System.Models
{
    public class Instructor : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }

        public ICollection<Exam> Exams { get; set; }
        public ICollection<Choice> Choices { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<InstructorStudent> InstructorStudents { get; set; }
    }
}
