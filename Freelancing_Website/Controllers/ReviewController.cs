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
    public class ReviewController : ControllerBase
    {
        private readonly IRepository<Review> _reviewRepository;
        private readonly ILogger<ReviewController> _logger;
        private readonly IMapper _mapper;

        public ReviewController(IRepository<Review> reviewRepository, ILogger<ReviewController> logger, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews(int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching all reviews.");
            var (reviews, paginationMetaData) = await _reviewRepository.GetAllAsync(pageNumber, pageSize);
            return Ok(_mapper.Map<List<ReviewView>>(reviews));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            _logger.LogInformation($"Fetching review with id {id}");
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ReviewView>(review));
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewForCreate review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating new review.");
            var newReview = _mapper.Map<Review>(review);
            await _reviewRepository.AddAsync(newReview);
            return CreatedAtAction(nameof(GetReview), new { id = ((IBase)newReview).Id }, _mapper.Map<ReviewView>(newReview));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewForCreate review)
        {
            var oldReview = await _reviewRepository.GetByIdAsync(id);
            if (oldReview == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Updating review with id {id}");

            oldReview.Comment = review.Comment;
            oldReview.Rating = review.Rating;

            await _reviewRepository.UpdateAsync(oldReview);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            _logger.LogInformation($"Deleting review with id {id}");
            await _reviewRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}