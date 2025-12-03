using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.Trade;
using Microsoft.AspNetCore.Authorization;

namespace P7CreateRestApi.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des transactions (Trades).
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/trades")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        /// <summary>
        /// Récupère toutes les transactions.
        /// </summary>
        /// <returns>Liste des transactions.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTrades()
        {
            List<TradeReadDto> trades = await _tradeService.GetAllAsync();
            return Ok(trades);
        }

        /// <summary>
        /// Récupère une transaction par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la transaction.</param>
        /// <returns>La transaction correspondante.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrade(int id)
        {
            TradeReadDto? trade = await _tradeService.GetByIdAsync(id);
            if (trade == null)
            {
                return NotFound($"La transaction {id} n'existe pas.");
            }
            return Ok(trade);
        }

        /// <summary>
        /// Crée une nouvelle transaction.
        /// </summary>
        /// <param name="trade">Données de la transaction à créer.</param>
        /// <returns>La transaction créée.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTrade([FromBody] TradeCreateDto trade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdTrade = await _tradeService.CreateAsync(trade);
            return Ok(createdTrade);
        }

        /// <summary>
        /// Met à jour une transaction existante.
        /// </summary>
        /// <param name="id">Identifiant de la transaction à mettre à jour.</param>
        /// <param name="trade">Données de mise à jour.</param>
        /// <returns>La transaction mise à jour.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrade(int id, [FromBody] TradeUpdateDto trade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedTrade = await _tradeService.UpdateAsync(id, trade);
            if (updatedTrade == null)
            {
                return NotFound($"La transaction {id} n'existe pas.");
            }
            return Ok(updatedTrade);
        }

        /// <summary>
        /// Supprime une transaction par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la transaction à supprimer.</param>
        /// <returns>Code 204 si la suppression est réussie.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteTrade(int id)
        {
            bool deletedTrade = await _tradeService.DeleteAsync(id);
            if (!deletedTrade)
            {
                return NotFound($"La transaction {id} n'existe pas.");
            }
            return NoContent();
        }
    }
}