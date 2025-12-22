using Examination_System.DTOs.Courses;
using Examination_System.Models;
using Examination_System.Services;
using Examination_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Courses
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController()
        {
            _courseService = new CourseService();
        }

        // GET: api/Course
        [HttpGet]
        public ActionResult<IEnumerable<GetCoursesViewModel>> Get()
        {
            var viewModel = new GetCoursesViewModel();
            var dtos = _courseService.GetAll();
            var courses = dtos.Select(dto => viewModel.ToViewModel(dto));
            return Ok(courses);
        }

        // GET: api/Course/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCoursesViewModel>> GetById(int id)
        {
            var viewModel = new GetCoursesViewModel();
            var courseDto = await _courseService.GetByIdAsync(id).ConfigureAwait(false);
            if (courseDto == null) return NotFound();
            var courseViewModel = viewModel.ToViewModel(courseDto);
            return Ok(courseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel course)
        {
            if (course == null) return BadRequest("Course data is null.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createDto = new CreateDTO().ToDTO(course);
            var ok = await _courseService.Create(createDto).ConfigureAwait(false);
            if (!ok) return BadRequest();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCourseViewModel updatedCourse)
        {
            if (updatedCourse == null) return BadRequest("this course is null");
            if (!ModelState.IsValid) return BadRequest("this course doesn't follow the Course Constraints");

            var updatedCourseDto = new UpdateCourseDto().ToDTO(updatedCourse);
            var ok = await _courseService.Update(id, updatedCourseDto).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _courseService.Delete(id).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }
    }
}