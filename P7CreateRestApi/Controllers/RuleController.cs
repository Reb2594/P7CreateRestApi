using P7CreateRestApi.DTOs.Rule;
using P7CreateRestApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace P7CreateRestApi.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des règles (Rules).
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/rules")]
    public class RuleController : ControllerBase
    {
        private readonly IRuleService _ruleService;

        public RuleController(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        /// <summary>
        /// Récupère toutes les règles.
        /// </summary>
        /// <returns>Liste des règles.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRules()
        {
            var rules = await _ruleService.GetAllAsync();
            return Ok(rules);
        }

        /// <summary>
        /// Récupère une règle par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la règle.</param>
        /// <returns>La règle correspondante.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRule(int id)
        {
            var rule = await _ruleService.GetByIdAsync(id);
            if (rule == null)
            {
                return NotFound($"La règle {id} n'existe pas.");
            }
            return Ok(rule);
        }

        /// <summary>
        /// Crée une nouvelle règle.
        /// </summary>
        /// <param name="rule">Données de la règle à créer.</param>
        /// <returns>La règle créée.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateRule([FromBody] RuleCreateDto rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdRule = await _ruleService.CreateAsync(rule);
            return Ok(createdRule);
        }

        /// <summary>
        /// Met à jour une règle existante.
        /// </summary>
        /// <param name="id">Identifiant de la règle à mettre à jour.</param>
        /// <param name="rule">Données de mise à jour.</param>
        /// <returns>La règle mise à jour.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRule(int id, [FromBody] RuleUpdateDto rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedRule = await _ruleService.UpdateAsync(id, rule);
            if (updatedRule == null)
            {
                return NotFound($"La règle {id} n'existe pas.");
            }
            return Ok(updatedRule);
        }

        /// <summary>
        /// Supprime une règle par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la règle à supprimer.</param>
        /// <returns>Code 204 si la suppression est réussie.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteRule(int id)
        {
            bool deletedRule = await _ruleService.DeleteAsync(id);
            if (!deletedRule)
            {
                return NotFound($"La règle {id} n'existe pas.");
            }
            return NoContent();
        }
    }
}