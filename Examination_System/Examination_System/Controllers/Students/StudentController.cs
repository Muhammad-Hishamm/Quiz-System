using Examination_System.Data;
using Examination_System.Models;
using Examination_System.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace Examination_System.Controllers.Students
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        GeneralRepository<Student> _studentRepository;
        public StudentController()
        {
            _studentRepository = new GeneralRepository<Student>();
        }
        // GET: api/student
        [HttpGet]
        public IQueryable<Student> Get()
        {
            var students = _studentRepository.GetAll();
            if (students == null)
            {
                return null;
            }
            return students;
        }

        // GET: api/student/1
        [HttpGet("{id}")]
        public async Task<Student> GetById(int id)
        {
            var student = await _studentRepository.GetByIDAsync(id);
            if (student == null)
            {
                return null;
            }
            return student;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (student == null)
            {
                return BadRequest("student data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _studentRepository.CreateAsync(student);
            return Ok("Create method in student Controller is working!");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student updatedStudent)
        {
            if (updatedStudent == null)
                return BadRequest("this student is null");
            if (!ModelState.IsValid)
            {
                return BadRequest("this student doesn't follow the student Constraints");
            }
            await _studentRepository.UpdateAsync(updatedStudent);
            return Ok($"the student with id {id} is updated successully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentRepository.DeleteAsync(id);
            return Ok($"the student with id {id} is deleted successully");
        }
    }
}
