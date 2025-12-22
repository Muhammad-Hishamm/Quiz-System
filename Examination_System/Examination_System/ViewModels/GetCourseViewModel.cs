using Examination_System.DTOs.Courses;
using Examination_System.Models;

namespace Examination_System.ViewModels
{
    public class GetCoursesViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }

        public GetCoursesViewModel ToViewModel(GetAllCoursesDTOs course)
        {
            return  new GetCoursesViewModel
            {
                Name = course.Name,
                Description = course.Description,
                CreditHours = course.CreditHours
            };
        }
    }
}
