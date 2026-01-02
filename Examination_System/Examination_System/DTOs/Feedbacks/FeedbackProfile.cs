using AutoMapper;
using Examination_System.Models;
using Examination_System.ViewModels.Feedback;

namespace Examination_System.DTOs.Feedbacks
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            // ViewModel <-> DTO
            CreateMap<GetFeedbackViewModel, GetAllFeedbacksDTOs>().ReverseMap();
            CreateMap<CreateFeedbackViewModel, CreateFeedbackDTO>().ReverseMap();
            CreateMap<UpdateFeedbackViewModel, UpdateFeedbackDto>().ReverseMap();

            // Model <-> DTO
            CreateMap<Feedback, GetAllFeedbacksDTOs>().ReverseMap();
            CreateMap<CreateFeedbackDTO, Feedback>().ReverseMap();
            CreateMap<UpdateFeedbackDto, Feedback>().ReverseMap();
        }
    }
}