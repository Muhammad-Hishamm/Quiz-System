using AutoMapper;
using Examination_System.DTOs.Instructors;
using Examination_System.Services;
using Examination_System.ViewModels;
using Examination_System.ViewModels.Instructor;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Instructors
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly InstructorService _instructorService;
        private readonly IMapper _mapper;

        public InstructorController(InstructorService instructorService, IMapper mapper)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        // GET: api/Instructor
        [HttpGet]
        public ResponseViewModel<IEnumerable<GetInstructorViewModel>> Get()
        {
            var dtos = _instructorService.GetAll();
            var vm = _mapper.Map<IEnumerable<GetInstructorViewModel>>(dtos);
            return new ResponseViewModel<IEnumerable<GetInstructorViewModel>> { Data = vm, IsSuccess = true, ErrorCode = Models.ErrorCode.NoError, Message = string.Empty };
        }

        // GET: api/Instructor/1
        [HttpGet("{id}")]
        public ResponseViewModel<GetInstructorViewModel> GetById(int id)
        {
            var dto = _instructorService.GetById(id);
            if (dto == null) return new ResponseViewModel<GetInstructorViewModel> { Data = null, IsSuccess = false, ErrorCode = Models.ErrorCode.InstructorNotFound, Message = "Instructor not found." };

            var vm = _mapper.Map<GetInstructorViewModel>(dto);
            return new ResponseViewModel<GetInstructorViewModel> { Data = vm, IsSuccess = true, ErrorCode = Models.ErrorCode.NoError, Message = string.Empty };
        }

        [HttpPost]
        public async Task<ResponseViewModel<bool>> Create(CreateInstructorViewModel instructor)
        {
            var dto = _mapper.Map<CreateInstructorDTO>(instructor);
            var ok = await _instructorService.Create(dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? Models.ErrorCode.NoError : Models.ErrorCode.InvalidCouseID, Message = ok ? string.Empty : "Failed to create instructor." };
        }

        [HttpPut("{id}")]
        public async Task<ResponseViewModel<bool>> Update(int id, UpdateInstructorViewModel updatedInstructor)
        {
            var dto = _mapper.Map<UpdateInstructorDto>(updatedInstructor);
            var ok = await _instructorService.Update(id, dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? Models.ErrorCode.NoError : Models.ErrorCode.InstructorNotFound, Message = ok ? string.Empty : "Instructor not found." };
        }

        [HttpDelete("{id}")]
        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var ok = await _instructorService.Delete(id).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? Models.ErrorCode.NoError : Models.ErrorCode.InstructorNotFound, Message = ok ? string.Empty : "Instructor not found." };
        }
    }
}