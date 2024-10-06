using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using Freelancing_Website.Interfaces;

namespace Freelancing_Website.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<DataWithPagination<Skill>> GetAllSkillsAsync(int pageNumber,int pageSize)
        {
            var result = await _skillRepository.GetAllAsync(pageNumber, pageSize);
            return result ;
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

        public async Task<DataWithPagination<Skill>> GetSkillsForFreelancerAsync(int id, int pageNumber, int pageSize)
        {
            return await _skillRepository.GetSkillsForFreelancerAsync(id, pageNumber, pageSize);
        }

        public async Task CreateSkillsToFreelancerAsync(int freelancerId,List<Skill>  skills)
        {
            foreach (Skill skill in skills)
             await _skillRepository.AddSkillToFreelancerAsync(freelancerId, skill);
        }
    }
}
