using Examination_System.ViewModels;

namespace Examination_System.DTOs.Courses
{
    public class CreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }

        public CreateDTO ToDTO(CreateCourseViewModel course)
        {
            return new CreateDTO
            {
                Name = course.Name,
                Description = course.Description,
                CreditHours = course.CreditHours
            };
        }
    }

}
