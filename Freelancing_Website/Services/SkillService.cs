using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using CodeSphere.Infrastructure.Repos;
using Freelancing_Website.Interfaces;

namespace Freelancing_Website.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IFreelancerRepository _freelancerRepository;

        public SkillService(ISkillRepository skillRepository, IFreelancerRepository freelancerRepository)
        {
            _skillRepository = skillRepository;
            _freelancerRepository = freelancerRepository;
        }

        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            var result = await _skillRepository.GetAllAsync(1, int.MaxValue);
            return result.Items ;
        }

        public async Task<Skill> GetSkillByIdAsync(int id)
        {
            return await _skillRepository.GetByIdAsync(id);
        }

        public async Task CreateSkillAsync(Skill skill)
        {
            await _skillRepository.AddAsync(skill);
        }

        public async Task UpdateSkillAsync(Skill skill)
        {
            await _skillRepository.UpdateAsync(skill);
        }

        public async Task DeleteSkillAsync(int id)
        {
            await _skillRepository.DeleteAsync(id);
        }
        public async Task ReplaceSkillsForFreelancerAsync(int freelancerId, List<int> skillIds)
        {
            var freelancer = await _freelancerRepository.GetByIdWithIncludesAsync(
                     freelancerId,
                         f => f.Profile,
                      f => f.Profile.Skills
                        );

            if (freelancer == null) throw new Exception("Freelancer not found");

            freelancer.Profile.Skills.Clear();

            var newSkills = await _skillRepository.GetByIdsAsync(skillIds);

            foreach (var skill in newSkills)
            {
                freelancer.Profile.Skills.Add(skill);
            }

            await _freelancerRepository.UpdateAsync(freelancer);
        }

        public async Task<List<Skill>> GetSkillsForFreelancerAsync(int id)
        {
            return await _skillRepository.GetSkillsForFreelancerAsync(id);
        }


        public async Task CreateSkillsToFreelancerAsync(int freelancerId,List<int>  skillsIds)
        {
            foreach (int skillId in skillsIds)
            {
                var skill = await GetSkillByIdAsync(skillId);
                await _skillRepository.AddSkillToFreelancerAsync(freelancerId, skill);
            }
        }
    }
}
