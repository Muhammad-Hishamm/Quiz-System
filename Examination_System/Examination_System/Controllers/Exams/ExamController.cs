using Examination_System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Examination_System.Models;

namespace Examination_System.Controllers.Exams
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        Context context = new Context();
        // GET: api/Exam
        [HttpGet]
        public IActionResult Get()
        {
            var exams = context.Exams.ToList();
            if (exams == null || exams.Count == 0)
            {
                return NotFound("No exams found.");
            }
            return Ok("Get method in exam Controller is working!");
        }

        // GET: api/Exam/1

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var exam = context.Exams.Find(id);
            if (exam == null)
            {
                return NotFound($"Exam with id {id} is not found.");
            }
            return Ok($"GetById method in Exam Controller is working for ID: {id}");
        }

        [HttpPost]
        public IActionResult Create(Models.Exam exam)
        {

            if (exam == null)
            {
                return NotFound("Exam data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Exams.Add(exam);
            context.SaveChanges();
            return Ok("Create method in Exam Controller is working!");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, dynamic updatedExam)
        {
            var exam = context.Exams.Find(id);
            if (exam == null)
            {
                return NotFound($"the Exam with id {id} is not exist ya batooot");
            }
            if (updatedExam == null)
            {
                return BadRequest($"the updated exam is null");
            }
            exam.Course = updatedExam.Course;
            exam.Title = updatedExam.Title;

            context.Exams.Update(exam);
            context.SaveChanges();
            return Ok($"the exam with id {id} is updated successully");

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var exam = context.Exams.Find(id);
            if (exam == null)
            {
                return NotFound($"the Exam with id {id} is not found");
            }
            return Ok($"the exam with id {id} is deleted successully");
        }

    }

}
