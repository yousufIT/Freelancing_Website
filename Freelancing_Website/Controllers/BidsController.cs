using AutoMapper;
using CodeSphere.Domain.Interfaces;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Freelancing_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IRepository<Bid> _bidRepository;
        private readonly ILogger<BidsController> _logger;
        private readonly IMapper _mapper;

        public BidsController(IRepository<Bid> bidRepository, ILogger<BidsController> logger, IMapper mapper)
        {
            _bidRepository = bidRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBids(int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching all bids.");
            var (bids, paginationMetaData) = await _bidRepository.GetAllAsync(pageNumber, pageSize);
            return Ok(_mapper.Map<List<BidView>>(bids));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBid(int id)
        {
            _logger.LogInformation($"Fetching bid with id {id}");
            var bid = await _bidRepository.GetByIdAsync(id);
            if

 (bid == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BidView>(bid));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBid([FromBody] BidForCreate bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating new bid.");
            var newBid = _mapper.Map<Bid>(bid);
            await _bidRepository.AddAsync(newBid);
            return CreatedAtAction(nameof(GetBid), new { id = ((IBase)newBid).Id }, _mapper.Map<BidView>(newBid));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidForCreate bid)
        {
            var oldBid = await _bidRepository.GetByIdAsync(id);
            if (oldBid == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Updating bid with id {id}");

            oldBid.Amount = bid.Amount;
            oldBid.Proposal = bid.Proposal;

            await _bidRepository.UpdateAsync(oldBid);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            _logger.LogInformation($"Deleting bid with id {id}");
            await _bidRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}