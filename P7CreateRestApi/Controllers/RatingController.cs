using P7CreateRestApi.DTOs.Rating;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly ILogger<RatingController> _logger;
        private readonly IRatingService _ratingService;
        public RatingController(ILogger<RatingController> logger, IRatingService ratingService)
        {
            _logger = logger;
            _ratingService = ratingService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> Home()
        {
            var ratings = await _ratingService.GetAllAsync();
            return Ok(ratings);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Validate([FromBody]RatingCreateDto rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdRating = await _ratingService.CreateAsync(rating);
            return Ok(createdRating);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ShowUpdateForm(int id)
        {
            var rating = await _ratingService.GetByIdAsync(id);
            if (rating == null)
            {
                return NotFound($"Le rating {id} n'existe pas.");
            }
            return Ok(rating);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRating(int id, [FromBody] RatingUpdateDto rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedRating = await _ratingService.UpdateAsync(id, rating);
            if (updatedRating == null)
            {
                return NotFound($"Le rating {id} n'existe pas.");
            }
            return Ok(updatedRating);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            bool deletedRating = await _ratingService.DeleteAsync(id);
            if (!deletedRating)
            {
                return NotFound($"Le rating {id} n'existe pas.");
            }
            return NoContent();
        }
    }
}