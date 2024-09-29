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

        public async Task<IEnumerable<Review>> GetReviewsByClientIdAsync(int clientId)
        {
            _logger.LogInformation($"Fetching reviews for client ID {clientId}");
            return await _context.Reviews
                .Where(r => r.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByFreelancerIdAsync(int freelancerId)
        {
            _logger.LogInformation($"Fetching reviews for freelancer ID {freelancerId}");
            return await _context.Reviews
                .Where(r => r.FreelancerId == freelancerId)
                .ToListAsync();
        }
        public async Task DeleteReviewsByFreelancerIdAsync(int freelancerId)
        {
            var reviews = await _context.Reviews
                .Where(review => review.FreelancerId == freelancerId && !review.IsDeleted)
                .ToListAsync();

            foreach (var review in reviews)
            {
                review.IsDeleted = true; // Soft delete
                await UpdateAsync(review);
            }
        }
    }
}
