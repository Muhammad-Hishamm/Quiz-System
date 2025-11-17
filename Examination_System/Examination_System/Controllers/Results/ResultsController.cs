using Examination_System.Models;
using Examination_System.Services;
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

        [HttpGet]
        public IQueryable<Result> Get()
        {
            var results = _resultService.GetAll();
            if (results == null) return null;
            return results;
        }

        [HttpGet("{id}")]
        public async Task<Result> GetById(int id)
        {
            var result = await _resultService.GetById(id);
            if (result == null) return null;
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Result result)
        {
            if (await _resultService.Create(result))
                return Ok("Create method in Result Controller is working!");
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Result updatedResult)
        {
            if (await _resultService.Update(id, updatedResult))
                return Ok($"the Result with id {id} is updated successfully");
            return BadRequest("this result doesn't follow the result Constraints");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _resultService.Delete(id))
                return Ok($"the result with id {id} is deleted successfully");
            return BadRequest("this result doesn't exist");
        }
    }
}