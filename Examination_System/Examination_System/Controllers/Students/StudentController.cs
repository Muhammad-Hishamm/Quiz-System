using Examination_System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Examination_System.Models;


namespace Examination_System.Controllers.Students
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        Context context = new Context();
        // GET: api/Student
        [HttpGet]
        public IActionResult Get()
        {
            var students = context.Students.ToList();
            if (students == null || students.Count == 0)
            {
                return NotFound("No students found.");
            }
            return Ok(students);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            var student = context.Students.Find(id);
            if(student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            return Ok($"GetById method in Student Controller is working for ID: {id}");
        }

        // POST: api/Student
        [HttpPost]
        public IActionResult Create(Models.Student student)
        {
            if(student == null)
            {
                return BadRequest("student data is null.");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Students.Add(student);
            context.SaveChanges();
            return Ok("Create method in student Controller is working!");
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public IActionResult Update(int id,Models.Student updatedStudent)
        {
            var student = context.Students.Find(id);

            if(student == null)
            {
                return NotFound($"student with ID {id} not found.");
            }
            student.Email = updatedStudent.Email;
            student.Name = updatedStudent.Name;

            context.Students.Update(student);
            context.SaveChanges();

            return Ok($"Update method in Student Controller is working for ID: {id}");
        }

        // Delete:api/Student/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = context.Students.Find(id);
            if(student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            context.Students.Remove(student);
            context.SaveChanges();
            return Ok($"Delete method in Student Controller is working for ID: {id}");
        }


    }
}
