using AutoMapper;
using Examination_System.DTOs.Instructors;
using Examination_System.Models;
using Examination_System.ViewModels.Course;
using Examination_System.ViewModels.Instructor;
namespace Examination_System.DTOs.Courses
{
    public class CourseProfile:Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, GetAllCoursesDTOs>().ReverseMap();

            CreateMap<GetCoursesViewModel, GetAllCoursesDTOs>().ReverseMap();
            CreateMap<CreateCourseViewModel, CreateDTO>().ReverseMap();
            CreateMap<UpdateCourseViewModel, UpdateCourseDto>().ReverseMap();
            CreateMap<Course, CreateDTO>().ReverseMap();
            CreateMap<Course, UpdateCourseDto>().ReverseMap();
            CreateMap<Course,Course>().ReverseMap();
        }
    }
}

