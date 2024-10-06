using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ViewModels;
using CodeSphere.Domain.Interfaces.Repos;
using Freelancing_Website.Services;

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
            var reviewViews = _mapper.Map<List<ReviewView>>(reviews.Items);
            DataWithPagination<ReviewView> reviewsWithPagination = new DataWithPagination<ReviewView>();
            reviewsWithPagination.Items = reviewViews;
            reviewsWithPagination.PaginationMetaData = reviews.PaginationMetaData;
            return Ok(reviewsWithPagination);
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetReviewsForClient(int clientId, int pageNumber = 1, int pageSize = 10)
        {
            var reviews = await _reviewService.GetReviewsByClientIdAsync(clientId, pageNumber, pageSize);
            var reviewViews = _mapper.Map<List<ReviewView>>(reviews.Items);
            DataWithPagination<ReviewView> reviewsWithPagination = new DataWithPagination<ReviewView>();
            reviewsWithPagination.Items = reviewViews;
            reviewsWithPagination.PaginationMetaData = reviews.PaginationMetaData;
            return Ok(reviewsWithPagination);
        }

        [HttpPost("Client/{clientId}/Freelancer/{freelancerId}")]
        public async Task<IActionResult> CreateReview(int clientId,int freelancerId,[FromBody] ReviewForCreate reviewForCreate)
        {
            var review = _mapper.Map<Review>(reviewForCreate);
            await _reviewService.CreateReviewAsync(clientId,freelancerId,review);
            var reviewView = _mapper.Map<ReviewView>(review);
            return Ok(reviewView);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id,[FromBody] ReviewForCreate reviewForCreate)
        {
            var review= await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            review.Rating = reviewForCreate.Rating;
            review.Comment = reviewForCreate.Comment;
            await _reviewService.UpdateReviewAsync(review);
            var reviewViewModel = _mapper.Map<ReviewView>(review);
            return Ok(reviewViewModel);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviewById(int id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return NoContent();
        }
    }
}
