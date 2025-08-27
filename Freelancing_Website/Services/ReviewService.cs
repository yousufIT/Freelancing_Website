using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using Freelancing_Website.Interfaces;

namespace Freelancing_Website.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IFreelancerRepository _freelancerRepository;

        public ReviewService(IReviewRepository reviewRepository, IFreelancerRepository freelancerRepository)
        {
            _reviewRepository = reviewRepository;
            _freelancerRepository = freelancerRepository;
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

            await RecalculateFreelancerRatingAsync(freelancerId);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            await _reviewRepository.UpdateAsync(review);

            await RecalculateFreelancerRatingAsync(review.FreelancerId);
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) return;

            // mark deleted (repository.DeleteAsync sets IsDeleted = true)
            await _reviewRepository.DeleteAsync(id);

            // recalc rating for that freelancer
            await RecalculateFreelancerRatingAsync(review.FreelancerId);
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _reviewRepository.GetByIdAsync(id);
        }

        private async Task RecalculateFreelancerRatingAsync(int freelancerId)
        {
            // fetch non-deleted reviews for freelancer
            var reviews = await _reviewRepository.GetReviewsByFreelancerIdAsync(freelancerId);

            double newRating = 0;
            if (reviews != null && reviews.Count > 0)
            {
                // Average rating (safe conversion)
                newRating = Math.Round(reviews.Average(r => r.Rating), 2);
            }

            var freelancer = await _freelancerRepository.GetByIdAsync(freelancerId);
            if (freelancer != null)
            {
                freelancer.Rating = newRating;
                await _freelancerRepository.UpdateAsync(freelancer);
            }
        }

    }
}
