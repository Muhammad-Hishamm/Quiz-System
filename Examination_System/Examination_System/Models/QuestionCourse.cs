namespace Examination_System.Models
{
    public class QuestionCourse: BaseModel
    {
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }

    }
}
