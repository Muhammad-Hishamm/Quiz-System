using AutoMapper;
using Examination_System.Models;
using Examination_System.ViewModels.Student;

namespace Examination_System.DTOs.Students
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            // Domain <-> DTO
            CreateMap<Student, GetAllStudentsDTOs>().ReverseMap();
            CreateMap<Student, CreateStudentDTO>().ReverseMap();
            CreateMap<Student, UpdateStudentDto>().ReverseMap();

            // DTO <-> ViewModel
            CreateMap<GetAllStudentsDTOs, GetStudentViewModel>().ReverseMap();
            CreateMap<CreateStudentViewModel, CreateStudentDTO>().ReverseMap();
            CreateMap<UpdateStudentViewModel, UpdateStudentDto>().ReverseMap();
        }
    }
}