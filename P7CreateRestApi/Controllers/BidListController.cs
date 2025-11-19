using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.BidList;
using Microsoft.AspNetCore.Authorization;
using P7CreateRestApi.DTOs.Trade;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly IBidListService _bidListService;
        public BidListController(IBidListService bidListService)
        {
            _bidListService = bidListService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllBidLists()
        {
            List<BidListReadDto> bidLists = await _bidListService.GetAllAsync();
            return Ok(bidLists);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateBid([FromBody] BidListCreateDto bidList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdBidList = await _bidListService.CreateAsync(bidList);
            return Ok(createdBidList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBid(int id)
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
            bool deletedBidList = await _bidListService.DeleteAsync(id);
            if (!deletedBidList)
            {
                return NotFound($"L'offre {id} n'existe pas.");
            }
            return NoContent();
        }
    }
}