using AutoMapper;
using Examination_System.DTOs.Questions;
using Examination_System.Models;
using Examination_System.Services;
using Examination_System.ViewModels;
using Examination_System.ViewModels.Question;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Examination_System.Controllers.Questions
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly IMapper _mapper;

        public QuestionController(QuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        // GET: api/Question
        [HttpGet]
        public async Task<ResponseViewModel<IEnumerable<GetQuestionViewModel>>> GetAll()
        {
            var dtos = await _questionService.GetAll().ConfigureAwait(false);
            var vm = _mapper.Map<IEnumerable<GetQuestionViewModel>>(dtos);
            return new ResponseViewModel<IEnumerable<GetQuestionViewModel>> { Data = vm, IsSuccess = true, ErrorCode = ErrorCode.NoError, Message = string.Empty };
        }

        // GET: api/Question/1
        [HttpGet("{id}")]
        public async Task<ResponseViewModel<GetQuestionViewModel>> GetById(int id)
        {
            var dto = await _questionService.GetById(id).ConfigureAwait(false);
            if (dto == null) return new ResponseViewModel<GetQuestionViewModel> { Data = null, IsSuccess = false, ErrorCode = ErrorCode.CourseNotFound, Message = "Question not found." };

            var res = _mapper.Map<GetQuestionViewModel>(dto);
            return new ResponseViewModel<GetQuestionViewModel>() { Data = res, IsSuccess = true, ErrorCode = ErrorCode.NoError, Message = string.Empty };
        }

        [HttpPost]
        public async Task<ResponseViewModel<bool>> Create(CreateQuestionViewModel question)
        {
            var dto = _mapper.Map<CreateQuestionDTO>(question);
            var ok = await _questionService.Create(dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.BadRequest, Message = ok ? string.Empty : "Failed to create question." };
        }

        [HttpPut("{id}")]
        public async Task<ResponseViewModel<bool>> Update(int id, UpdateQuestionViewModel updatedQuestion)
        {
            var dto = _mapper.Map<UpdateQuestionDto>(updatedQuestion);
            var ok = await _questionService.Update(id, dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.CourseNotFound, Message = ok ? string.Empty : "Question not found." };
        }

        [HttpDelete("{id}")]
        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var ok = await _questionService.Delete(id).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.CourseNotFound, Message = ok ? string.Empty : "Question not found." };
        }
    }
}
