using Examination_System.DTOs.Results;
using Examination_System.Services;
using Examination_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Results
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly ResultService _resultService;

        public ResultController()

        {
            _resultService = new ResultService();
        }

        // GET: api/Result
        [HttpGet]
        public ActionResult<IQueryable<GetResultViewModel>> Get()
        {
            var vm = new GetResultViewModel();
            var dtos = _resultService.GetAll();
            var results = dtos.Select(dto => vm.ToViewModel(dto));
            return Ok(results);
        }

        // GET: api/Result/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GetResultViewModel>> GetById(int id)
        {
            var vm = new GetResultViewModel();
            var dto = await _resultService.GetByIdAsync(id).ConfigureAwait(false);
            if (dto == null) return NotFound();
            return Ok(vm.ToViewModel(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateResultViewModel model)
        {
            if (model == null) return BadRequest("Result data is null.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createDto = new CreateResultDTO().ToDTO(model);
            var ok = await _resultService.Create(createDto).ConfigureAwait(false);
            if (!ok) return BadRequest();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateResultViewModel updated)
        {
            if (updated == null) return BadRequest("this result is null");
            if (!ModelState.IsValid) return BadRequest("this result doesn't follow the constraints");

            var updatedDto = new UpdateResultDto().ToDTO(updated);
            var ok = await _resultService.Update(id, updatedDto).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _resultService.Delete(id).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }
    }
}