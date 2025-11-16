using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.Trade;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> Home()
        {
            List<TradeReadDto> trades = await _tradeService.GetAllAsync();
            return Ok(trades);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Validate([FromBody]TradeCreateDto trade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _tradeService.CreateAsync(trade);
            return Ok(trade);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ShowUpdateForm(int id)
        {
            TradeReadDto? trade = await _tradeService.GetByIdAsync(id);
            if (trade == null)
            {
                return NotFound($"Le trade {id} n'existe pas.");
            }
            return Ok(trade);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTrade(int id, [FromBody] TradeUpdateDto trade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedTrade = await _tradeService.UpdateAsync(id, trade);
            if (updatedTrade == null)
            {
                return NotFound($"Le trade {id} n'existe pas.");
            }

            return Ok(updatedTrade);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTrade(int id)
        {
            bool deletedTrade = await _tradeService.DeleteAsync(id);
            if (!deletedTrade)
            {
                return NotFound($"Le trade {id} n'existe pas.");
            }
            return Ok();
        }
    }
}