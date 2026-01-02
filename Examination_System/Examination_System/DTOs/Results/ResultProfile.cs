using AutoMapper;
using Examination_System.Models;
using Examination_System.ViewModels.Result;

namespace Examination_System.DTOs.Results
{
    public class ResultProfile : Profile
    {
        public ResultProfile()
        {
            // ViewModel <-> DTO
            CreateMap<GetResultViewModel, GetAllResultsDTOs>().ReverseMap();
            CreateMap<CreateResultViewModel, CreateResultDTO>().ReverseMap();
            CreateMap<UpdateResultViewModel, UpdateResultDto>().ReverseMap();

            // Model <-> DTO
            CreateMap<Result, GetAllResultsDTOs>().ReverseMap();
            CreateMap<CreateResultDTO, Result>().ReverseMap();
            CreateMap<UpdateResultDto, Result>().ReverseMap();
        }
    }
}