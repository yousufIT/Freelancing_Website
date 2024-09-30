using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using Freelancing_Website.Interfaces;

namespace Freelancing_Website.Services
{
    public class FreelancerService : IFreelancerService
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ISkillRepository _skillRepository;

        public FreelancerService(
            IFreelancerRepository freelancerRepository,
            IProfileRepository profileRepository,
            IReviewRepository reviewRepository,
            ISkillRepository skillRepository)
        {
            _freelancerRepository = freelancerRepository;
            _profileRepository = profileRepository;
            _reviewRepository = reviewRepository;
            _skillRepository = skillRepository;
        }

        public async Task<Freelancer> GetFreelancerByIdAsync(int id)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(id);
            if (freelancer != null)
            {
                freelancer.Profile = await _profileRepository.GetProfileByFreelancerIdAsync(freelancer.Id);
                freelancer.Profile.Skills = (await _skillRepository.GetSkillsForFreelancerAsync(freelancer.Id,1,int.MaxValue)).Items;

                // Fix: Convert IEnumerable to List explicitly
                freelancer.ReviewsReceived = ((await _reviewRepository.GetReviewsByFreelancerIdAsync(freelancer.Id, 1, int.MaxValue)).Items).ToList();
            }
            return freelancer;
        }

        public async Task CreateFreelancerAsync(Freelancer freelancer)
        {
            await _freelancerRepository.AddAsync(freelancer);

            // Assign FreelancerId to Profile
            freelancer.Profile.FreelancerId = freelancer.Id;
            await _profileRepository.AddAsync(freelancer.Profile);

            foreach (var skillId in freelancer.Profile.Skills.Select(s => s.Id))
            {
                // Fix: Fetch Skill entity by Id
                var skill = await _skillRepository.GetByIdAsync(skillId);
                if (skill != null)
                {
                    await _skillRepository.AddSkillToFreelancerAsync(freelancer.Id, skill);
                }
            }
        }

        public async Task UpdateFreelancerAsync(Freelancer freelancer)
        {
            await _freelancerRepository.UpdateAsync(freelancer);
            await _profileRepository.UpdateAsync(freelancer.Profile);

            // Fix: Fetch the Skill entities from the list of skill IDs
            var skillEntities = new List<Skill>();
            foreach (var skillId in freelancer.Profile.Skills.Select(s => s.Id))
            {
                var skill = await _skillRepository.GetByIdAsync(skillId);
                if (skill != null)
                {
                    skillEntities.Add(skill);
                }
            }
            await _skillRepository.UpdateSkillsForFreelancerAsync(freelancer.Id, skillEntities);
        }

        public async Task DeleteFreelancerAsync(int id)
        {
            await _profileRepository.DeleteByFreelancerIdAsync(id);
            await _skillRepository.DeleteSkillsForFreelancerAsync(id);
            await _reviewRepository.DeleteReviewsByFreelancerIdAsync(id);
            await _freelancerRepository.DeleteAsync(id);
        }

        public async Task<DataWithPagination<Review>> GetReviewsForFreelancerAsync(int freelancerId, int pageNumber, int pageSize)
        {
            return await _reviewRepository.GetReviewsByFreelancerIdAsync(freelancerId,pageNumber,pageSize);
        }
    }



}
