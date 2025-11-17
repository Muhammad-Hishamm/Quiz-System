using Examination_System.Data;
using Examination_System.Models;
using Examination_System.Repositories;
using Examination_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace Examination_System.Controllers.Instructors
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        InstructorService _instructorService;
        public InstructorController()
        {
            _instructorService = new InstructorService();
        }


        // GET: api/Instructor
        [HttpGet]
        public IQueryable<Instructor> Get()
        {
            var instructors = _instructorService.GetAll();
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
            var instructor = await _instructorService.GetById(id);
            if (instructor == null)
            {
                return null;
            }
            return instructor;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Instructor instrucor)
        {            
            if(await _instructorService.Create(instrucor))
                 return Ok("Create method in Instructor Controller is working!");
            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Instructor updatedInstructor)
        {

            if(await _instructorService.Update(id,updatedInstructor))
                return Ok($"the Instructor with id {id} is updated successully");
            return BadRequest("this instructor doesn't follow the instructor Constraints");
        }

            
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(await _instructorService.Delete(id))
                return Ok($"the instructor with id {id} is deleted successully");
            return BadRequest("this instructor doesn't exist");
        }
    }
}
