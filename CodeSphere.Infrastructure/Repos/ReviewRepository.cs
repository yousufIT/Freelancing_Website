using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using CodeSphere.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Infrastructure.Repos
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(CodeSphereContext context, ILogger<Repository<Review>> logger)
            : base(context, logger)
        {
        }

        public async Task DeleteReviewsByFreelancerIdAsync(int freelancerId)
        {
            var reviews = await _context.Reviews
                .Where(review => review.FreelancerId == freelancerId && !review.IsDeleted)
                .ToListAsync();

            foreach (var review in reviews)
            {
                review.IsDeleted = true; 
                await UpdateAsync(review);
            }
        }

        public async Task DeleteReviewsByClientIdAsync(int clientId)
        {
            var reviews = await _context.Reviews.Where(r => r.ClientId == clientId).ToListAsync();
            foreach (var review in reviews)
            {
                await DeleteAsync(review.Id);
            }
        }

        public async Task<DataWithPagination<Review>> GetReviewsByClientIdAsync(int clientId, int pageNumber, int pageSize)
        {
            var reviews = await _context.Reviews
                .Where(r => r.ClientId == clientId && !r.IsDeleted)
                .ToListAsync();

            var totalItemCount = reviews.Count();

            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            DataWithPagination<Review> result = new DataWithPagination<Review>();
            result.PaginationMetaData = paginationData;
            result.Items = reviews;
            return result;
        }

        public async Task<DataWithPagination<Review>> GetReviewsByFreelancerIdAsync(int freelancerId, int pageNumber, int pageSize)
        {
            var reviews = await _context.Reviews
                .Where(r => r.FreelancerId == freelancerId && !r.IsDeleted)
                .ToListAsync();

            var totalItemCount = reviews.Count();

            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            DataWithPagination<Review> result = new DataWithPagination<Review>();
            result.PaginationMetaData = paginationData;
            result.Items = reviews;
            return result;
        }

        public async Task AddReviewToFreelancerAndClient(int clientId, int freelancerId, Review review)
        {
            var client=await _context.Clients
                .Include(c =>c.ReviewsGiven)
                .FirstOrDefaultAsync(c => c.Id==clientId);
            var freelancer=await _context.Freelancers
                .Include(f => f.ReviewsReceived)
                .FirstOrDefaultAsync(f => f.Id==freelancerId);
            review.FreelancerId = freelancerId;
            review.Freelancer = freelancer;
            review.ClientId = clientId;
            review.Client = client;
            await AddAsync(review);
            freelancer.ReviewsReceived.Add(review);
            client.ReviewsGiven.Add(review);
            await _context.SaveChangesAsync();
        }
    }
}
