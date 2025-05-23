﻿using CodeSphere.Domain.Interfaces.Repos;
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
            var project = await _projectRepository.GetProjectById(id);
            return project;
        }

        public async Task CreateProjectAsync(int clientId, Project project)
        {
            await _projectRepository.AddProjectToClient(clientId,project);
        }

        public async Task UpdateProjectAsync(Project project)
        {
            await _projectRepository.UpdateAsync(project);
        }

        public async Task DeleteProjectAsync(int id)
        {
            await _bidRepository.DeleteBidsForProjectAsync(id);
            await _requiredSkillRepository.DeleteSkillsForProjectAsync(id);
            await _projectRepository.DeleteAsync(id);
        }

        public async Task<DataWithPagination<Project>> GetProjectsBySkills(List<int> skillsIds,int pageNumber, int pageSize)
        {

            return await _projectRepository.GetProjectsBySkillsAsync(skillsIds,pageNumber,pageSize);
        }
    }

}
