using AutoMapper;
using Examination_System.Models;
using Examination_System.ViewModels.Exam;

namespace Examination_System.DTOs.Exams
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            // ViewModel <-> DTO
            CreateMap<GetExamViewModel, GetAllExamsDTOs>().ReverseMap();
            CreateMap<CreateExamViewModel, CreateExamDTO>().ReverseMap();
            CreateMap<UpdateExamViewModel, UpdateExamDto>().ReverseMap();

            // Model <-> DTO
            CreateMap<Exam, GetAllExamsDTOs>().ReverseMap();
            CreateMap<CreateExamDTO, Exam>().ReverseMap();
            CreateMap<UpdateExamDto, Exam>().ReverseMap();
        }
    }
}