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

        public async Task<List<Skill>> GetSkillsForProjectAsync(int projectId)
        {
            return await _requiredSkillRepository.GetSkillsForProjectAsync(projectId);
        }

        public async Task AddSkillToProjectAsync(int projectId, Skill skill)
        {
            await _requiredSkillRepository.AddSkillToProjectAsync(projectId, skill);
        }

        public async Task UpdateSkillForProjectAsync( Skill requiredSkill)
        {
            await _requiredSkillRepository.UpdateAsync(requiredSkill);
        }

        public async Task DeleteSkillsForProjectAsync(int projectId)
        {
            await _requiredSkillRepository.DeleteSkillsForProjectAsync(projectId);
        }

        public async Task AddSkillsToProjectAsync(int projectId, List<Skill> skills)
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

        public async Task<Skill> GetSkillByIdAsync(int id)
        {
            return await _requiredSkillRepository.GetByIdAsync(id);
        }
    }

}
