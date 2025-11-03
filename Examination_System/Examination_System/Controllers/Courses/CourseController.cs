using Examination_System.Data;
using Examination_System.Models;
using Examination_System.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Examination_System.Controllers.Courses
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        CourseRepository _courseRepository;
        public CourseController()
        {
            _courseRepository = new CourseRepository();
        }
        // GET: api/Course
        [HttpGet]
        public IQueryable<Course> Get()
        {
            var courses =  _courseRepository.GetAll();
            if (courses == null)
            {
                return null;
            }
            return courses;
        }

        // GET: api/Course/1
        [HttpGet("{id}")]
        public async Task<Course> GetById(int id)
        {
            var course = await  _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return null;
            }
            return course;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (course == null)
            {
                return BadRequest("Course data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _courseRepository.Add(course);
            return Ok("Create method in Course Controller is working!");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Course updatedCourse)
        {
            if (updatedCourse == null)
                return BadRequest("this course is null");
            if (!ModelState.IsValid)
            {
                return BadRequest("this course doesn't follow the Course Constraints");
            }
            await _courseRepository.Update(updatedCourse);
            return Ok($"the course with id {id} is updated successully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseRepository.Delete(id);
            return Ok($"the couruse with id {id} is deleted successully");
        }
    }
}