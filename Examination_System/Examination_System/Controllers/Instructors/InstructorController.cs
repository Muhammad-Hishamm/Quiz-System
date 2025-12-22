using Examination_System.DTOs.Instructors;
using Examination_System.Services;
using Examination_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Instructors
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly InstructorService _instructorService;

        public InstructorController()
        {
            _instructorService = new InstructorService();
        }

        // GET: api/Instructor
        [HttpGet]
        public ActionResult<IQueryable<GetInstructorViewModel>> Get()
        {
            var vm = new GetInstructorViewModel();
            var dtos = _instructorService.GetAll();
            var instructors = dtos.Select(dto => vm.ToViewModel(dto));
            return Ok(instructors);
        }

        // GET: api/Instructor/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GetInstructorViewModel>> GetById(int id)
        {
            var vm = new GetInstructorViewModel();
            var dto = await _instructorService.GetByIdAsync(id).ConfigureAwait(false);
            if (dto == null) return NotFound();
            return Ok(vm.ToViewModel(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInstructorViewModel instructor)
        {
            if (instructor == null) return BadRequest("Instructor data is null.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createDto = new CreateInstructorDTO().ToDTO(instructor);
            var ok = await _instructorService.Create(createDto).ConfigureAwait(false);
            if (!ok) return BadRequest();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateInstructorViewModel updatedInstructor)
        {
            if (updatedInstructor == null) return BadRequest("this instructor is null");
            if (!ModelState.IsValid) return BadRequest("this instructor doesn't follow the constraints");

            var updatedDto = new UpdateInstructorDto().ToDTO(updatedInstructor);
            var ok = await _instructorService.Update(id, updatedDto).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _instructorService.Delete(id).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }
    }
}
    