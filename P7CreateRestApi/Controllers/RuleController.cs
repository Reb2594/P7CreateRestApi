using P7CreateRestApi.DTOs.Rule;
using P7CreateRestApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuleController : ControllerBase
    {
        private readonly IRuleService _ruleService;
        public RuleController(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllRule()
        {
            var rules = await _ruleService.GetAllAsync();
            return Ok(rules);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateRule([FromBody]RuleCreateDto rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdRule = await _ruleService.CreateAsync(rule);
            return Ok(createdRule);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRule(int id)
        {
            var rule = await _ruleService.GetByIdAsync(id);
            if (rule == null)
            {
                return NotFound($"Le Rule {id} n'existe pas.");
            }
            return Ok(rule);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRule(int id, [FromBody] RuleUpdateDto rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedRule = await _ruleService.UpdateAsync(id, rating);
            if (updatedRule == null)
            {
                return NotFound($"Le Rule {id} n'existe pas.");
            }
            return Ok(updatedRule);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRule(int id)
        {
            bool rule = await _ruleService.DeleteAsync(id);
            if (!rule)
            {
                return NotFound($"Le Rule {id} n'existe pas.");
            }            
            return NoContent();
        }
    }
}