using System.ComponentModel.DataAnnotations;

namespace Examination_System.Models
{
    public class Course : BaseModel
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }

        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }


        public ICollection<Exam> Exams { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
