using Examination_System.Models;
using Examination_System.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Feedbacks
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;
        public FeedbackController()
        {
            _feedbackService = new FeedbackService();
        }

        [HttpGet]
        public IQueryable<Feedback> Get()
        {
            var feedbacks = _feedbackService.GetAll();
            if (feedbacks == null) return null;
            return feedbacks;
        }

        [HttpGet("{id}")]
        public async Task<Feedback> GetById(int id)
        {
            var feedback = await _feedbackService.GetById(id);
            if (feedback == null) return null;
            return feedback;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Feedback feedback)
        {
            if (await _feedbackService.Create(feedback))
                return Ok("Create method in Feedback Controller is working!");
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Feedback updatedFeedback)
        {
            if (await _feedbackService.Update(id, updatedFeedback))
                return Ok($"the Feedback with id {id} is updated successfully");
            return BadRequest("this feedback doesn't follow the feedback Constraints");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _feedbackService.Delete(id))
                return Ok($"the feedback with id {id} is deleted successfully");
            return BadRequest("this feedback doesn't exist");
        }
    }
}