using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using Freelancing_Website.Interfaces;

namespace Freelancing_Website.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<DataWithPagination<Review>> GetReviewsByFreelancerIdAsync(int freelancerId, int pageNumber, int pageSize)
        {
            return await _reviewRepository.GetReviewsByFreelancerIdAsync(freelancerId, pageNumber, pageSize);
        }

        public async Task<DataWithPagination<Review>> GetReviewsByClientIdAsync(int clientId, int pageNumber, int pageSize)
        {
            return await _reviewRepository.GetReviewsByClientIdAsync(clientId, pageNumber, pageSize);
        }

        public async Task CreateReviewAsync(int clientId,int freelancerId,Review review)
        {
            await _reviewRepository.AddReviewToFreelancerAndClient(clientId,freelancerId, review);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteReviewAsync(int id)
        {
            await _reviewRepository.DeleteAsync(id);
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _reviewRepository.GetByIdAsync(id);
        }
    }
}
