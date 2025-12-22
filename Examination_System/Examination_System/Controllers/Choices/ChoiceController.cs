using Examination_System.DTOs.Choices;
using Examination_System.Services;
using Examination_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Choices
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoiceController : ControllerBase
    {
        private readonly ChoiceService _choiceService;

        public ChoiceController()
        {
            _choiceService = new ChoiceService();
        }

        // GET: api/Choice
        [HttpGet]
        public ActionResult<IQueryable<GetChoiceViewModel>> Get()
        {
            var vm = new GetChoiceViewModel();
            var dtos  = _choiceService.GetAll();
            var choices = dtos.Select(dto => vm.ToViewModel(dto));
            return Ok(choices);
        }

        // GET: api/Choice/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GetChoiceViewModel>> GetById(int id)
        {
            var vm = new GetChoiceViewModel();
            var dto = await _choiceService.GetByIdAsync(id).ConfigureAwait(false);
            if (dto == null) return NotFound();
            return Ok(vm.ToViewModel(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateChoiceViewModel choice)
        {
            if (choice == null) return BadRequest("Choice data is null.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createDto = new CreateChoiceDTO().ToDTO(choice);
            var ok = await _choiceService.Create(createDto).ConfigureAwait(false);
            if (!ok) return BadRequest();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateChoiceViewModel updatedChoice)
        {
            if (updatedChoice == null) return BadRequest("this choice is null");
            if (!ModelState.IsValid) return BadRequest("this choice doesn't follow the constraints");

            var updatedDto = new UpdateChoiceDto().ToDTO(updatedChoice);
            var ok = await _choiceService.Update(id, updatedDto).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _choiceService.Delete(id).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }
    }
}