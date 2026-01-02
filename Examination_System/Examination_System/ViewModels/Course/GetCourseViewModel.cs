using Examination_System.DTOs.Courses;
using Examination_System.DTOs.Instructors;
using Examination_System.Models;
using Examination_System.ViewModels.Instructor;

namespace Examination_System.ViewModels.Course
{
    public class GetCoursesViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }
        public GetInstructorViewModel Instructor { get; set; }

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
