namespace Examination_System.Models
{
    public class Instructor : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
