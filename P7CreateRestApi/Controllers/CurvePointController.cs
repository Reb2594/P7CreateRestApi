using P7CreateRestApi.Domain;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.CurvePoint;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CurvePointController : ControllerBase
    {
        private readonly ICurvePointService _curvePointService;
        
        public CurvePointController(ICurvePointService curvePointService)
        {
            _curvePointService = curvePointService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllCurvePoints()
        {
            List<CurvePointReadDto> curvePoints = await _curvePointService.GetAllAsync();
            return Ok(curvePoints);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateCurvePoint([FromBody]CurvePointCreateDto curvePoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var createdCurvePoint = await _curvePointService.CreateAsync(curvePoint);
           return Ok(createdCurvePoint);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCurvePoint(int id)
        {
            CurvePointReadDto? curvePoint = await _curvePointService.GetByIdAsync(id);
            if (curvePoint == null)
            {
                return NotFound($"Le CurvePoint {id} n'existe pas.");
            }
            return Ok(curvePoint);
        }

        [HttpPut]
        [Route("{id}")]
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

        [HttpDelete]
        [Route("{id}")]
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