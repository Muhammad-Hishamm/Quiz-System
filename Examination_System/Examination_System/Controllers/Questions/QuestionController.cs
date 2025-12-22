using Examination_System.DTOs.Questions;
using Examination_System.Services;
using Examination_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Questions
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;

        public QuestionController()
        {
            _questionService = new QuestionService();
        }

        // GET: api/Question
        [HttpGet]
        public ActionResult<IQueryable<GetQuestionViewModel>> Get()
        {
            var vm = new GetQuestionViewModel();
            var dtos = _questionService.GetAll();
            var questions = dtos.Select(dto => vm.ToViewModel(dto));
            return Ok(questions);
        }

        // GET: api/Question/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GetQuestionViewModel>> GetById(int id)
        {
            var vm = new GetQuestionViewModel();
            var dto = await _questionService.GetByIdAsync(id).ConfigureAwait(false);
            if (dto == null) return NotFound();
            return Ok(vm.ToViewModel(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionViewModel question)
        {
            if (question == null) return BadRequest("Question data is null.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createDto = new CreateQuestionDTO().ToDTO(question);
            var ok = await _questionService.Create(createDto).ConfigureAwait(false);
            if (!ok) return BadRequest();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateQuestionViewModel updatedQuestion)
        {
            if (updatedQuestion == null) return BadRequest("this question is null");
            if (!ModelState.IsValid) return BadRequest("this question doesn't follow the constraints");

            var updatedDto = new UpdateQuestionDto().ToDTO(updatedQuestion);
            var ok = await _questionService.Update(id, updatedDto).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _questionService.Delete(id).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }
    }
}
