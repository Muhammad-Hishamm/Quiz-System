using AutoMapper;
using Examination_System.DTOs.Courses;
using Examination_System.Models;
using Examination_System.Services;
using Examination_System.ViewModels;
using Examination_System.ViewModels.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Courses
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;
        private readonly IMapper _mapper;

        public CourseController(CourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        // GET: api/Course?name=math&departmentId=1
        [HttpGet]
        [Authorize]
        public async Task<ResponseViewModel<IEnumerable<GetCoursesViewModel>>> GetAll([FromQuery] string? name = null)
        {
            Expression<Func<Course, bool>>? filter = null;

            var dtos = await _courseService.GetAll(filter).ConfigureAwait(false);
            var vm = _mapper.Map<IEnumerable<GetCoursesViewModel>>(dtos);
            return new ResponseViewModel<IEnumerable<GetCoursesViewModel>> { Data = vm, IsSuccess = true, ErrorCode = ErrorCode.NoError, Message = string.Empty };
        }

        // GET: api/Course/1
        [HttpGet("{id}")]
        public async Task<ResponseViewModel<GetCoursesViewModel>> GetById(int id)
        {
            var entityDTO = await _courseService.GetById(id);
            if(entityDTO == null)
            {
                return new ResponseViewModel<GetCoursesViewModel>() { Data = null, IsSuccess = false, ErrorCode = ErrorCode.CourseNotFound, Message = "Course not found." };
            }
            var res =  _mapper.Map<GetCoursesViewModel>(entityDTO);
            return new ResponseViewModel<GetCoursesViewModel>() { Data= res, IsSuccess=true,ErrorCode = ErrorCode.NoError,Message=""};
        }

        [HttpPost]
        public async Task<ResponseViewModel<bool>> Create(CreateCourseViewModel course)
        {   
            if (course == null)
            {
                return new ResponseViewModel<bool>() { Data = false, IsSuccess = false, ErrorCode = ErrorCode.BadRequest, Message = "Invalid course data." };
            }
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