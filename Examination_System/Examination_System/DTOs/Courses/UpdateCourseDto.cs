using Examination_System.ViewModels.Course;

namespace Examination_System.DTOs.Courses
{
    public class UpdateCourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }

        public UpdateCourseDto ToDTO(UpdateCourseViewModel course)
        {
            return new UpdateCourseDto
            {
                Name = course.Name,
                Description = course.Description,
                CreditHours = course.CreditHours
            };
        }
    }
}
