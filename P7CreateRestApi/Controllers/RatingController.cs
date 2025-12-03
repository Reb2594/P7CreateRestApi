using P7CreateRestApi.DTOs.Rating;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace P7CreateRestApi.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des notations (Ratings).
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/ratings")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        /// <summary>
        /// Récupère toutes les notations.
        /// </summary>
        /// <returns>Liste des notations.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRatings()
        {
            var ratings = await _ratingService.GetAllAsync();
            return Ok(ratings);
        }

        /// <summary>
        /// Récupère une notation par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la notation.</param>
        /// <returns>La notation correspondante.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRating(int id)
        {
            var rating = await _ratingService.GetByIdAsync(id);
            if (rating == null)
            {
                return NotFound($"La notation {id} n'existe pas.");
            }
            return Ok(rating);
        }

        /// <summary>
        /// Crée une nouvelle notation.
        /// </summary>
        /// <param name="rating">Données de la notation à créer.</param>
        /// <returns>La notation créée.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateRating([FromBody] RatingCreateDto rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdRating = await _ratingService.CreateAsync(rating);
            return Ok(createdRating);
        }

        /// <summary>
        /// Met à jour une notation existante.
        /// </summary>
        /// <param name="id">Identifiant de la notation à mettre à jour.</param>
        /// <param name="rating">Données de mise à jour.</param>
        /// <returns>La notation mise à jour.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(int id, [FromBody] RatingUpdateDto rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedRating = await _ratingService.UpdateAsync(id, rating);
            if (updatedRating == null)
            {
                return NotFound($"La notation {id} n'existe pas.");
            }
            return Ok(updatedRating);
        }

        /// <summary>
        /// Supprime une notation par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la notation à supprimer.</param>
        /// <returns>Code 204 si la suppression est réussie.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            bool deletedRating = await _ratingService.DeleteAsync(id);
            if (!deletedRating)
            {
                return NotFound($"La notation {id} n'existe pas.");
            }
            return NoContent();
        }
    }
}