using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ViewModels;

namespace Freelancing_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [HttpGet("freelancer/{freelancerId}")]
        public async Task<IActionResult> GetReviewsForFreelancer(int freelancerId, int pageNumber = 1, int pageSize = 10)
        {
            var reviews = await _reviewService.GetReviewsByFreelancerIdAsync(freelancerId, pageNumber, pageSize);
            var reviewViews = _mapper.Map<IEnumerable<ReviewView>>(reviews);
            return Ok(reviewViews);
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetReviewsForClient(int clientId, int pageNumber = 1, int pageSize = 10)
        {
            var reviews = await _reviewService.GetReviewsByClientIdAsync(clientId, pageNumber, pageSize);
            var reviewViews = _mapper.Map<IEnumerable<ReviewView>>(reviews);
            return Ok(reviewViews);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewForCreate reviewForCreate)
        {
            var review = _mapper.Map<Review>(reviewForCreate);
            await _reviewService.CreateReviewAsync(review);
            return CreatedAtAction(nameof(GetReviewsForFreelancer), new { freelancerId = review.FreelancerId }, review);
        }
    }
}
