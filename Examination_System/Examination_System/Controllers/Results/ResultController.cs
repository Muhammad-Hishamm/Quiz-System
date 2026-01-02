using AutoMapper;
using Examination_System.DTOs.Results;
using Examination_System.Models;
using Examination_System.Services;
using Examination_System.ViewModels;
using Examination_System.ViewModels.Result;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Results
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly ResultService _resultService;
        private readonly IMapper _mapper;

        public ResultController(ResultService resultService, IMapper mapper)
        {
            _resultService = resultService;
            _mapper = mapper;
        }

        // GET: api/Result
        [HttpGet]
        public ResponseViewModel<IEnumerable<GetResultViewModel>> Get()
        {
            var dtos = _resultService.GetAll();
            var vm = _mapper.Map<IEnumerable<GetResultViewModel>>(dtos);
            return new ResponseViewModel<IEnumerable<GetResultViewModel>> { Data = vm, IsSuccess = true, ErrorCode = ErrorCode.NoError, Message = string.Empty };
        }

        // GET: api/Result/1
        [HttpGet("{id}")]
        public ResponseViewModel<GetResultViewModel> GetById(int id)
        {
            var dto = _resultService.GetById(id);
            if (dto == null) return new ResponseViewModel<GetResultViewModel> { Data = null, IsSuccess = false, ErrorCode = ErrorCode.CourseNotFound, Message = "Result not found." };

            var res = _mapper.Map<GetResultViewModel>(dto);
            return new ResponseViewModel<GetResultViewModel>() { Data = res, IsSuccess = true, ErrorCode = ErrorCode.NoError, Message = string.Empty };
        }

        [HttpPost]
        public async Task<ResponseViewModel<bool>> Create(CreateResultViewModel model)
        {
            var dto = _mapper.Map<CreateResultDTO>(model);
            var ok = await _resultService.Create(dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.BadRequest, Message = ok ? string.Empty : "Failed to create result." };
        }

        [HttpPut("{id}")]
        public async Task<ResponseViewModel<bool>> Update(int id, UpdateResultViewModel updated)
        {
            var dto = _mapper.Map<UpdateResultDto>(updated);
            var ok = await _resultService.Update(id, dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.CourseNotFound, Message = ok ? string.Empty : "Result not found." };
        }

        [HttpDelete("{id}")]
        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var ok = await _resultService.Delete(id).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.CourseNotFound, Message = ok ? string.Empty : "Result not found." };
        }
    }
}