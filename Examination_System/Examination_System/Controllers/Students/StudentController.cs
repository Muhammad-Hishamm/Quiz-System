using Examination_System.DTOs.Students;
using Examination_System.Services;
using Examination_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;


namespace Examination_System.Controllers.Students
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController()
        {
            _studentService = new StudentService();
        }

        // GET: api/Student
        [HttpGet]
        public ActionResult<IQueryable<GetStudentViewModel>> Get()
        {
            var vm = new GetStudentViewModel();
            var dtos = _studentService.GetAll();
            var students = dtos.Select(dto => vm.ToViewModel(dto));
            return Ok(students);
        }

        // GET: api/Student/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GetStudentViewModel>> GetById(int id)
        {
            var vm = new GetStudentViewModel();
            var dto = await _studentService.GetByIdAsync(id).ConfigureAwait(false);
            if (dto == null) return NotFound();
            return Ok(vm.ToViewModel(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentViewModel student)
        {
            if (student == null) return BadRequest("Student data is null.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createDto = new CreateStudentDTO().ToDTO(student);
            var ok = await _studentService.Create(createDto).ConfigureAwait(false);
            if (!ok) return BadRequest();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateStudentViewModel updatedStudent)
        {
            if (updatedStudent == null) return BadRequest("this student is null");
            if (!ModelState.IsValid) return BadRequest("this student doesn't follow the constraints");

            var updatedDto = new UpdateStudentDto().ToDTO(updatedStudent);
            var ok = await _studentService.Update(id, updatedDto).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _studentService.Delete(id).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }
    }
}
