using AutoMapper;
using Examination_System.DTOs.Questions;
using Examination_System.Models;
using Examination_System.Services;
using Examination_System.ViewModels;
using Examination_System.ViewModels.Question;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Questions
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly IMapper _mapper;

        public QuestionController(IMapper mapper)
        {
            _mapper = mapper;
            _questionService = new QuestionService(_mapper);
        }

        // GET: api/Question
        [HttpGet]
        public ResponseViewModel<IEnumerable<GetQuestionViewModel>> Get()
        {
            var dtos = _questionService.GetAll();
            var vm = _mapper.Map<IEnumerable<GetQuestionViewModel>>(dtos);
            return new ResponseViewModel<IEnumerable<GetQuestionViewModel>> { Data = vm, IsSuccess = true, ErrorCode = ErrorCode.NoError, Message = string.Empty };
        }

        // GET: api/Question/1
        [HttpGet("{id}")]
        public ResponseViewModel<GetQuestionViewModel> GetById(int id)
        {
            var dto = _questionService.GetById(id);
            if (dto == null) return new ResponseViewModel<GetQuestionViewModel> { Data = null, IsSuccess = false, ErrorCode = ErrorCode.CourseNotFound, Message = "Question not found." };

            var res = _mapper.Map<GetQuestionViewModel>(dto);
            return new ResponseViewModel<GetQuestionViewModel>() { Data = res, IsSuccess = true, ErrorCode = ErrorCode.NoError, Message = string.Empty };
        }

        [HttpPost]
        public async Task<ResponseViewModel<bool>> Create(CreateQuestionViewModel question)
        {
            var dto = _mapper.Map<CreateQuestionDTO>(question);
            var ok = await _question_service_create(dto).ConfigureAwait(false);
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

        // small private wrapper to avoid accidental naming collisions in mapping
        private Task<bool> _question_service_create(CreateQuestionDTO dto) => _questionService.Create(dto);
    }
}
