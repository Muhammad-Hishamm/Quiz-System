using AutoMapper;
using Examination_System.DTOs.Choices;
using Examination_System.Services;
using Examination_System.ViewModels;
using Examination_System.ViewModels.Choice;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Choices
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoiceController : ControllerBase
    {
        private readonly ChoiceService _choiceService;
        private readonly IMapper _mapper;

        public ChoiceController(ChoiceService choiceService, IMapper mapper)
        {
            _choiceService = choiceService;
            _mapper = mapper;
        }

        // GET: api/Choice
        [HttpGet]
        public ResponseViewModel<IEnumerable<GetChoiceViewModel>> Get()
        {
            var dtos = _choiceService.GetAll();
            var vm = _mapper.Map<IEnumerable<GetChoiceViewModel>>(dtos);
            return new ResponseViewModel<IEnumerable<GetChoiceViewModel>> { Data = vm, IsSuccess = true, ErrorCode = Models.ErrorCode.NoError, Message = string.Empty };
        }

        // GET: api/Choice/1
        [HttpGet("{id}")]
        public ResponseViewModel<GetChoiceViewModel> GetById(int id)
        {
            var dto = _choiceService.GetById(id);
            if (dto == null) return new ResponseViewModel<GetChoiceViewModel> { Data = null, IsSuccess = false, ErrorCode = Models.ErrorCode.InvalidCouseID, Message = "Choice not found." };

            var res = _mapper.Map<GetChoiceViewModel>(dto);
            return new ResponseViewModel<GetChoiceViewModel>() { Data = res, IsSuccess = true, ErrorCode = Models.ErrorCode.NoError, Message = "" };
        }

        [HttpPost]
        public async Task<ResponseViewModel<bool>> Create(CreateChoiceViewModel choice)
        {
            var dto = _mapper.Map<CreateChoiceDTO>(choice);
            var ok = await _choiceService.Create(dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? Models.ErrorCode.NoError : Models.ErrorCode.InvalidCouseID, Message = ok ? string.Empty : "Failed to create choice." };
        }

        [HttpPut("{id}")]
        public async Task<ResponseViewModel<bool>> Update(int id, UpdateChoiceViewModel updatedChoice)
        {
            var dto = _mapper.Map<UpdateChoiceDto>(updatedChoice);
            var ok = await _choiceService.Update(id, dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? Models.ErrorCode.NoError : Models.ErrorCode.InvalidCouseID, Message = ok ? string.Empty : "Choice not found." };
        }

        [HttpDelete("{id}")]
        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var ok = await _choiceService.Delete(id).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? Models.ErrorCode.NoError : Models.ErrorCode.InvalidCouseID, Message = ok ? string.Empty : "Choice not found." };
        }
    }
}