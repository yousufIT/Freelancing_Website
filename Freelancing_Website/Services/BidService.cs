// Freelancing_Website/Services/BidService.cs
using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using Freelancing_Website.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Freelancing_Website.Hubs;
using System.Threading.Tasks;

namespace Freelancing_Website.Services
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        private readonly IProjectRepository _projectRepository; // optional, for payload enrichment
        private readonly IHubContext<NotificationHub> _hubContext;

        public BidService(IBidRepository bidRepository, IProjectRepository projectRepository, IHubContext<NotificationHub> hubContext)
        {
            _bidRepository = bidRepository;
            _projectRepository = projectRepository;
            _hubContext = hubContext;
        }

        public async Task<DataWithPagination<Bid>> GetBidsByProjectIdAsync(int projectId, int pageNumber, int pageSize)
        {
            return await _bidRepository.GetBidsByProjectIdAsync(projectId, pageNumber, pageSize);
        }

        public async Task<DataWithPagination<Bid>> GetBidsByFreelancerIdAsync(int freelancerId, int pageNumber, int pageSize)
        {
            return await _bidRepository.GetBidsByFreelancerIdAsync(freelancerId, pageNumber, pageSize);
        }

        public async Task CreateBidAsync(int freelancerId, int projectId, Bid bid)
        {
            // Add bid to DB (existing repository method)
            await _bidRepository.AddBidToProjectAsync(freelancerId, projectId, bid);

            // Optional: fetch project title or other fields to enrich payload
            var project = await _projectRepository.GetByIdAsync(projectId);

            // Build a simple payload describing the new bid
            var payload = new
            {
                type = "NewBid",
                bidId = bid.Id,
                projectId,
                projectTitle = project?.Title,
                amount = bid.Amount,
                proposal = bid.Proposal,
                freelancerId,
                freelancerName = bid.Freelancer?.Name
            };

            // Broadcast to ALL connected clients
            await _hubContext.Clients.All.SendAsync("NewBid", payload);
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
