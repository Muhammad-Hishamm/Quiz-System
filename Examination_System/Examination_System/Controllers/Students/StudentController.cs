using AutoMapper;
using Examination_System.DTOs.Students;
using Examination_System.Services;
using Examination_System.ViewModels;
using Examination_System.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Students
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController(StudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        // GET: api/Student
        [HttpGet]
        public ResponseViewModel<IEnumerable<GetStudentViewModel>> Get()
        {
            var dtos = _studentService.GetAll();
            var vm = _mapper.Map<IEnumerable<GetStudentViewModel>>(dtos);
            return new ResponseViewModel<IEnumerable<GetStudentViewModel>> { Data = vm, IsSuccess = true, ErrorCode = Models.ErrorCode.NoError, Message = string.Empty };
        }

        // GET: api/Student/1
        [HttpGet("{id}")]
        public ResponseViewModel<GetStudentViewModel> GetById(int id)
        {
            var dto = _studentService.GetById(id);
            if (dto == null)
            {
                return new ResponseViewModel<GetStudentViewModel> { Data = null, IsSuccess = false, ErrorCode = Models.ErrorCode.StudentNotFound, Message = "Student not found." };
            }

            var vm = _mapper.Map<GetStudentViewModel>(dto);
            return new ResponseViewModel<GetStudentViewModel> { Data = vm, IsSuccess = true, ErrorCode = Models.ErrorCode.NoError, Message = string.Empty };
        }

        [HttpPost]
        public async Task<ResponseViewModel<bool>> Create(CreateStudentViewModel student)
        {
            var dto = _mapper.Map<CreateStudentDTO>(student);
            var ok = await _studentService.Create(dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? Models.ErrorCode.NoError : Models.ErrorCode.InvalidCouseID, Message = ok ? string.Empty : "Failed to create student." };
        }

        [HttpPut("{id}")]
        public async Task<ResponseViewModel<bool>> Update(int id, UpdateStudentViewModel updatedStudent)
        {
            var dto = _mapper.Map<UpdateStudentDto>(updatedStudent);
            var ok = await _studentService.Update(id, dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? Models.ErrorCode.NoError : Models.ErrorCode.StudentNotFound, Message = ok ? string.Empty : "Student not found." };
        }

        [HttpDelete("{id}")]
        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var ok = await _studentService.Delete(id).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? Models.ErrorCode.NoError : Models.ErrorCode.StudentNotFound, Message = ok ? string.Empty : "Student not found." };
        }
    }
}
