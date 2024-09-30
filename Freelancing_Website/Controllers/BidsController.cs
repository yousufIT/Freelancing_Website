using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using CodeSphere.Domain.Models;

namespace Freelancing_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidsController : ControllerBase
    {
        private readonly IBidService _bidService;
        private readonly IMapper _mapper;

        public BidsController(IBidService bidService, IMapper mapper)
        {
            _bidService = bidService;
            _mapper = mapper;
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetBidsByProjectId(int projectId, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _bidService.GetBidsByProjectIdAsync(projectId, pageNumber, pageSize);
            var bids = _mapper.Map<IEnumerable<BidView>>(result);
            return Ok(bids);
        }

        [HttpGet("freelancer/{freelancerId}")]
        public async Task<IActionResult> GetBidsByFreelancerId(int freelancerId, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _bidService.GetBidsByFreelancerIdAsync(freelancerId, pageNumber, pageSize);
            var bids = _mapper.Map<IEnumerable<BidView>>(result);
            return Ok(bids);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBid([FromBody] BidForCreate bidForCreate)
        {
            var bid = _mapper.Map<Bid>(bidForCreate);
            await _bidService.CreateBidAsync(bid);
            var bidViewModel = _mapper.Map<BidView>(bid);
            return CreatedAtAction(nameof(GetBidsByProjectId), new { projectId = bid.ProjectId }, bidViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidForCreate bidForCreate)
        {
            var bid = _mapper.Map<Bid>(bidForCreate);
            if (id != bid.Id)
            {
                return BadRequest();
            }
            await _bidService.UpdateBidAsync(bid);
            var bidViewModel = _mapper.Map<BidView>(bid);
            return Ok(bidViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            await _bidService.DeleteBidAsync(id);
            return NoContent();
        }

        [HttpDelete("project/{projectId}")]
        public async Task<IActionResult> DeleteBidsForProject(int projectId)
        {
            await _bidService.DeleteBidsForProjectAsync(projectId);
            return NoContent();
        }
    }
}
