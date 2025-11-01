using Examination_System.Data;
using Examination_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Examination_System.Controllers.Questions
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        Context context = new Context();
        // GET: api/Questions
        [HttpGet]
        public IActionResult Get()
        {
            var questions = context.Questions.ToList();
            if (questions == null || questions.Count == 0)
            {
                return NotFound("No Questions found.");
            }
            return Ok(questions);
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            var question = context.Questions.Find(id);
            if (question == null)
            {
                return NotFound($"question with ID {id} not found.");
            }
            return Ok($"GetById method in question Controller is working for ID: {id}");
        }

        // POST: api/question
        [HttpPost]
        public IActionResult Create(Question question)
        {
            if (question == null)
            {
                return BadRequest("question data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Questions.Add(question);
            context.SaveChanges();
            return Ok("Create method in question Controller is working!");
        }

        // PUT: api/question/5
        [HttpPut("{id}")]
        public IActionResult Update(int id,Question updatedQuestion)
        {
            var question = context.Questions.Find(id);

            if (question == null)
            {
                return NotFound($"Questions with ID {id} not found.");
            }
           question.Instructor = updatedQuestion.Instructor;
            question.Level = updatedQuestion.Level;
            context.Questions.Add(question);
            context.SaveChanges();

            return Ok($"Update method in Question Controller is working for ID: {id}");
        }

        // Delete:api/Questions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var question = context.Questions.Find(id);
            if (question == null)
            {
                return NotFound($"question with ID {id} not found.");
            }
            context.Questions.Remove(question);
            context.SaveChanges();
            return Ok($"Delete method in question Controller is working for ID: {id}");
        }
    }
}
