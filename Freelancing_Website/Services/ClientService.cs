using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using Freelancing_Website.Interfaces;

namespace Freelancing_Website.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IReviewRepository _reviewRepository;

        public ClientService(IClientRepository clientRepository, IProjectRepository projectRepository, IReviewRepository reviewRepository)
        {
            _clientRepository = clientRepository;
            _projectRepository = projectRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client != null)
            {
                client.PostedProjects = (await _projectRepository.GetProjectsByClientIdAsync(id,1,int.MaxValue)).Items;
                client.ReviewsGiven = (await _reviewRepository.GetReviewsByClientIdAsync(id,1, int.MaxValue)).Items;
            }
            return client;
        }

        public async Task CreateClientAsync(Client client)
        {
            await _clientRepository.AddAsync(client);
        }

        public async Task UpdateClientAsync(Client client)
        {
            await _clientRepository.UpdateAsync(client);
        }

        public async Task DeleteClientAsync(int id)
        {
            await _projectRepository.DeleteProjectsByClientIdAsync(id);
            await _reviewRepository.DeleteReviewsByClientIdAsync(id);
            await _clientRepository.DeleteAsync(id);
        }

        public async Task<DataWithPagination<Review>> GetReviewsForClientAsync(int clientId, int pageNumber, int pageSize)
        {
            return await _reviewRepository.GetReviewsByClientIdAsync(clientId,pageNumber,pageSize);
        }

    }

}
