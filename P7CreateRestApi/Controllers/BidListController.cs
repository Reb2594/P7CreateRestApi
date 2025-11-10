using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.BidList;
using Microsoft.AspNetCore.Authorization;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly IBidListService _bidListService;
        private readonly ILogger<BidListController> _logger;
        public BidListController(IBidListService bidListService, ILogger<BidListController> logger)
        {
            _bidListService = bidListService;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Validate([FromBody] BidListCreateDto bidList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _bidListService.CreateAsync(bidList);
            return Ok(bidList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ShowUpdateForm(int id)
        {
            BidListReadDto? bidList = await _bidListService.GetByIdAsync(id);
            if (bidList == null)
            {
                return NotFound($"L'offre {id} n'existe pas.");
            }

            return Ok(bidList);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidListUpdateDto bidList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedBid = await _bidListService.UpdateAsync(id, bidList);
            if (updatedBid == null)
            {
                return NotFound($"L'offre {id} n'existe pas.");
            }

            return Ok(updatedBid);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var bidList = await _bidListService.DeleteAsync(id);
            if (!bidList)
            {
                return NotFound($"L'offre {id} n'existe pas.");
            }
            return Ok();
        }
    }
}