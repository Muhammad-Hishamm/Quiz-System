using Examination_System.Data;
using Examination_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Examination_System.Controllers.Courses
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        Context context = new Context();
        // GET: api/Course
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            var courses = context.Courses.ToList();
            if (courses == null || courses.Count == 0)
            {
                return null;
            }
            return courses;
        }

        // GET: api/Course/1

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = context.Courses.Find(id);
            if(course == null)
            {
                return NotFound($"Course with id {id} is not found.");
            }
            return Ok($"GetById method in Course Controller is working for ID: {id}");
        }

        [HttpPost]
        public IActionResult Create(Models.Course course) 
        {
            if (course == null)
            {
                return BadRequest("Course data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Courses.Add(course);
            context.SaveChanges();
            return Ok("Create method in Course Controller is working!");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id , Models.Course updatedCourse)
        {
            var course = context.Courses.Find(id);
            if (course ==  null)
            {
                return NotFound($"the course with id {id} is not exist ya batooot");
            }
            if (updatedCourse == null)
            {
                return BadRequest($"the updated course is null");
            }
            course.CreditHours = updatedCourse.CreditHours;
            course.Description = updatedCourse.Description;
            course.Name = updatedCourse.Name;
            context.Courses.Update(course);
            context.SaveChanges();
            return Ok($"the course with id {id} is updated successully");

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = context.Courses.Find(id);
            if(course == null)
            {
                return NotFound($"the course with id {id} is not found");
            }
            return Ok($"the couruse with id {id} is deleted successully");
        }
    }
}
