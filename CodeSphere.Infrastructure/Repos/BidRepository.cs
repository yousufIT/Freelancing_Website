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
    public class BidRepository : Repository<Bid>, IBidRepository
    {
        public BidRepository(CodeSphereContext context, ILogger<Repository<Bid>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<Bid>> GetBidsByProjectIdAsync(int projectId)
        {
            _logger.LogInformation($"Fetching bids for project ID {projectId}");
            return await _context.Bids.Where(b => b.ProjectId == projectId).ToListAsync();
        }

        public async Task<IEnumerable<Bid>> GetBidsByFreelancerIdAsync(int freelancerId)
        {
            _logger.LogInformation($"Fetching bids for freelancer ID {freelancerId}");
            return await _context.Bids.Where(b => b.FreelancerId == freelancerId).ToListAsync();
        }
    }

}
