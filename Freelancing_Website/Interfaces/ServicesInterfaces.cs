using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;

namespace Freelancing_Website.Interfaces
{
    public interface IFreelancerService
    {
        Task<Freelancer> GetFreelancerByIdAsync(int id);
        Task CreateFreelancerAsync(Freelancer freelancer);
        Task UpdateFreelancerAsync(Freelancer freelancer);
        Task DeleteFreelancerAsync(int id);
        Task<DataWithPagination<Review>> GetReviewsForFreelancerAsync(int freelancerId, int pageNumber, int pageSize);
    }

    public interface IClientService
    {
        Task<Client> GetClientByIdAsync(int id);
        Task CreateClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
        Task<DataWithPagination<Review>> GetReviewsForClientAsync(int clientId, int pageNumber, int pageSize);
    }


    public interface IProjectService
    {
        Task<Project> GetProjectByIdAsync(int id);
        Task CreateProjectAsync(int clientId,Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int id);
    }


    public interface ISkillService
    {
        Task<DataWithPagination<Skill>> GetAllSkillsAsync(int pageNumber, int pageSize);
        Task<Skill> GetSkillByIdAsync(int id);
        Task CreateSkillAsync(Skill skill);
        Task UpdateSkillAsync(Skill skill);
        Task DeleteSkillAsync(int id);
    }

    public interface IReviewService
    {
        Task<DataWithPagination<Review>> GetReviewsByFreelancerIdAsync(int freelancerId, int pageNumber, int pageSize);
        Task<DataWithPagination<Review>> GetReviewsByClientIdAsync(int clientId, int pageNumber, int pageSize);
        Task CreateReviewAsync(int clientId,int freelancerId, Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(int id);
    }
    public interface IBidService
    {
        Task<DataWithPagination<Bid>> GetBidsByProjectIdAsync(int projectId, int pageNumber, int pageSize);
        Task<Bid> GetByIdAsync(int id);
        Task CreateBidAsync(int freelancerId, int projectId, Bid bid);
        Task UpdateBidAsync(Bid bid);
        Task DeleteBidAsync(int bidId);
        Task DeleteBidsForProjectAsync(int projectId);
        Task<DataWithPagination<Bid>> GetBidsByFreelancerIdAsync(int freelancerId, int pageNumber, int pageSize);
    }
    public interface IRequiredSkillService
    {
        Task<DataWithPagination<RequiredSkill>> GetSkillsForProjectAsync(int projectId, int pageNumber, int pageSize);
        Task<RequiredSkill> GetSkillByIdAsync(int id);
        Task AddSkillToProjectAsync(int projectId, RequiredSkill skill);
        Task AddSkillsToProjectAsync(int projectId, List<RequiredSkill> skills);
        Task DeleteSkillsForProjectAsync(int projectId);
        Task UpdateSkillForProjectAsync(RequiredSkill requiredSkill);
        Task RemoveSkillFromProjectAsync(int projectId, int skillId);
    }

    public interface IProfileService
    {
        Task<DataWithPagination<PortfolioItem>> GetPortfolioItemsAsync(int profileId, int pageNumber, int pageSize);
        Task<PortfolioItem> CreatePortfolioItemAsync(int profileId, PortfolioItem item);
        Task<PortfolioItem> GetPortfolioItemByIdAsync(int id);
        Task UpdatePortfolioItemAsync(PortfolioItem item);
        Task DeletePortfolioItemAsync(int itemId);
    }

}
