using Examination_System.DTOs.Feedbacks;
using Examination_System.Services;
using Examination_System.ViewModels;
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

        // GET: api/Feedback
        [HttpGet]
        public ActionResult<IEnumerable<GetFeedbackViewModel>> Get()
        {
            var vm = new GetFeedbackViewModel();
            var dtos = _feedbackService.GetAll();
            var results = dtos.Select(dto => vm.ToViewModel(dto));
            return Ok(results);
        }

        // GET: api/Feedback/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GetFeedbackViewModel>> GetById(int id)
        {
            var vm = new GetFeedbackViewModel();
            var dto = await _feedbackService.GetByIdAsync(id).ConfigureAwait(false);
            if (dto == null) return NotFound();
            return Ok(vm.ToViewModel(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFeedbackViewModel model)
        {
            if (model == null) return BadRequest("Feedback data is null.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createDto = new CreateFeedbackDTO().ToDTO(model);
            var ok = await _feedbackService.Create(createDto).ConfigureAwait(false);
            if (!ok) return BadRequest();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateFeedbackViewModel updated)
        {
            if (updated == null) return BadRequest("this feedback is null");
            if (!ModelState.IsValid) return BadRequest("this feedback doesn't follow the constraints");

            var updatedDto = new UpdateFeedbackDto().ToDTO(updated);
            var ok = await _feedbackService.Update(id, updatedDto).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _feedbackService.Delete(id).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }
    }
}