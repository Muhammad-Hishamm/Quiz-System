using Examination_System.Data;
using Examination_System.Models;
using Examination_System.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers.Exams
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        GeneralRepository<Exam> _examRepository;
        public ExamController()
        {
            _examRepository = new GeneralRepository<Exam>();
        }
        // GET: api/Exam
        [HttpGet]
        public IQueryable<Exam> Get()
        {
            var courses = _examRepository.GetAll();
            if (courses == null)
            {
                return null;
            }
            return courses;
        }

        // GET: api/Exam/1
        [HttpGet("{id}")]
        public async Task<Exam> GetById(int id)
        {
            var exam = await _examRepository.GetByIDAsync(id);
            if (exam == null)
            {
                return null;
            }
            return exam;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Exam exam)
        {
            if (exam == null)
            {
                return BadRequest("Exam data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _examRepository.CreateAsync(exam);
            return Ok("Create method in Exam Controller is working!");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Exam updatedExam)
        {
            if (updatedExam == null)
                return BadRequest("this exam is null");
            if (!ModelState.IsValid)
            {
                return BadRequest("this exam doesn't follow the exam Constraints");
            }
            await _examRepository.UpdateAsync(updatedExam);
            return Ok($"the Exam with id {id} is updated successully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _examRepository.DeleteAsync(id);
            return Ok($"the couruse with id {id} is deleted successully");
        }

    }

}
