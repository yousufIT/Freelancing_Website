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
            : base(context, logger) { }

        public async Task<DataWithPagination<Bid>> GetBidsByProjectIdAsync(int projectId, int pageNumber, int pageSize)
        {
            var bids = await _context.Bids.Where(b => b.ProjectId == projectId && !b.IsDeleted).ToListAsync();
            var totalItemCount = bids.Count();
            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            DataWithPagination<Bid> result = new DataWithPagination<Bid>();
            result.PaginationMetaData = paginationData;
            result.Items = bids;
            return  result;
        }

        public async Task<DataWithPagination<Bid>> GetBidsByFreelancerIdAsync(int freelancerId, int pageNumber, int pageSize)
        {
            var bids = await _context.Bids.Where(b => b.FreelancerId == freelancerId && !b.IsDeleted).ToListAsync();
            var totalItemCount = bids.Count();
            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            DataWithPagination<Bid> result = new DataWithPagination<Bid>();
            result.PaginationMetaData = paginationData;
            result.Items = bids;
            return result;
        }

        public async Task DeleteBidsForProjectAsync(int projectId)
        {
            var bids = await _context.Bids.Where(b => b.ProjectId == projectId).ToListAsync();
            foreach (var bid in bids)
            {
                await DeleteAsync(bid.Id);
            }
        }

        public async Task AddBidToProjectAsync(int freelancerId, int projectId, Bid bid)
        {

            var freelancer = await _context.Freelancers
                .Include(f=>f.Bids)
                .FirstOrDefaultAsync(f => f.Id == freelancerId);
            var project=await _context.Projects
                .Include(p=>p.Bids)
                .FirstOrDefaultAsync(p=>p.Id == projectId);
            bid.FreelancerId = freelancerId;
            bid.Freelancer = freelancer;
            bid.ProjectId = projectId;
            bid.Project = project;
            await AddAsync(bid);
            freelancer.Bids.Add(bid);
            project.Bids.Add(bid);
            
            _context.SaveChanges();

        }
    }

}
