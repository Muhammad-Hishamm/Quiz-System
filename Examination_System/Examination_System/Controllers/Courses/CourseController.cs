using AutoMapper;
using Examination_System.DTOs.Courses;
using Examination_System.Models;
using Examination_System.Services;
using Examination_System.ViewModels;
using Examination_System.ViewModels.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Courses
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;
        IMapper _mapper;
        public CourseController(IMapper mapper)
        {
            _mapper = mapper;
            _courseService = new CourseService(_mapper);
        }

        // GET: api/Course
        [HttpGet]
        public ResponseViewModel<IEnumerable<GetCoursesViewModel>> Get()
        {
            var query = _courseService.GetAll(); 
            var res = _mapper.Map<IEnumerable<GetCoursesViewModel>>(query);
            return new ResponseViewModel<IEnumerable<GetCoursesViewModel>>() { Data = res, IsSuccess = true, ErrorCode = ErrorCode.NoError, Message = "" };
        }

        // GET: api/Course/1
        [HttpGet("{id}")]
        public ResponseViewModel<GetCoursesViewModel> GetById(int id)
        {
            var entityDTO =  _courseService.GetById(id);
            var res =  _mapper.Map<GetCoursesViewModel>(entityDTO);
            return new ResponseViewModel<GetCoursesViewModel>() { Data= res, IsSuccess=true,ErrorCode = ErrorCode.NoError,Message=""};
        }

        [HttpPost]
        public async Task<ResponseViewModel<bool>> Create(CreateCourseViewModel course)
        {   
            var CourseDto = _mapper.Map<CreateDTO>(course);
            var ok = await _courseService.Create(CourseDto).ConfigureAwait(false);
            return new ResponseViewModel<bool>() { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.BadRequest, Message = ok ? "" : "Failed to create course." };
        }

        [HttpPut("{id}")]
        public async Task<ResponseViewModel<bool>> Update(int id, UpdateCourseViewModel updatedCourse)
        {
            var updatedCourseDto = _mapper.Map<UpdateCourseDto>(updatedCourse);
            var ok = await _courseService.Update(id, updatedCourseDto).ConfigureAwait(false);
            return new ResponseViewModel<bool>() { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.CourseNotFound, Message = ok ? "" : "Course not found." };
        }

        [HttpDelete("{id}")]
        public async Task<ResponseViewModel<bool>>  Delete(int id)
        {
            var ok = await _courseService.Delete(id).ConfigureAwait(false);
            return new ResponseViewModel<bool>() { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.CourseNotFound, Message = ok ? "" : "Course not found." };
        }
    }
}