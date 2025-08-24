using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ViewModels;
using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancing_Website.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IClientRepository _clientRepo;
        private readonly IFreelancerRepository _freelancerRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IBidRepository _bidRepo;
        private readonly IReviewRepository _reviewRepo;
        private readonly ISkillRepository _skillRepo;

        public DashboardService(
            IClientRepository clientRepo,
            IFreelancerRepository freelancerRepo,
            IProjectRepository projectRepo,
            IBidRepository bidRepo,
            IReviewRepository reviewRepo,
            ISkillRepository skillRepo)
        {
            _clientRepo = clientRepo;
            _freelancerRepo = freelancerRepo;
            _projectRepo = projectRepo;
            _bidRepo = bidRepo;
            _reviewRepo = reviewRepo;
            _skillRepo = skillRepo;
        }

        public async Task<DashboardSummaryView> GetDashboardSummaryAsync(int recentCount = 5)
        {
            // Use repository.GetAllAsync(1,1) to get counts from PaginationMetaData
            var clientsPage = await _clientRepo.GetAllAsync(1, 1);
            var freelancersPage = await _freelancerRepo.GetAllAsync(1, 1);
            var projectsPage = await _projectRepo.GetAllAsync(1, 1);
            var bidsPage = await _bidRepo.GetAllAsync(1, 1);
            var reviewsPage = await _reviewRepo.GetAllAsync(1, 1);
            var skillsPage = await _skillRepo.GetAllAsync(1, 1);

            var totalClients = clientsPage.PaginationMetaData.TotalItemCount;
            var totalFreelancers = freelancersPage.PaginationMetaData.TotalItemCount;
            var totalProjects = projectsPage.PaginationMetaData.TotalItemCount;
            var totalBids = bidsPage.PaginationMetaData.TotalItemCount;
            var totalReviews = reviewsPage.PaginationMetaData.TotalItemCount;
            var totalSkills = skillsPage.PaginationMetaData.TotalItemCount;

            // Recent / top lists using specialized repo methods
            var recentProjects = await _projectRepo.GetRecentProjectsAsync(recentCount);
            var topFreelancers = await _freelancerRepo.GetTopFreelancersByRatingAsync(recentCount);
            var recentBids = await _bidRepo.GetRecentBidsAsync(recentCount);

            // Map to view models (manual mapping keeps dependency minimal)
            var recentProjectsVm = recentProjects.Select(p => new RecentProjectView
            {
                Id = p.Id,
                Title = p.Title,
                ClientName = p.Client?.Name,
                Budget = p.Budget,
                Status = p.Status,
                BidCount = p.Bids?.Count ?? 0
            }).ToList();

            var topFreelancersVm = topFreelancers.Select(f => new TopFreelancerView
            {
                Id = f.Id,
                Name = f.Name,
                Rating = f.Rating,
                CompletedProjectsCount = f.CompletedProjects?.Count ?? 0
            }).ToList();

            var recentBidsVm = recentBids.Select(b => new RecentBidView
            {
                Id = b.Id,
                Amount = b.Amount,
                Proposal = b.Proposal,
                FreelancerName = b.Freelancer?.Name,
                ProjectTitle = b.Project?.Title
            }).ToList();

            var summary = new DashboardSummaryView
            {
                TotalClients = totalClients,
                TotalFreelancers = totalFreelancers,
                TotalProjects = totalProjects,
                TotalBids = totalBids,
                TotalReviews = totalReviews,
                TotalSkills = totalSkills,
                RecentProjects = recentProjectsVm,
                TopFreelancers = topFreelancersVm,
                RecentBids = recentBidsVm
            };

            return summary;
        }
    }
}
