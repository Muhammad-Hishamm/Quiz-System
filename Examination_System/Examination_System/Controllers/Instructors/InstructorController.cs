using Examination_System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Examination_System.Models;


namespace Examination_System.Controllers.Instructors
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        Context context = new Context();
        // GET: api/Instructor
        [HttpGet]
        public IActionResult Get()
        {
            var instructors = context.Instructors.ToList();
            if (instructors == null || instructors.Count == 0)
            {
                return NotFound("No instructors found.");
            }
            return Ok(instructors);
        }

        // GET: api/Instructor/5
        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            var instructor = context.Instructors.Find(id);
            if(instructor == null)
            {
                return NotFound($"Instructor with ID {id} not found.");
            }
            return Ok($"GetById method in Instructor Controller is working for ID: {id}");
        }

        // POST: api/Instructor
        [HttpPost]
        public IActionResult Create(Models.Instructor instructor)
        {
            if(instructor == null)
            {
                return BadRequest("Instructor data is null.");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Instructors.Add(instructor);
            context.SaveChanges();
            return Ok("Create method in Instructor Controller is working!");
        }

        // PUT: api/Instructor/5
        [HttpPut("{id}")]
        public IActionResult Update(int id,Models.Instructor updatedInstructor)
        {
            var instructor = context.Instructors.Find(id);

            if(instructor == null)
            {
                return NotFound($"Instructor with ID {id} not found.");
            }
            instructor.Name = updatedInstructor.Name;
            instructor.Email = updatedInstructor.Email;
            instructor.Department = updatedInstructor.Department;

            context.Instructors.Update(instructor);
            context.SaveChanges();

            return Ok($"Update method in Instructor Controller is working for ID: {id}");
        }

        // Delete:api/Instructor/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var instructor = context.Instructors.Find(id);
            if(instructor == null)
            {
                return NotFound($"Instructor with ID {id} not found.");
            }
            context.Instructors.Remove(instructor);
            context.SaveChanges();
            return Ok($"Delete method in Instructor Controller is working for ID: {id}");
        }


    }
}
