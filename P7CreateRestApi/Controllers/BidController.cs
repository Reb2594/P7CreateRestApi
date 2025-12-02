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
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;
        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllBids()
        {
            List<BidReadDto> bids = await _bidService.GetAllAsync();
            return Ok(bids);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateBid([FromBody] BidCreateDto bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdBid = await _bidService.CreateAsync(bid);
            return Ok(createdBid);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBid(int id)
        {
            BidReadDto? bid = await _bidService.GetByIdAsync(id);
            if (bid == null)
            {
                return NotFound($"L'offre {id} n'existe pas.");
            }

            return Ok(bid);
        }

        [HttpPut]
        [Route("{id}")]
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

        [HttpDelete]
        [Route("{id}")]
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