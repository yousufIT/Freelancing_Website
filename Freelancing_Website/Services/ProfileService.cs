using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using Freelancing_Website.Interfaces;

namespace Freelancing_Website.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPortfolioItemRepository _portfolioItemRepository;

        public ProfileService(IProfileRepository profileRepository, IPortfolioItemRepository portfolioItemRepository)
        {
            _profileRepository = profileRepository;
            _portfolioItemRepository = portfolioItemRepository;
        }

        public async Task<Profile> GetProfileAsync(int profileId)
        {
            return await _profileRepository.GetProfileWithPortfolioAsync(profileId);
        }

        public async Task<DataWithPagination<PortfolioItem>> GetPortfolioItemsAsync(int profileId, int pageNumber, int pageSize)
        {
            return await _portfolioItemRepository.GetPortfolioItemsByProfileIdAsync(profileId,pageNumber,pageSize);
        }

        public async Task<Profile> CreateProfileAsync(Profile profile)
        {
            await _profileRepository.AddAsync(profile);
            return profile;
        }

        public async Task<PortfolioItem> CreatePortfolioItemAsync(int profileId, PortfolioItem item)
        {
            item.ProfileId = profileId; // Set the profile ID for the portfolio item
            await _portfolioItemRepository.AddAsync(item);
            return item;
        }

        public async Task UpdateProfileAsync(Profile profile)
        {
            await _profileRepository.UpdateAsync(profile);
        }

        public async Task UpdatePortfolioItemAsync(PortfolioItem item)
        {
            await _portfolioItemRepository.UpdateAsync(item);
        }

        public async Task DeleteProfileAsync(int profileId)
        {
            var profile = await _profileRepository.GetByIdAsync(profileId);
            if (profile != null)
            {
                profile.IsDeleted = true;
                await _profileRepository.UpdateAsync(profile);
            }
        }

        public async Task DeletePortfolioItemAsync(int itemId)
        {
            var item = await _portfolioItemRepository.GetByIdAsync(itemId);
            if (item != null)
            {
                item.IsDeleted = true; 
                await _portfolioItemRepository.UpdateAsync(item);
            }
        }
    }
}
