using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using Freelancing_Website.Interfaces;

namespace Freelancing_Website.Services
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;

        public BidService(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        public async Task<DataWithPagination<Bid>> GetBidsByProjectIdAsync(int projectId, int pageNumber, int pageSize)
        {
            return await _bidRepository.GetBidsByProjectIdAsync(projectId, pageNumber, pageSize);
        }

        public async Task<DataWithPagination<Bid>> GetBidsByFreelancerIdAsync(int freelancerId, int pageNumber, int pageSize)
        {
            return await _bidRepository.GetBidsByFreelancerIdAsync(freelancerId, pageNumber, pageSize);
        }

        public async Task CreateBidAsync(int freelancerId,int projectId,Bid bid)
        {
            await _bidRepository.AddBidToProjectAsync(freelancerId,projectId,bid);
        }

        public async Task UpdateBidAsync(Bid bid)
        {
            await _bidRepository.UpdateAsync(bid);
        }

        public async Task DeleteBidAsync(int id)
        {
            await _bidRepository.DeleteAsync(id);
        }

        public async Task DeleteBidsForProjectAsync(int projectId)
        {
            await _bidRepository.DeleteBidsForProjectAsync(projectId);
        }

        public async Task<Bid> GetByIdAsync(int id)
        {
            return await _bidRepository.GetByIdAsync(id);
        }
    }

}
