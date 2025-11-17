using Examination_System.Models;
using Examination_System.Services;
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

        [HttpGet]
        public IQueryable<Choice> Get()
        {
            var choices = _choiceService.GetAll();
            if (choices == null) return null;
            return choices;
        }

        [HttpGet("{id}")]
        public async Task<Choice> GetById(int id)
        {
            var choice = await _choiceService.GetById(id);
            if (choice == null) return null;
            return choice;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Choice choice)
        {
            if (await _choiceService.Create(choice))
                return Ok("Create method in Choice Controller is working!");
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Choice updatedChoice)
        {
            if (await _choiceService.Update(id, updatedChoice))
                return Ok($"the Choice with id {id} is updated successfully");
            return BadRequest("this choice doesn't follow the choice Constraints");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _choiceService.Delete(id))
                return Ok($"the choice with id {id} is deleted successfully");
            return BadRequest("this choice doesn't exist");
        }
    }
}