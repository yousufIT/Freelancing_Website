using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using Freelancing_Website.Interfaces;

namespace Freelancing_Website.Services
{
    public class RequiredSkillService : IRequiredSkillService
    {
        private readonly IRequiredSkillRepository _requiredSkillRepository;

        public RequiredSkillService(IRequiredSkillRepository requiredSkillRepository)
        {
            _requiredSkillRepository = requiredSkillRepository;
        }

        public async Task<DataWithPagination<RequiredSkill>> GetSkillsForProjectAsync(int projectId, int pageNumber, int pageSize)
        {
            return await _requiredSkillRepository.GetSkillsForProjectAsync(projectId, pageNumber, pageSize);
        }

        public async Task AddSkillToProjectAsync(int projectId, RequiredSkill skill)
        {
            await _requiredSkillRepository.AddSkillToProjectAsync(projectId, skill);
        }

        public async Task UpdateSkillForProjectAsync( RequiredSkill requiredSkill)
        {
            await _requiredSkillRepository.UpdateAsync(requiredSkill);
        }

        public async Task DeleteSkillsForProjectAsync(int projectId)
        {
            await _requiredSkillRepository.DeleteSkillsForProjectAsync(projectId);
        }

        public async Task AddSkillsToProjectAsync(int projectId, List<RequiredSkill> skills)
        {
            foreach (var skill in skills)
            {
                await _requiredSkillRepository.AddSkillToProjectAsync(projectId, skill);
            }
        }

        public async Task RemoveSkillFromProjectAsync(int projectId, int skillId)
        {
            await _requiredSkillRepository.DeleteSkillForProjectAsync(projectId, skillId);
        }

        public async Task<RequiredSkill> GetSkillByIdAsync(int id)
        {
            return await _requiredSkillRepository.GetByIdAsync(id);
        }
    }

}
