namespace Examination_System.Models
{
    public class InstructorStudent: BaseModel
    {
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
