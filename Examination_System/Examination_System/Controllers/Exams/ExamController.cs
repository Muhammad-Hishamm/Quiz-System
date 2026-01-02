using AutoMapper;
using Examination_System.Data;
using Examination_System.DTOs.Exams;
using Examination_System.Models;
using Examination_System.Repositories;
using Examination_System.Services;
using Examination_System.ViewModels;
using Examination_System.ViewModels.Exam;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Exams
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ExamService _examService;
        private readonly IMapper _mapper;

        public ExamController(IMapper mapper)
        {
            _mapper = mapper;
            _examService = new ExamService(_mapper);
        }

        // GET: api/Exam
        [HttpGet]
        public ResponseViewModel<IEnumerable<GetExamViewModel>> Get()
        {
            var dtos = _examService.GetAll();
            var vm = _mapper.Map<IEnumerable<GetExamViewModel>>(dtos);
            return new ResponseViewModel<IEnumerable<GetExamViewModel>> { Data = vm, IsSuccess = true, ErrorCode = ErrorCode.NoError, Message = string.Empty };
        }

        // GET: api/Exam/1
        [HttpGet("{id}")]
        public ResponseViewModel<GetExamViewModel> GetById(int id)
        {
            var dto = _examService.GetById(id);
            if (dto == null) return new ResponseViewModel<GetExamViewModel> { Data = null, IsSuccess = false, ErrorCode = ErrorCode.CourseNotFound, Message = "Exam not found." };

            var res = _mapper.Map<GetExamViewModel>(dto);
            return new ResponseViewModel<GetExamViewModel>() { Data = res, IsSuccess = true, ErrorCode = ErrorCode.NoError, Message = string.Empty };
        }

        [HttpPost]
        public async Task<ResponseViewModel<bool>> Create(CreateExamViewModel exam)
        {
            var dto = _mapper.Map<CreateExamDTO>(exam);
            var ok = await _examService.Create(dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.ExamNotFound, Message = ok ? string.Empty : "Failed to create exam." };
        }

        [HttpPut("{id}")]
        public async Task<ResponseViewModel<bool>> Update(int id, UpdateExamViewModel updatedExam)
        {
            var dto = _mapper.Map<UpdateExamDto>(updatedExam);
            var ok = await _examService.Update(id, dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.CourseNotFound, Message = ok ? string.Empty : "Exam not found." };
        }

        [HttpDelete("{id}")]
        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var ok = await _examService.Delete(id).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.CourseNotFound, Message = ok ? string.Empty : "Exam not found." };
        }
    }
}
