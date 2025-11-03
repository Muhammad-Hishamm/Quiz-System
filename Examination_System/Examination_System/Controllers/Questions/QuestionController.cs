using Examination_System.Data;
using Examination_System.Models;
using Examination_System.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Examination_System.Controllers.Questions
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        GeneralRepository<Question> _questionRepository;
        public QuestionController()
        {
            _questionRepository = new GeneralRepository<Question>();
        }
        // GET: api/Question
        [HttpGet]
        public IQueryable<Question> Get()
        {
            var questions = _questionRepository.GetAll();
            if (questions == null)
            {
                return null;
            }
            return questions;
        }

        // GET: api/Question/1
        [HttpGet("{id}")]
        public async Task<Question> GetById(int id)
        {
            var question = await _questionRepository.GetByIDAsync(id);
            if (question == null)
            {
                return null;
            }
            return question;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            if (question == null)
            {
                return BadRequest("question data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _questionRepository.CreateAsync(question);
            return Ok("Create method in Question Controller is working!");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Question updatedQuestion)
        {
            if (updatedQuestion == null)
                return BadRequest("this question is null");
            if (!ModelState.IsValid)
            {
                return BadRequest("this question doesn't follow the question Constraints");
            }
            await _questionRepository.UpdateAsync(updatedQuestion);
            return Ok($"the Question with id {id} is updated successully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _questionRepository.DeleteAsync(id);
            return Ok($"the question with id {id} is deleted successully");
        }
    }
}
