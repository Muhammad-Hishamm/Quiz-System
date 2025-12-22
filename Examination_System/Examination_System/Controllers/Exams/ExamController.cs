using Examination_System.Data;
using Examination_System.DTOs.Exams;
using Examination_System.Models;
using Examination_System.Repositories;
using Examination_System.Services;
using Examination_System.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Exams
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ExamService _examService;

        public ExamController()
        {
            _examService = new ExamService();
        }

        // GET: api/Exam
        [HttpGet]
        public ActionResult<IQueryable<GetExamViewModel>> Get()
        {
            var viewModel = new GetExamViewModel();
            var dtos = _examService.GetAll();
            var exams = dtos.Select(dto => viewModel.ToViewModel(dto));
            return Ok(exams);
        }

        // GET: api/Exam/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GetExamViewModel>> GetById(int id)
        {
            var viewModel = new GetExamViewModel();
            var examDto = await _examService.GetByIdAsync(id).ConfigureAwait(false);
            if (examDto == null) return NotFound();
            var examViewModel = viewModel.ToViewModel(examDto);
            return Ok(examViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExamViewModel exam)
        {
            if (exam == null) return BadRequest("Exam data is null.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createDto = new CreateExamDTO().ToDTO(exam);
            var ok = await _examService.Create(createDto).ConfigureAwait(false);
            if (!ok) return BadRequest();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateExamViewModel updatedExam)
        {
            if (updatedExam == null) return BadRequest("this exam is null");
            if (!ModelState.IsValid) return BadRequest("this exam doesn't follow the Exam Constraints");

            var updatedExamDto = new UpdateExamDto().ToDTO(updatedExam);
            var ok = await _examService.Update(id, updatedExamDto).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _examService.Delete(id).ConfigureAwait(false);
            if (!ok) return NotFound();
            return Ok();
        }
    }
}
