using P7CreateRestApi.DTOs.RuleName;
using P7CreateRestApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuleNameController : ControllerBase
    {
        private readonly IRuleNameService _ruleNameService;
        public RuleNameController(IRuleNameService ruleNameService)
        {
            _ruleNameService = ruleNameService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllRuleName()
        {
            var ruleNames = await _ruleNameService.GetAllAsync();
            return Ok(ruleNames);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateRuleName([FromBody]RuleNameCreateDto ruleName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdRuleName = await _ruleNameService.CreateAsync(ruleName);
            return Ok(createdRuleName);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRuleName(int id)
        {
            var ruleName = await _ruleNameService.GetByIdAsync(id);
            if (ruleName == null)
            {
                return NotFound($"Le RuleName {id} n'existe pas.");
            }
            return Ok(ruleName);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRuleName(int id, [FromBody] RuleNameUpdateDto rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedRuleName = await _ruleNameService.UpdateAsync(id, rating);
            if (updatedRuleName == null)
            {
                return NotFound($"Le RuleName {id} n'existe pas.");
            }
            return Ok(updatedRuleName);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRuleName(int id)
        {
            bool ruleName = await _ruleNameService.DeleteAsync(id);
            if (!ruleName)
            {
                return NotFound($"Le RuleName {id} n'existe pas.");
            }            
            return NoContent();
        }
    }
}