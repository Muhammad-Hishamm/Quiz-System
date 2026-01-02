using Examination_System.DTOs.Instructors;
using Examination_System.Models;
using Examination_System.ViewModels;

namespace Examination_System.DTOs.Courses
{
    public class GetAllCoursesDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public int CreditHours { get; set; }
        public GetAllInstructorsDTOs Instructor { get; set; }
    }
}
