using AutoMapper;
using Examination_System.Models;
using Examination_System.ViewModels.Instructor;

namespace Examination_System.DTOs.Instructors
{
    public class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            // Domain <-> DTO
            CreateMap<Instructor, GetAllInstructorsDTOs>().ReverseMap();
            CreateMap<Instructor, CreateInstructorDTO>().ReverseMap();
            CreateMap<Instructor, UpdateInstructorDto>().ReverseMap();

            // DTO <-> ViewModel
            CreateMap<GetAllInstructorsDTOs, GetInstructorViewModel>().ReverseMap();
            CreateMap<CreateInstructorViewModel, CreateInstructorDTO>().ReverseMap();
            CreateMap<UpdateInstructorViewModel, UpdateInstructorDto>().ReverseMap();
        }
    }
}