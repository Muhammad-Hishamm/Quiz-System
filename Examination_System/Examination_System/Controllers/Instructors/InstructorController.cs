using Examination_System.Data;
using Examination_System.Models;
using Examination_System.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace Examination_System.Controllers.Instructors
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        GeneralRepository<Instructor> _instructorRepository;
        public InstructorController()
        {
            _instructorRepository = new GeneralRepository<Instructor>();
        }
        // GET: api/Instructor
        [HttpGet]
        public IQueryable<Instructor> Get()
        {
            var instructors = _instructorRepository.GetAll();
            if (instructors == null)
            {
                return null;
            }
            return instructors;
        }

        // GET: api/Instructor/1
        [HttpGet("{id}")]
        public async Task<Instructor> GetById(int id)
        {
            var instructor = await _instructorRepository.GetByIDAsync(id);
            if (instructor == null)
            {
                return null;
            }
            return instructor;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Instructor instrucor)
        {
            if (instrucor == null)
            {
                return BadRequest("instructor data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _instructorRepository.CreateAsync(instrucor);
            return Ok("Create method in Instructor Controller is working!");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Instructor updatedInstructor)
        {
            if (updatedInstructor == null)
                return BadRequest("this instructor is null");
            if (!ModelState.IsValid)
            {
                return BadRequest("this instructor doesn't follow the instructor Constraints");
            }
            await _instructorRepository.UpdateAsync(updatedInstructor);
            return Ok($"the Instructor with id {id} is updated successully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _instructorRepository.DeleteAsync(id);
            return Ok($"the instructor with id {id} is deleted successully");
        }
    }
}
