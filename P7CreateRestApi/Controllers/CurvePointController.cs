using P7CreateRestApi.Domain;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.CurvePoint;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des CurvePoints.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/curvepoints")]
    public class CurvePointController : ControllerBase
    {
        private readonly ICurvePointService _curvePointService;

        public CurvePointController(ICurvePointService curvePointService)
        {
            _curvePointService = curvePointService;
        }

        /// <summary>
        /// Récupère tous les CurvePoints.
        /// </summary>
        /// <returns>Liste des CurvePoints.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCurvePoints()
        {
            List<CurvePointReadDto> curvePoints = await _curvePointService.GetAllAsync();
            return Ok(curvePoints);
        }

        /// <summary>
        /// Crée un nouveau CurvePoint.
        /// </summary>
        /// <param name="curvePoint">Données du CurvePoint à créer.</param>
        /// <returns>Le CurvePoint créé.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCurvePoint([FromBody] CurvePointCreateDto curvePoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCurvePoint = await _curvePointService.CreateAsync(curvePoint);
            return Ok(createdCurvePoint);
        }

        /// <summary>
        /// Récupère un CurvePoint par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant du CurvePoint.</param>
        /// <returns>Le CurvePoint correspondant.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurvePoint(int id)
        {
            CurvePointReadDto? curvePoint = await _curvePointService.GetByIdAsync(id);
            if (curvePoint == null)
            {
                return NotFound($"Le CurvePoint {id} n'existe pas.");
            }
            return Ok(curvePoint);
        }

        /// <summary>
        /// Met à jour un CurvePoint existant.
        /// </summary>
        /// <param name="id">Identifiant du CurvePoint à mettre à jour.</param>
        /// <param name="curvePoint">Données de mise à jour.</param>
        /// <returns>Le CurvePoint mis à jour.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurvePoint(int id, [FromBody] CurvePointUpdateDto curvePoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedCurvePoint = await _curvePointService.UpdateAsync(id, curvePoint);
            if (updatedCurvePoint == null)
            {
                return NotFound($"Le CurvePoint {id} n'existe pas.");
            }
            return Ok(updatedCurvePoint);
        }

        /// <summary>
        /// Supprime un CurvePoint par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant du CurvePoint à supprimer.</param>
        /// <returns>Code 204 si la suppression est réussie.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurvePoint(int id)
        {
            bool deletedCurvePoint = await _curvePointService.DeleteAsync(id);
            if (!deletedCurvePoint)
            {
                return NotFound($"Le CurvePoint {id} n'existe pas.");
            }
            return NoContent();
        }
    }
}
