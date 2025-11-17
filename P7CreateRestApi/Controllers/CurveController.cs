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
    public class CurveController : ControllerBase
    {
        private readonly ICurvePointService _curvePointService;
        private readonly ILogger<CurveController> _logger;
        public CurveController(ICurvePointService curvePointService, ILogger<CurveController> logger)
        {
            _curvePointService = curvePointService;
            _logger = logger;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> Home()
        {
            List<CurvePointReadDto> curvePoints = await _curvePointService.GetAllAsync();
            return Ok(curvePoints);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Validate([FromBody]CurvePointCreateDto curvePoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _curvePointService.CreateAsync(curvePoint);
            return Ok(curvePoint);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ShowUpdateForm(int id)
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
        public async Task<IActionResult> DeleteBid(int id)
        {
            bool deletedCurvePoint = await _curvePointService.DeleteAsync(id);
            if (!deletedCurvePoint)
            {
                return NotFound($"Le CurvePoint {id} n'existe pas.");
            }
            return Ok();
        }
    }
}