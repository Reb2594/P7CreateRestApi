using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.Bid;
using Microsoft.AspNetCore.Authorization;
using P7CreateRestApi.DTOs.Trade;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des offres (Bids).
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/bids")]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        /// <summary>
        /// Récupère toutes les offres.
        /// </summary>
        /// <returns>Liste des offres.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBids()
        {
            List<BidReadDto> bids = await _bidService.GetAllAsync();
            return Ok(bids);
        }

        /// <summary>
        /// Récupère une offre par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de l'offre.</param>
        /// <returns>L'offre correspondante.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBid(int id)
        {
            BidReadDto? bid = await _bidService.GetByIdAsync(id);
            if (bid == null)
            {
                return NotFound($"L'offre {id} n'existe pas.");
            }
            return Ok(bid);
        }

        /// <summary>
        /// Crée une nouvelle offre.
        /// </summary>
        /// <param name="bid">Données de l'offre à créer.</param>
        /// <returns>L'offre créée.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateBid([FromBody] BidCreateDto bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdBid = await _bidService.CreateAsync(bid);
            return Ok(createdBid);
        }

        /// <summary>
        /// Met à jour une offre existante.
        /// </summary>
        /// <param name="id">Identifiant de l'offre à mettre à jour.</param>
        /// <param name="bid">Données de mise à jour.</param>
        /// <returns>L'offre mise à jour.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidUpdateDto bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedBid = await _bidService.UpdateAsync(id, bid);
            if (updatedBid == null)
            {
                return NotFound($"L'offre {id} n'existe pas.");
            }
            return Ok(updatedBid);
        }

        /// <summary>
        /// Supprime une offre par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de l'offre à supprimer.</param>
        /// <returns>Code 204 si la suppression est réussie.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            bool deletedBid = await _bidService.DeleteAsync(id);
            if (!deletedBid)
            {
                return NotFound($"L'offre {id} n'existe pas.");
            }
            return NoContent();
        }
    }
}