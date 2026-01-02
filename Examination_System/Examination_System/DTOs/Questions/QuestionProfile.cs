using AutoMapper;
using Examination_System.Models;
using Examination_System.ViewModels.Question;

namespace Examination_System.DTOs.Questions
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            // ViewModel <-> DTO
            CreateMap<GetQuestionViewModel, GetAllQuestionsDTOs>().ReverseMap();
            CreateMap<CreateQuestionViewModel, CreateQuestionDTO>().ReverseMap();
            CreateMap<UpdateQuestionViewModel, UpdateQuestionDto>().ReverseMap();

            // Model <-> DTO
            CreateMap<Question, GetAllQuestionsDTOs>().ReverseMap();
            CreateMap<CreateQuestionDTO, Question>().ReverseMap();
            CreateMap<UpdateQuestionDto, Question>().ReverseMap();
        }
    }
}