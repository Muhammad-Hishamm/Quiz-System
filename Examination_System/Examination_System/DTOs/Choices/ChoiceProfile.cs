using AutoMapper;
using Examination_System.Models;
using Examination_System.ViewModels.Choice;

namespace Examination_System.DTOs.Choices
{
    public class ChoiceProfile:Profile
    {
        public ChoiceProfile()
        {

            CreateMap<GetChoiceViewModel, GetAllChoicesDTOs>().ReverseMap();
            CreateMap<CreateChoiceViewModel, CreateChoiceDTO>().ReverseMap();
            CreateMap<UpdateChoiceViewModel, UpdateChoiceDto>().ReverseMap();
            CreateMap<Choice, CreateChoiceDTO>().ReverseMap();
            CreateMap<Choice, UpdateChoiceDto>().ReverseMap();
            

            CreateMap<Choice, GetAllChoicesDTOs>().ReverseMap();
            CreateMap<CreateChoiceDTO, Choice>().ReverseMap();
            CreateMap<UpdateChoiceDto, Choice>().ReverseMap();
            CreateMap<UpdateChoiceDto, GetChoiceViewModel>().ReverseMap();

        }
    }
}
