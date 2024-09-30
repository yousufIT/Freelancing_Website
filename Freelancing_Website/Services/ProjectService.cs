using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using Freelancing_Website.Interfaces;

namespace Freelancing_Website.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IRequiredSkillRepository _requiredSkillRepository;

        public ProjectService(IProjectRepository projectRepository, IBidRepository bidRepository, IRequiredSkillRepository requiredSkillRepository)
        {
            _projectRepository = projectRepository;
            _bidRepository = bidRepository;
            _requiredSkillRepository = requiredSkillRepository;
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project != null)
            {
                project.Bids = (await _bidRepository.GetBidsByProjectIdAsync(id, 1, int.MaxValue)).Items;
                project.RequiredSkills = (await _requiredSkillRepository.GetSkillsForProjectAsync(id, 1, int.MaxValue)).Items;
            }
            return project;
        }

        public async Task CreateProjectAsync(Project project)
        {
            await _projectRepository.AddAsync(project);
            foreach (var requiredSkill in project.RequiredSkills)
            {
                var skill = new Skill
                {
                    Id = requiredSkill.Id,
                    Name = requiredSkill.Name
                };
                await _requiredSkillRepository.AddSkillToProjectAsync(project.Id, skill);
            }
        }

        public async Task UpdateProjectAsync(Project project)
        {
            await _projectRepository.UpdateAsync(project);
            var requiredSkills = project.RequiredSkills.Select(rs => new RequiredSkill
            {
                Id = rs.Id,
                Name = rs.Name
            }).ToList();

            await _requiredSkillRepository.UpdateSkillsForProjectAsync(project.Id, requiredSkills);
        }

        public async Task DeleteProjectAsync(int id)
        {
            await _bidRepository.DeleteBidsForProjectAsync(id);
            await _requiredSkillRepository.DeleteSkillsForProjectAsync(id);
            await _projectRepository.DeleteAsync(id);
        }
    }

}
