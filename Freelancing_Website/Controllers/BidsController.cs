using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using CodeSphere.Domain.Models;
using CodeSphere.Domain.Interfaces.Repos;
using Microsoft.EntityFrameworkCore;

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
            var bids = _mapper.Map<List<BidView>>(result.Items);
            DataWithPagination<BidView> bidsWithPagination = new DataWithPagination<BidView>();

            bidsWithPagination.Items = bids;
            bidsWithPagination.PaginationMetaData = result.PaginationMetaData;

            return Ok(bidsWithPagination);
        }

        [HttpGet("freelancer/{freelancerId}")]
        public async Task<IActionResult> GetBidsByFreelancerId(int freelancerId, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _bidService.GetBidsByFreelancerIdAsync(freelancerId, pageNumber, pageSize);
            var bids = _mapper.Map<List<BidView>>(result.Items);
            DataWithPagination<BidView> bidsWithPagination = new DataWithPagination<BidView>();

            bidsWithPagination.Items = bids;
            bidsWithPagination.PaginationMetaData = result.PaginationMetaData;

            return Ok(bidsWithPagination);
        }

        [HttpPost("freelancer/{freelancerId}/project/{projectId}")]
        public async Task<IActionResult> CreateBid(int freelancerId, int projectId, [FromBody] BidForCreate bidForCreate)
        {
            try
            {
                var bid = _mapper.Map<Bid>(bidForCreate);
                await _bidService.CreateBidAsync(freelancerId, projectId, bid);

                var bidViewModel = _mapper.Map<BidView>(bid);
                return CreatedAtAction(nameof(GetBidsByProjectId), new { projectId = bid.ProjectId }, bidViewModel);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("IX_Bids_ProjectId_FreelancerId") == true)
                {
                    return BadRequest(new { message = "You have already placed a bid on this project." });
                }

                return StatusCode(500, "An error occurred while creating the bid.");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidForCreate bidForCreate)
        {
            
            var oldBid = await _bidService.GetByIdAsync(id);
            if (oldBid==null)
            {
                return NotFound();
            }
            oldBid.Proposal=bidForCreate.Proposal;
            oldBid.Amount=bidForCreate.Amount;
            await _bidService.UpdateBidAsync(oldBid);
            var bidViewModel = _mapper.Map<BidView>(oldBid);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBidById(int id)
        {
            var bid = await _bidService.GetByIdAsync(id);
            var bidView = _mapper.Map<BidView>(bid);
            return Ok(bidView);
        }
    }
}
